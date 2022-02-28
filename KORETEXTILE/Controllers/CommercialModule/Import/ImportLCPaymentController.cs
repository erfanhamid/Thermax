using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.Bridge;
using BEEERP.Models.CommercialModule.Import;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using BEEERP.Models.ViewModel.CommercialVM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.CommercialModule.Import
{
    //[Permission]
    [AccountingModule]
    [ShowNotification]
    public class ImportLCPaymentController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: ImportLCPayment
        public ActionResult ImportLCPayment()
        {
            ViewBag.Debit = LoadComboBox.LoadAllAccount();
            ViewBag.Credit = LoadComboBox.LoadAllAccount();
            return View();
        }
        public ActionResult GetLcInfo(int liId)
        {
            if (liId != 0)
            {
                var lcDetails = (from l in context.ImportLC
                                 where l.ILCId == liId
                                 select new { ILCID = l.ILCId, ILCNo = l.ILCNO, AltILCNo = l.IALCNO }).FirstOrDefault();
                if (lcDetails != null)
                {
                    return Json(lcDetails, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }

            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetILCPaymentInfo(int id)
        {
            if (id != 0)
            {
                var lcDetails = (from l in context.ImportLCPaymentD
                                 join lc in context.ImportLC on l.ILCID equals lc.ILCId 
                                 where l.ILCPNo == id
                                 select new { found = 1,ILCID = l.ILCID, ILCNo = lc.ILCNO, AltILCNo = l.AltILCNo, Date = l.Date, RefNo = l.RefNo, Description = l.Description,debit=l.DebitAccID,credit=l.CreditAccID,Amount=l.Amount }).ToList();
                if (lcDetails.Count>0)
                {
                    var lcp = lcDetails.FirstOrDefault();
                    return Json(lcp, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { found = 0}, JsonRequestBehavior.AllowGet);
                }
            }

            else
            {
                return Json(new { found = 0 }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult SaveILCPaymentInfo(ImportLCPayment importLCPayment)
        {
            if (importLCPayment.ILCPNo == 0)
            {
                string ilcbNo = "";
                string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
                var noOfInvoice = context.ImportLCPaymentD.Count();
                if (noOfInvoice > 0)
                {
                    var lastInvoice = context.ImportLCPaymentD.ToList().LastOrDefault(x => x.ILCPNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                    ilcbNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.ILCPNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                }
                else
                {
                    ilcbNo = yearLastTwoPart + "00001";
                }

                importLCPayment.ILCPNo = Convert.ToInt32(ilcbNo);
                context.ImportLCPaymentD.Add(importLCPayment);
                AccountModuleBridge.InsertFromILCPD(ref context, importLCPayment);
                LcBalanceCalculationBridge.InsertFromImportLCPaymentD(ref context, importLCPayment);
                UserLog.SaveUserLog(ref context, new UserLog(importLCPayment.ILCPNo.ToString(), DateTime.Now, "ILCP", ScreenAction.Save));
            }
            else
            {
                var ilcbExist = context.ImportLCPaymentD.FirstOrDefault(x => x.ILCPNo == importLCPayment.ILCPNo);
                ilcbExist.AltILCNo = importLCPayment.AltILCNo;
                ilcbExist.Amount = importLCPayment.Amount;
                ilcbExist.CreditAccID = importLCPayment.CreditAccID;
                ilcbExist.Date = importLCPayment.Date;
                ilcbExist.DebitAccID = importLCPayment.DebitAccID;
                ilcbExist.Description = importLCPayment.Description;
                ilcbExist.ILCID = importLCPayment.ILCID;
                ilcbExist.RefNo = importLCPayment.RefNo;
                AccountModuleBridge.InsertFromILCPD(ref context, ilcbExist);
                LcBalanceCalculationBridge.InsertFromImportLCPaymentD(ref context, ilcbExist);
                UserLog.SaveUserLog(ref context, new UserLog(importLCPayment.ILCPNo.ToString(), DateTime.Now, "ILCP", ScreenAction.Update));
            }
            int i = context.SaveChanges();
            if (i > 0)
            {
                return Json(new { id = importLCPayment.ILCPNo,ilcpd = importLCPayment.ILCPNo }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { id = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteFromILCPD(int id)
        {
            var ilcpd = context.ImportLCPaymentD.FirstOrDefault(x => x.ILCPNo == id);
            if (ilcpd != null)
            {
                context.ImportLCPaymentD.Remove(ilcpd);
                AccountModuleBridge.DeleteFromILCPD(ref context, ilcpd);
                LcBalanceCalculationBridge.DeleteFromImportLCPaymentD(ref context, ilcpd);
                UserLog.SaveUserLog(ref context, new UserLog(ilcpd.ILCPNo.ToString(), DateTime.Now, "ILCPD", ScreenAction.Delete));
                int i = context.SaveChanges();
                if (i > 0)
                {
                    return Json(new { id = 1 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { id = 0 }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { id = 0 }, JsonRequestBehavior.AllowGet);
            }
        }


        public FileResult GetImportLCPaymentPreview(int iLCPNo)
        {
            Session["ReportName"] = "PreviewImportLCPaymentR";
            ReportParmPersister param = new ReportParmPersister();

            var ilcp = context.ImportLCPaymentD.FirstOrDefault(x => x.ILCPNo == iLCPNo);
            if (ilcp != null)
            {
                param.SampleNo = ilcp.ILCPNo;
                param.SampleDate = ilcp.Date;
                param.ID = ilcp.ILCID;
            }
            else
            {
                param.SampleNo = 0;
                param.ID = 0;
            }

            param.ILCNo = context.ImportLC.FirstOrDefault(x => x.ILCId == ilcp.ILCID).ILCNO;
            var supplierId = context.ImportLC.FirstOrDefault(x => x.ILCId == ilcp.ILCID).SupplierId;
            param.SupplierName = context.Supplier.FirstOrDefault(x => x.Id == supplierId).SupplierName;

            param.PostedBy = (from e in context.Employees.AsEnumerable()
                              join u in context.UserLog.AsEnumerable() on e.LogInUserName equals u.LogInName
                              where u.ScreenName == "ILCPD" && u.Action == "Save" && u.TrnasId == ilcp.ILCPNo.ToString()
                              select e.Name).FirstOrDefault();

            param.ApprovedBy = (from e in context.Employees.AsEnumerable()
                                join u in context.UserLog.AsEnumerable() on e.LogInUserName equals u.LogInName
                                where u.ScreenName == "ILCPD" && u.Action == "Approve" && u.TrnasId == ilcp.ILCPNo.ToString()
                                select e.Name).FirstOrDefault();

            if (param.ApprovedBy == null)
            {
                param.ApprovedBy = "";
            }

            string sql = string.Format("exec PreviewImportLCPayment '" + iLCPNo + "'");
            var items = context.Database.SqlQuery<ImportLCPaymentReport>(sql).ToList();
            if (items.Count == 0)
            {
                ImportLCPaymentReport report = new ImportLCPaymentReport();
                items.Add(report);
            }

            PreviewImportLCPaymentR rd = ShowReport(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");

        }

        public PreviewImportLCPaymentR ShowReport(ReportParmPersister persister, List<ImportLCPaymentReport> data)
        {
            PreviewImportLCPaymentR previewImportLCPaymentR = new PreviewImportLCPaymentR();

            previewImportLCPaymentR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\PreviewImportLCPaymentR.rpt");
            previewImportLCPaymentR.Load(APPPATH);
            previewImportLCPaymentR.SetDataSource(data);

            previewImportLCPaymentR.SetParameterValue("ilcpdNo", persister.SampleNo);
            previewImportLCPaymentR.SetParameterValue("sampleDate", persister.SampleDate.ToString("dd-MM-yyyy"));
            previewImportLCPaymentR.SetParameterValue("ilcId", persister.ID);
            previewImportLCPaymentR.SetParameterValue("ilcNo", persister.ILCNo);
            previewImportLCPaymentR.SetParameterValue("supName", persister.SupplierName);

            previewImportLCPaymentR.SetParameterValue("postedBy", persister.PostedBy);
            previewImportLCPaymentR.SetParameterValue("approvedBy", persister.ApprovedBy);

            previewImportLCPaymentR.SetParameterValue("compName", persister.CompName);
            previewImportLCPaymentR.SetParameterValue("compAddress", persister.CompAddress);
            previewImportLCPaymentR.SetParameterValue("compContact", persister.CompContact);
            previewImportLCPaymentR.SetParameterValue("compFax", persister.Fax);
            previewImportLCPaymentR.SetParameterValue("factAddress", persister.FactAddress);
            previewImportLCPaymentR.SetParameterValue("factContact", persister.FactContact);
            return previewImportLCPaymentR;
        }
    }
}