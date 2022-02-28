using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.Procurement;
using System.Data.Entity;
using BEEERP.Models.Log;
using BEEERP.Models.Bridge;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using System.IO;

namespace BEEERP.Controllers.Procurement
{
    [ShowNotification]
    //[Authorize(Roles = "ProAdmin,ProOperator,ProViewer,ProApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    //[ProcurementModule]
    public class GeneralBillEntryController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: GeneralBillEntry
        public ActionResult GeneralBillEntry()
        {
            //var gbeCountNum = context.GeneralBills.Count();
            //if (gbeCountNum == 0)
            //{
            //    ViewBag.gbeCountNumPlausOne = 1;
            //}
            //else
            //{
            //    ViewBag.gbeCountNumPlausOne = context.GeneralBills.ToList().Max(m => m.GBENo) + 1;
            //}

            ViewBag.SupplierGroup = LoadComboBox.LoadAllSupplierGroup();
            ViewBag.Depot = LoadComboBox.LoadBranchInfo();
            ViewBag.Supplier = LoadComboBox.LoadSupplier(null);
            ViewBag.DebitAcc = LoadComboBox.LoadAllAccount();
            ViewBag.CostGroup = LoadComboBox.LoadALLCostGroup();
     
            return View();
        }

        public ActionResult GetSupplierByGroupId(int groupId)
        {
            return Json(LoadComboBox.LoadSupplier(groupId), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveGBE(GeneralBill generalBill, List<GeneralBillsLineItem> generalBillsLineItems)
        {
            //var gbeCountNum = context.GeneralBills.Count();
            //if (gbeCountNum == 0)
            //{
            //    generalBill.GBENo = 1;
            //}
            //else
            //{
            //    generalBill.GBENo = context.GeneralBills.ToList().Max(m => m.GBENo) + 1;
            //}

            string yearLastTwoPart = generalBill.GBEDate.Year.ToString().Substring(2, 2).ToString();
            var noOfInvoice = context.FreezerWorkOrder.Count();
            string GBENo = "";

            if (noOfInvoice > 0)
            {
                var lastInvoice = context.GeneralBills.ToList().LastOrDefault(x => x.GBENo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                if (lastInvoice == null)
                {
                    GBENo = yearLastTwoPart + "00001";
                }
                else
                {
                    GBENo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.GBENo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                }
            }
            else
            {
                GBENo = yearLastTwoPart + "00001";
            }


            generalBill.GBENo = Convert.ToInt32(GBENo);
            generalBill.GBEDate = generalBill.GBEDate.Add(DateTime.Now.TimeOfDay);
            try
            {
                context.GeneralBills.Add(generalBill);
                generalBillsLineItems.ForEach(m => { m.GBENo = generalBill.GBENo; context.GeneralBillsLineItems.Add(m); });
                UserLog.SaveUserLog(ref context, new UserLog(generalBill.GBENo.ToString(), generalBill.GBEDate, "GSB", ScreenAction.Save));
                AccountModuleBridge.InsertUpdateFromGeneralBillEntry(ref context, generalBill, generalBillsLineItems);
                SupplierBalanceCalculationBridge.InsertUpdateFromGeneralBillEntry(ref context, generalBill, generalBillsLineItems);
                context.SaveChanges();
                return Json(new { gbe=generalBill.GBENo,Message=1, JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                return Json(new { Message = 0, JsonRequestBehavior.AllowGet });
            }
        }
        public ActionResult UpdateGBE(GeneralBill generalBill, List<GeneralBillsLineItem> generalBillsLineItems)
        {
            try
            {
                var existGBE = context.GeneralBills.AsNoTracking().FirstOrDefault(m => m.GBENo == generalBill.GBENo);
                if (existGBE == null)
                {
                    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var prevGBELineItems = context.GeneralBillsLineItems.Where(m => m.GBENo == generalBill.GBENo).ToList();
                    prevGBELineItems.ForEach(m => { context.GeneralBillsLineItems.Remove(m); });
                    context.Entry<GeneralBill>(generalBill).State = EntityState.Modified;
                    generalBillsLineItems.ForEach(m => { m.GBENo = generalBill.GBENo; context.GeneralBillsLineItems.Add(m); });
                    UserLog.SaveUserLog(ref context, new UserLog(generalBill.GBENo.ToString(), generalBill.GBEDate, "GSB", ScreenAction.Update));
                    AccountModuleBridge.InsertUpdateFromGeneralBillEntry(ref context, generalBill, generalBillsLineItems);
                    SupplierBalanceCalculationBridge.InsertUpdateFromGeneralBillEntry(ref context, generalBill, generalBillsLineItems);
                    context.SaveChanges();
                }
                return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = 0, JsonRequestBehavior.AllowGet });
            }
        }
        public ActionResult SearcheGBEByGBENo(int generalBill)
        {
            if (generalBill <= 0)
            {
                return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var GBE = context.GeneralBills.AsNoTracking().FirstOrDefault(m => m.GBENo == generalBill);
               
                if (GBE == null)
                {
                    return Json(new { Message = 0, JsonRequestBehavior.AllowGet });
                }
                else
                {
                    GBE.SGId = context.Supplier.FirstOrDefault(x => x.Id == GBE.SupplierId).GroupID;
                    GBE.AIT = (GBE.AITAmount / GBE.GBETotal) * 100;
                    GBE.VAT = (GBE.VATAmount / GBE.GBETotal) * 100;
                    GBE.VDS = (GBE.VDSAmount / GBE.GBETotal) * 100;
                    var gbeLineItems = context.GeneralBillsLineItems.Where(m => m.GBENo == GBE.GBENo).ToList();
                    gbeLineItems.ForEach(m =>
                    {
                        m.DebitAccountName = context.ChartOfAccount.FirstOrDefault(n => n.Id == m.DebitAccId).Name;
                        m.CostGroupName = context.EmployeeSection.FirstOrDefault(n => n.ID == m.CGId).CGroup;
                        //m.BusinessUnitName = context.BussinessUnit.FirstOrDefault(n => n.ID == m.BusinessUnit).Name;
                    });
                    return Json(new { GBE = GBE, GBELineItems = gbeLineItems, Message = 1, JsonRequestBehavior.AllowGet });
                }
            }

        }

        public ActionResult DeleteGBByID(int gbeno)
        {
            var isExist = context.GeneralBills.FirstOrDefault(x => x.GBENo == gbeno);
            if(isExist != null)
            {
                context.GeneralBills.Remove(isExist);
                var line = context.GeneralBillsLineItems.Where(x => x.GBENo == gbeno).ToList();
                foreach(var x in line)
                {
                    context.GeneralBillsLineItems.Remove(x);
                }
                UserLog.SaveUserLog(ref context, new UserLog(isExist.GBENo.ToString(), isExist.GBEDate, "GSB", ScreenAction.Delete));
                AccountModuleBridge.DeleteFromGeneralBillEntry(ref context, isExist.GBENo);
                SupplierBalanceCalculationBridge.DeleteFromGeneralBillEntry(ref context, isExist);
                context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public FileResult GetGBEPreview(int GBENo)
        {
            ReportParmPersister param = new ReportParmPersister();

            var gsb = context.GeneralBills.FirstOrDefault(x => x.GBENo == GBENo);
            var postedBy = (from e in context.Employees.AsEnumerable()
                              join u in context.UserLog.AsEnumerable() on e.LogInUserName equals u.LogInName
                              where u.ScreenName == "GSB" && u.Action == "Save" && u.TrnasId == gsb.GBENo.ToString()
                              select e.Name).FirstOrDefault();
            if(postedBy != null)
            {
                param.PostedBy = postedBy;
            }
            else
            {
                param.PostedBy = "";
            }
            string sql = string.Format("exec spPreviewGSB '" + GBENo + "'");
            var items = context.Database.SqlQuery<PreviewGBEReport>(sql).ToList();
            if (items.Count == 0)
            {
                PreviewGBEReport report = new PreviewGBEReport();
                items.Add(report);
            }

            PreviewGBER rd = ShowReport(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");

        }
        public PreviewGBER ShowReport(ReportParmPersister persister, List<PreviewGBEReport> data)
        {
            PreviewGBER gbe = new PreviewGBER();

            gbe.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\PreviewGBER.rpt");
            gbe.Load(APPPATH);
            gbe.SetDataSource(data);
            gbe.SetParameterValue("postedBy", persister.PostedBy);
            return gbe;
        }

    }
}
