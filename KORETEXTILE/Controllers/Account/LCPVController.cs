using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CommercialModule.Import;
using BEEERP.Models.Log;
using BEEERP.Models.Bridge;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using System.IO;
using BEEERP.Models.AccountModule;

namespace BEEERP.Controllers.Account
{
    [Permission]
    [AccountingModule]
    [ShowNotification]
    // Old controller name ILCPaymentEntry
    public class LCPVController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: LCPV
        public ActionResult LCPVView()
        {
            ViewBag.ILC = LoadComboBox.LoadAllILC();
            ViewBag.GlAccount = LoadComboBox.LoadAllAccount();
            ViewBag.CashBank = LoadComboBox.LoadAccount();
            ViewBag.Account = LoadComboBox.LoadAllDebitAccount();
            ViewBag.SubLedger = LoadComboBox.LoadAllSubLedgerAccount(null);
            return View();
        }

        public ActionResult SaveLCPV(ILCP ilcp, List<ILCPLine> addedItems1, List<LCPVSubLedgerLine> addedItems)
        {
            var ScreenID = context.Screens.FirstOrDefault(x => x.Name == "Import LC Payment").ScreenID;
            var IsExpired = context.ScreenEntryLocks.FirstOrDefault(x => x.ScreenID == ScreenID);

            if (ilcp.clmDate > IsExpired.SELDate)
            {

                try
                {

                    string ilcpNo = "";
                    string yearLastTwoPart = ilcp.clmDate.Year.ToString().Substring(2, 2).ToString();
                    var noOfInvoice = context.ILCP.Count();
                    if (noOfInvoice > 0)
                    {
                        var lastInvoice = context.ILCP.ToList().LastOrDefault(x => x.clmILCPNO.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                        if (lastInvoice == null)
                        {
                            ilcpNo = yearLastTwoPart + "00001";
                        }
                        else
                        {
                            ilcpNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.clmILCPNO.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                        }

                    }
                    else
                    {
                        ilcpNo = yearLastTwoPart + "00001";
                    }

                    ilcp.clmILCPNO = Convert.ToInt32(ilcpNo);

                    ilcp.clmDate = ilcp.clmDate.Add(DateTime.Now.TimeOfDay);

                    //  for line item

                    addedItems1.ForEach(x =>
                    {
                        x.clmILCPNo = ilcp.clmILCPNO;
                        context.ILCPLine.Add(x);
                    });

                    //for sub ledger line Item

                    addedItems.ForEach(x =>
                    {
                        x.LCPVNo = ilcp.clmILCPNO;
                        context.LCPVSubLedgerLine.Add(x);
                    });

                    context.ILCP.Add(ilcp);
                    AccountModuleBridge.InsertUpdateFromLCPV(context, ilcp, addedItems1);
                    LcBalanceCalculationBridge.InsertUpdateFromLCPV(ref context, ilcp, addedItems1);
                    UserLog.SaveUserLog(ref context, new UserLog(ilcp.clmILCPNO.ToString(), DateTime.Now, "LCPV", ScreenAction.Save));
                    context.SaveChanges();
                    return Json(new { ilcp }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }


            }
            else
            {
                return Json(3, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateLCPV(ILCP ilcp, List<ILCPLine> addedItems1, List<LCPVSubLedgerLine> addedItems)
        {
            var ScreenID = context.Screens.FirstOrDefault(x => x.Name == "Import LC Payment").ScreenID;
            var IsExpired = context.ScreenEntryLocks.FirstOrDefault(x => x.ScreenID == ScreenID);

            if (ilcp.clmDate > IsExpired.SELDate)
            {

                try
                {

                    var ilcpExist = context.ILCP.FirstOrDefault(x => x.clmILCPNO == ilcp.clmILCPNO);
                    context.ILCP.Remove(ilcpExist);

                    var ilcpLineExist = context.ILCPLine.Where(x => x.clmILCPNo == ilcp.clmILCPNO).ToList();
                    ilcpLineExist.ForEach(x =>
                    {
                        context.ILCPLine.Remove(x);
                    });

                    var ilcbSubLineExist = context.LCPVSubLedgerLine.Where(x => x.LCPVNo == ilcp.clmILCPNO).ToList();
                    ilcbSubLineExist.ForEach(x =>
                    {
                        x.LCPVNo = ilcp.clmILCPNO;
                        context.LCPVSubLedgerLine.Remove(x);
                    });



                    ilcp.clmDate = ilcp.clmDate.Add(DateTime.Now.TimeOfDay);

                    addedItems1.ForEach(x =>
                    {
                        x.clmILCPNo = ilcp.clmILCPNO;
                        context.ILCPLine.Add(x);
                    });

                    //for sub ledger line Item



                    addedItems.ForEach(x =>
                    {
                        x.LCPVNo = ilcp.clmILCPNO;
                        context.LCPVSubLedgerLine.Add(x);
                    });

                    context.ILCP.Add(ilcp);
                    AccountModuleBridge.InsertUpdateFromLCPV(context, ilcp, addedItems1);

                    LcBalanceCalculationBridge.InsertUpdateFromLCPV(ref context, ilcp, addedItems1);
                    UserLog.SaveUserLog(ref context, new UserLog(ilcp.clmILCPNO.ToString(), DateTime.Now, "LCPV", ScreenAction.Update));
                    context.SaveChanges();
                    return Json(new { ilcp }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }

                
            }
            else
            {
                return Json(3, JsonRequestBehavior.AllowGet);
            }
        }
        
        public ActionResult GetLcInfo(int liId)
        {
            if (liId != 0)
            {
                var lcDetails = (from l in context.ImportLC
                                 join s in context.Supplier on l.SupplierId equals s.Id
                                 where l.ILCId == liId
                                 select new { ILCID = l.ILCId, ILCNo = l.ILCNO, AltILCNo = l.IALCNO, Supplier = s.SupplierName }).FirstOrDefault();

                if (lcDetails != null)
                {

                    return Json(new { lcDetails = lcDetails, PreviousBill = 0 }, JsonRequestBehavior.AllowGet);


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

        public ActionResult GetILCPById(int id)
        {
            var ilcpItem = context.ILCP.FirstOrDefault(x => x.clmILCPNO == id);

            if (ilcpItem != null)
            {
                var ilcpLineItem = context.ILCPLine.Where(x => x.clmILCPNo == id).ToList().Select(x =>
                {
                    var item = context.ILCPLine.FirstOrDefault(y => y.clmILCPNo == x.clmILCPNo && y.DebitAccID == x.DebitAccID);
                    var chartOfAcc = context.ChartOfAccount.FirstOrDefault(z => z.Id == x.DebitAccID);
                    x.DebitAccName = chartOfAcc.Name;
                    return x;
                }).ToList();

                var ilcbSubledgerLineItem = context.LCPVSubLedgerLine.Where(x => x.LCPVNo == id).ToList().Select(x =>
                {
                    var item = context.LCPVSubLedgerLine.FirstOrDefault(y => y.LCPVNo == x.LCPVNo && y.SubLedgerID == x.SubLedgerID);
                    var subLedger = context.ImportCostItems.FirstOrDefault(z => z.SlnNo == x.SubLedgerID);
                    x.SubLedgerName = subLedger.CostItem;
                    return x;
                }).ToList();
                return Json(new { ilcpItem, ilcpLineItem, ilcbSubledgerLineItem }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteILCPByid(int id)
        {
            var ilcpExist = context.ILCP.FirstOrDefault(x => x.clmILCPNO == id);
            if (ilcpExist != null)
            {
                context.ILCP.Remove(ilcpExist);
                context.ILCPLine.Where(x => x.clmILCPNo == id).ToList().ForEach(x =>
                {
                    context.ILCPLine.Remove(x);
                });

                context.LCPVSubLedgerLine.Where(x => x.LCPVNo == id).ToList().ForEach(x =>
                {
                    context.LCPVSubLedgerLine.Remove(x);
                });
                
                AccountModuleBridge.DeleteFromLCPV(context, ilcpExist);
                LcBalanceCalculationBridge.DeleteFromILCPayment(ref context, ilcpExist);
                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "LCPV", ScreenAction.Delete));
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult TestCss()
        {
            return View();
        }

        public FileResult GetILCPPreview(int clmILCPNo)
        {
            Session["ReportName"] = "ILCPPreview";

            ReportParmPersister param = new ReportParmPersister();
            var loginName = context.UserLog.FirstOrDefault(x => x.TrnasId == clmILCPNo.ToString() && x.ScreenName == "ILCP" && x.Action == "Save").LogInName;
            param.PostedBy = context.Employees.FirstOrDefault(x => x.LogInUserName == loginName).Name;
            Session["ILCPPreview"] = param;

            string sql = string.Format("exec PreviewILCP '" + clmILCPNo + "'");
            var items = context.Database.SqlQuery<ILCPPreviewReport>(sql).ToList();
            if (items.Count == 0)
            {
                ILCPPreviewReport report = new ILCPPreviewReport();
                items.Add(report);
            }

            ILCPPreviewR rd = ShowReport(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");

        }

        public ILCPPreviewR ShowReport(ReportParmPersister persister, List<ILCPPreviewReport> data)
        {
            ILCPPreviewR iLCPPreviewR = new ILCPPreviewR();

            iLCPPreviewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\ILCPPreviewR.rpt");
            iLCPPreviewR.Load(APPPATH);
            iLCPPreviewR.SetDataSource(data);
            iLCPPreviewR.SetParameterValue("PostedBy", persister.PostedBy);
            iLCPPreviewR.SetParameterValue("CompName", persister.CompName);
            iLCPPreviewR.SetParameterValue("CompAddress", persister.CompAddress);
            return iLCPPreviewR;
        }
    }
}