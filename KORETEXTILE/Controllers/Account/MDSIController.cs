using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.AccountModule;
using BEEERP.Models.Bridge;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Account
{
    [ShowNotification]
    public class MDSIController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: MDSI
        public ActionResult MDSIEntry()
        {
            string MDSINO = "";
            string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
            if (context.MDSI.Count() > 0)
            {
                var lastRMC = context.MDSI.ToList().LastOrDefault(x => x.MDSINO.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                if (lastRMC == null)
                {
                    MDSINO = yearLastTwoPart + "00001";
                }
                else
                {
                    MDSINO = yearLastTwoPart + (Convert.ToInt32(lastRMC.MDSINO.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                }
            }
            else
            {
                MDSINO = yearLastTwoPart + "00001";
            }
            ViewBag.MDSINo = MDSINO;
            ViewBag.Depot = LoadComboBox.LoadAllDepot();
            ViewBag.Account = LoadComboBox.LoadExpenseAccount();
            ViewBag.Dealer = LoadComboBox.LoadAllCustomerByDepot(null);
            return View();
        }
        public ActionResult GetDealerByDepot(int id)
        {
            return Json(LoadComboBox.LoadAllCustomerByDepot(id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveMDSI(MDSI MDSI, List<MDSILineItem> addedItems)
        {
            var ScreenID = context.Screens.FirstOrDefault(x => x.Name == "MDSI").ScreenID;
            var IsExpired = context.ScreenEntryLocks.FirstOrDefault(x => x.ScreenID == ScreenID);

            if (MDSI.MDSIDate > IsExpired.SELDate)

                try
                {
                    string MDSINO = "";
                    string yearLastTwoPart = MDSI.MDSIDate.Year.ToString().Substring(2, 2).ToString();
                    if (context.MDSI.Count() > 0)
                    {
                        var lastRMC = context.MDSI.ToList().LastOrDefault(x => x.MDSINO.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                        if (lastRMC == null)
                        {
                            MDSINO = yearLastTwoPart + "00001";
                        }
                        else
                        {
                            MDSINO = yearLastTwoPart + (Convert.ToInt32(lastRMC.MDSINO.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                        }
                    }
                    else
                    {
                        MDSINO = yearLastTwoPart + "00001";
                    }

                    MDSI.MDSINO = Convert.ToInt32(MDSINO);
                    context.MDSI.Add(MDSI);
                    addedItems.ForEach(x =>
                    {
                        x.MDSINO = MDSI.MDSINO; context.MDSILineItem.Add(x);
                    });
                    //InventoryTransactionBridge.InsertFromFGProduction(ref context, fGProductionLineItem, fGProduction);
                    AccountModuleBridge.InsertUpdateFromMDSI(ref context, MDSI, addedItems);
                    DealerIncentiveBalCalculationBridge.InsertUpdateFromMDSI(ref context, MDSI, addedItems);
                    UserLog.SaveUserLog(ref context, new UserLog(MDSI.MDSINO.ToString(), DateTime.Now, "MDSI", ScreenAction.Save));
                    context.SaveChanges();
                    return Json(new { Message = 1, mdsi = MDSI.MDSINO }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                }
            else
            {
                return Json(3, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult GetMDSIByMDSINo(int MDSINO)
        {
            if (MDSINO == 0)
            {
                return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var mdsi = context.MDSI.FirstOrDefault(x => x.MDSINO == MDSINO);
                if (mdsi == null)
                {
                    return Json(0 , JsonRequestBehavior.AllowGet);
                }
                else
                {

                    var line = context.MDSILineItem.Where(x => x.MDSINO == MDSINO).ToList();
                    line.ForEach(x =>
                    {
                        var item = LoadComboBox.GetCustInfo().FirstOrDefault(y => y.Id == x.DealerID);
                        x.DealerName = item.CustomerName;

                    });
                    return Json(new { Message = 1, MDSI = mdsi, lineItem = line }, JsonRequestBehavior.AllowGet);
                }
            }
        }

            public ActionResult UpdateMDSI(MDSI MDSI, List<MDSILineItem> addedItems)
            {

                try
                {
                var isExist = context.MDSI.FirstOrDefault(x => x.MDSINO == MDSI.MDSINO);
                if(isExist != null)
                {
                    context.MDSI.Remove(isExist);
                    var isLineExist = context.MDSILineItem.Where(x => x.MDSINO == MDSI.MDSINO).ToList();
                    isLineExist.ForEach(x =>
                    {
                        context.MDSILineItem.Remove(x);
                    });
                }
                    context.MDSI.Add(MDSI);
                    addedItems.ForEach(x => {
                        x.MDSINO = MDSI.MDSINO; context.MDSILineItem.Add(x);
                    });
                    //InventoryTransactionBridge.InsertFromFGProduction(ref context, fGProductionLineItem, fGProduction);
                    AccountModuleBridge.InsertUpdateFromMDSI(ref context, MDSI, addedItems);
                    DealerIncentiveBalCalculationBridge.InsertUpdateFromMDSI(ref context, MDSI, addedItems);
                    UserLog.SaveUserLog(ref context, new UserLog(MDSI.MDSINO.ToString(), DateTime.Now, "MDSI", ScreenAction.Update));
                    context.SaveChanges();
                    return Json(new { Message = 1, MDSI = MDSI.MDSINO }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                }

            
        }

        public ActionResult DeleteMDSI(MDSI MDSI)
        {
            try
            {

            
            var isExist = context.MDSI.FirstOrDefault(x => x.MDSINO == MDSI.MDSINO);
            if(isExist != null)
            {
                context.MDSI.Remove(isExist);
                var isLineExist = context.MDSILineItem.Where(x => x.MDSINO == MDSI.MDSINO).ToList();
                if(isLineExist.Count > 0)
                {
                    isLineExist.ForEach(x =>
                    {
                        context.MDSILineItem.Remove(x);
                    });
                }

                DealerIncentiveBalCalculationBridge.DeleteFromMDSI(ref context, MDSI);
                AccountModuleBridge.DeleteFromMDSI(ref context, MDSI);
                UserLog.SaveUserLog(ref context, new UserLog(MDSI.MDSINO.ToString(), DateTime.Now, "MDSI", ScreenAction.Delete));
                context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            }
            catch (Exception ex)
            {
                return Json( 2, JsonRequestBehavior.AllowGet);
            }
        }



        public FileResult GetPreviewMDSI(int MDSINO)
        {
            Session["reportName"] = "Monthly Dealer Sales Incentive Details of ID - " + MDSINO;

            ReportParmPersister param = new ReportParmPersister();
            var data = (from e in context.Employees.AsEnumerable()
                        join u in context.UserLog.AsEnumerable() on e.LogInUserName equals u.LogInName
                        where u.ScreenName == "MDSI" && u.Action == "Save" && u.TrnasId == MDSINO.ToString()
                        select e.Name).ToList();
            if (data.Count > 0)
            {
                param.PostedBy = data.FirstOrDefault().ToString();
            }
            else
            {
                param.PostedBy = "";
            }
            //param.PostedBy = (from e in context.Employees.AsEnumerable()
            //                  join u in context.UserLog.AsEnumerable() on e.LogInUserName equals u.LogInName
            //                  where u.ScreenName == "Raw Material Consumption" && u.Action == "Save" && u.TrnasId == RMCNo.ToString()
            //                  select e.Name).FirstOrDefault();
            string sql = string.Format("exec PreviewMDSI " + MDSINO);
            var items = context.Database.SqlQuery<PreviewMDSI>(sql).ToList();
            if (items.Count == 0)
            {
                PreviewMDSI report = new PreviewMDSI();
                items.Add(report);
            }

            PreviewMDSIR rd = ShowReport(items, param);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");

        }

        public PreviewMDSIR ShowReport(List<PreviewMDSI> data, ReportParmPersister param)
        {
            PreviewMDSIR RMC = new PreviewMDSIR();

            RMC.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\PreviewMDSIR.rpt");
            RMC.Load(APPPATH);
            RMC.SetDataSource(data);
            RMC.SetParameterValue("postedBy", param.PostedBy);
            //RMC.SetParameterValue("compName", persister.CompName);
            //RMC.SetParameterValue("compAddress", persister.CompAddress);
            RMC.SetParameterValue("ReportName", Session["reportName"]);
            return RMC;
        }
    }
}