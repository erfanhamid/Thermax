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
    public class DealerSalesIncentiveAdjustmentController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: DealerSalesIncentiveAdjustment
        public ActionResult DSIAEntry()
        {
            string DSIANo = "";
            string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
            if (context.DealerSalesIncentiveAdjustment.Count() > 0)
            {
                var lastRMC = context.DealerSalesIncentiveAdjustment.ToList().LastOrDefault(x => x.DSIANo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                if (lastRMC == null)
                {
                    DSIANo = yearLastTwoPart + "00001";
                }
                else
                {
                    DSIANo = yearLastTwoPart + (Convert.ToInt32(lastRMC.DSIANo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                }
            }
            else
            {
                DSIANo = yearLastTwoPart + "00001";
            }
            ViewBag.DSIANo = DSIANo;
            ViewBag.Depot = LoadComboBox.LoadAllDepot();
            ViewBag.Account = LoadComboBox.LoadExpenseAccount();
            ViewBag.Dealer = LoadComboBox.LoadAllCustomerByDepot(null);
            return View();
        }

        public ActionResult GetDealerBalance(int id)
        {
            if(id != 0 && id.ToString() != "")
            {
                decimal balance = 0;
                var data = context.DealerIncentiveBalanceCalculation.Where(x => x.DealerID == id).ToList();
                if(data.Count > 0)
                {
                     balance = data.Sum(x => x.Amount);
                }
                else
                {
                    balance = 0;
                }

                return Json(balance, JsonRequestBehavior.AllowGet);
            }
            else{
                return Json(0, JsonRequestBehavior.AllowGet);
            }
           
            
        }

        public ActionResult SaveDSIA(DealerSalesIncentiveAdjustment DSIA, List<DSIAdjustmentLineItem> addedItems)
        {
            var ScreenID = context.Screens.FirstOrDefault(x => x.Name == "DSI Adjustment").ScreenID;
            var IsExpired = context.ScreenEntryLocks.FirstOrDefault(x => x.ScreenID == ScreenID);

            if (DSIA.DSIADate > IsExpired.SELDate)
            {


                try
                {
                    string DSIANo = "";
                    string yearLastTwoPart = DSIA.DSIADate.Year.ToString().Substring(2, 2).ToString();
                    if (context.DealerSalesIncentiveAdjustment.Count() > 0)
                    {
                        var lastRMC = context.DealerSalesIncentiveAdjustment.ToList().LastOrDefault(x => x.DSIANo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                        if (lastRMC == null)
                        {
                            DSIANo = yearLastTwoPart + "00001";
                        }
                        else
                        {
                            DSIANo = yearLastTwoPart + (Convert.ToInt32(lastRMC.DSIANo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                        }
                    }
                    else
                    {
                        DSIANo = yearLastTwoPart + "00001";
                    }

                    DSIA.DSIANo = Convert.ToInt32(DSIANo);
                    context.DealerSalesIncentiveAdjustment.Add(DSIA);
                    addedItems.ForEach(x =>
                    {
                        x.DSIANo = DSIA.DSIANo; context.DSIAdjustmentLineItem.Add(x);
                    });

                    AccountModuleBridge.InsertUpdateFromDSIAdj(ref context, DSIA, addedItems);
                    DealerIncentiveBalCalculationBridge.InsertUpdateFromDSIAdj(ref context, DSIA, addedItems);
                    CustomerCalculationBridge.InsertFromDSIAdj(ref context, DSIA, addedItems);
                    UserLog.SaveUserLog(ref context, new UserLog(DSIA.DSIANo.ToString(), DateTime.Now, "DSIAdj", ScreenAction.Save));
                    context.SaveChanges();
                    return Json(new { Message = 1, DSIA = DSIA.DSIANo }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(3, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult GetDSIAByDSIANo(int DSIANo)
        {
            if (DSIANo == 0)
            {
                return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var mdsi = context.DealerSalesIncentiveAdjustment.FirstOrDefault(x => x.DSIANo == DSIANo);
                if (mdsi == null)
                {
                    return Json( 0 , JsonRequestBehavior.AllowGet);
                }
                else
                {

                    var line = context.DSIAdjustmentLineItem.Where(x => x.DSIANo == DSIANo).ToList();
                    line.ForEach(x =>
                    {
                        var item = LoadComboBox.GetCustInfo().FirstOrDefault(y => y.Id == x.DealerID);
                        x.DealerName = item.CustomerName;

                    });
                    return Json(new { Message = 1, DSIA = mdsi, lineItem = line }, JsonRequestBehavior.AllowGet);
                }
            }
        }


        public ActionResult UpdateDSIA(DealerSalesIncentiveAdjustment DSIA, List<DSIAdjustmentLineItem> addedItems)
        {

            try
            {
                var isExist = context.DealerSalesIncentiveAdjustment.FirstOrDefault(x => x.DSIANo == DSIA.DSIANo);
                if (isExist != null)
                {
                    context.DealerSalesIncentiveAdjustment.Remove(isExist);
                    var isLineExist = context.DSIAdjustmentLineItem.Where(x => x.DSIANo == DSIA.DSIANo).ToList();
                    isLineExist.ForEach(x =>
                    {
                        context.DSIAdjustmentLineItem.Remove(x);
                    });
                }
                context.DealerSalesIncentiveAdjustment.Add(DSIA);
                addedItems.ForEach(x => {
                    x.DSIANo = DSIA.DSIANo; context.DSIAdjustmentLineItem.Add(x);
                });
                AccountModuleBridge.InsertUpdateFromDSIAdj(ref context, DSIA, addedItems);
                DealerIncentiveBalCalculationBridge.InsertUpdateFromDSIAdj(ref context, DSIA, addedItems);
                CustomerCalculationBridge.InsertFromDSIAdj(ref context, DSIA, addedItems);
                UserLog.SaveUserLog(ref context, new UserLog(DSIA.DSIANo.ToString(), DateTime.Now, "DSIAdj", ScreenAction.Update));
                context.SaveChanges();
                return Json(new { Message = 1, DSIA = DSIA.DSIANo }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult DeleteDSIA(DealerSalesIncentiveAdjustment DSIA)
        {
            try
            {


                var isExist = context.DealerSalesIncentiveAdjustment.FirstOrDefault(x => x.DSIANo == DSIA.DSIANo);
                if (isExist != null)
                {
                    context.DealerSalesIncentiveAdjustment.Remove(isExist);
                    var isLineExist = context.DSIAdjustmentLineItem.Where(x => x.DSIANo == DSIA.DSIANo).ToList();
                    if (isLineExist.Count > 0)
                    {
                        isLineExist.ForEach(x =>
                        {
                            context.DSIAdjustmentLineItem.Remove(x);
                        });
                    }

                    DealerIncentiveBalCalculationBridge.DeleteFromDSIAdj(ref context, DSIA);
                    AccountModuleBridge.DeleteFromDSIAdj(ref context, DSIA);
                    CustomerCalculationBridge.DeleteFromDSIAdj(ref context, DSIA);
                    UserLog.SaveUserLog(ref context, new UserLog(DSIA.DSIANo.ToString(), DateTime.Now, "DSIAdj", ScreenAction.Delete));
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
                return Json(2, JsonRequestBehavior.AllowGet);
            }
        }

        public FileResult GetPreviewDSIAdj(int DSIANo)
        {
            Session["reportName"] = " Dealer Sales Incentive Adjustment Details of ID - " + DSIANo;

            ReportParmPersister param = new ReportParmPersister();
            var data = (from e in context.Employees.AsEnumerable()
                        join u in context.UserLog.AsEnumerable() on e.LogInUserName equals u.LogInName
                        where u.ScreenName == "DSIAdj" && u.Action == "Save" && u.TrnasId == DSIANo.ToString()
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
            string sql = string.Format("exec PreViewDSIAdj " + DSIANo);
            var items = context.Database.SqlQuery<PreviewDSIAdj>(sql).ToList();
            if (items.Count == 0)
            {
                PreviewDSIAdj report = new PreviewDSIAdj();
                items.Add(report);
            }

            PreviewDSIAdjR rd = ShowReport(items, param);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");

        }

        public PreviewDSIAdjR ShowReport(List<PreviewDSIAdj> data, ReportParmPersister param)
        {
            PreviewDSIAdjR RMC = new PreviewDSIAdjR();

            RMC.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\PreviewDSIAdjR.rpt");
            RMC.Load(APPPATH);
            RMC.SetDataSource(data);
            RMC.SetParameterValue("PostedBy", param.PostedBy);
            //RMC.SetParameterValue("compName", persister.CompName);
            //RMC.SetParameterValue("compAddress", persister.CompAddress);
            RMC.SetParameterValue("ReportName", Session["reportName"]);
            return RMC;
        }
    }
}