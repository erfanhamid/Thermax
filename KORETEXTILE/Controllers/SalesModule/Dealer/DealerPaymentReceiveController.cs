using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.Bridge;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Data_Admin;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using BEEERP.Models.PaymentReceiveInfo;
using SMSService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.SalesModule.Dealer
{
    [ShowNotification]
    public class DealerPaymentReceiveController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: DealerPaymentReceive
        public ActionResult DPREntry()
        {
            string DPRNo = "";
            string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
            var noOfInvoice = context.ReceivePaymentInfos.Count();
            if (noOfInvoice > 0)
            {
                var lastInvoice = context.ReceivePaymentInfos.ToList().LastOrDefault(x => x.RPID.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                if(lastInvoice is null)
                {
                    DPRNo = yearLastTwoPart + "00001";
                }
                else
                {
                    DPRNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.RPID.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                }
                
            }
            else
            {
                DPRNo = yearLastTwoPart + "00001";
            }

            ViewBag.RPID = DPRNo;
            var DepotID = ScreenSessionData.GetEmployeeDepot();
            ViewBag.DepotId = DepotID;
            //ViewBag.Depot = LoadComboBox.LoadDepotByID(DepotID);
            ViewBag.Depot = LoadComboBox.LoadBranchInfo();
            ViewBag.Account = LoadComboBox.LoadDepot();
            return View();
        }

        public ActionResult GetAccountByDepot(int? id)
        {
            return Json( LoadComboBox.LoadDepot(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDealerInfoId(int id, int depot)
        {
            if(id != 0 && depot != 0)
            {
                var custinfo = context.Customers.FirstOrDefault(x => x.Id == id && x.DepotId == depot);
                if(custinfo != null)
                {
                var zoneinfo = context.TblZones.FirstOrDefault(x => x.ZoneId == custinfo.ZoneId);
                 var zone = "";
                if(zoneinfo  != null)
                {
                    zone = zoneinfo.ZoneName; 
                }
                var bal = context.CustomerBalanceCalculations.Where(x => x.CustomerID == id).ToList();
                decimal due = 0;
                if(bal.Count > 0)
                {
                   due = bal.Sum(x => x.Amount);
                }
                else
                {
                    due = 0;
                }
               
                return Json(new { dealer = custinfo, zone, due}, JsonRequestBehavior.AllowGet);
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
        public ActionResult SaveDPR(ReceivePaymentInfo dpr)
        {
            var ScreenID = context.Screens.FirstOrDefault(x => x.Name == "DPR").ScreenID;
            var IsExpired = context.ScreenEntryLocks.FirstOrDefault(x => x.ScreenID == ScreenID);
      
                if (dpr.TDate > IsExpired.SELDate)
                {


                    if (dpr != null)
                    {
                        var isExist = context.ReceivePaymentInfos.Where(x => x.RPID == dpr.RPID).ToList();
                        if (isExist.Count > 0)
                        {
                            return Json(2, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            string DPRNo = "";
                            string yearLastTwoPart = dpr.TDate.Year.ToString().Substring(2, 2).ToString();
                            var noOfInvoice = context.ReceivePaymentInfos.Count();
                            if (noOfInvoice > 0)
                            {
                                var lastInvoice = context.ReceivePaymentInfos.ToList().LastOrDefault(x => x.RPID.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                            if (lastInvoice is null)
                            {
                                DPRNo = yearLastTwoPart + "00001";
                            }
                            else
                            {
                                DPRNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.RPID.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                            }
                        }
                            else
                            {
                                DPRNo = yearLastTwoPart + "00001";
                            }
                            dpr.RPID = Convert.ToInt32(DPRNo);
                            dpr.Purpose = "DPR";

                            context.ReceivePaymentInfos.Add(dpr);
                            AccountModuleBridge.InsertUpdateFromDPR(ref context, dpr);
                            CustomerCalculationBridge.InsertUpdateFromDPR(ref context, dpr);
                            UserLog.SaveUserLog(ref context, new UserLog(dpr.RPID.ToString(), dpr.TDate, "DPR", ScreenAction.Save));
                            context.SaveChanges();
                        //For SMS
                        //var mobileNo = context.Customers.FirstOrDefault(x => x.Id == dpr.CustomerID).MobileNo;
                        //if (!string.IsNullOrEmpty(mobileNo) && mobileNo.Length == 14)
                        //{
                        //    mobileNo = mobileNo.Remove(0, 1);
                        //    string message = "Dear Dealer, We received your Payment, Amount " + dpr.NetAmount.ToString() + "tk. Your outstanding balance is " + SaleCommonInfo.GetDueByCustomerId(dpr.CustomerID) + "tk. Thank you. LOVELLO ICE CREAM.";

                        //    //SMS.SetParamValue("lovellosoft", "pacific@123", "LOVELLO", "Normal", "0", "0", mobileNo.Substring(0), message, "", "");
                        //    SMS.SetParamValue("lovellosoft", "pacific@123", "LOVELLO", "Normal", "0", "0", "8801712977476", message, "", "");
                        //    SMS.SendByHttp();
                        //}
                        
                        return Json(dpr.RPID, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(0, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(3, JsonRequestBehavior.AllowGet);
                }
            
            
        }
        public ActionResult GetDPR(int id)
        {
            if(id != 0 && id.ToString() != "")
            {
                var empDepot = ScreenSessionData.GetEmployeeDepot();
                BEEContext context = new BEEContext();
                var DPR = new ReceivePaymentInfo();
                if (User.IsInRole("Admin"))
                {
                     DPR = context.ReceivePaymentInfos.FirstOrDefault(x => x.RPID == id);
                }
                else
                {
                     DPR = context.ReceivePaymentInfos.FirstOrDefault(x => x.RPID == id && x.DepotID == empDepot);
                }
                
                if(DPR != null)
                {
                    return Json(DPR, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(2, JsonRequestBehavior.AllowGet);
                }
               
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateDPR(ReceivePaymentInfo dpr)
        {
            //var ScreenID = context.Screens.FirstOrDefault(x => x.Name == "DPR").ScreenID;
            //var IsExpired = context.ScreenEntryLocks.FirstOrDefault(x => x.ScreenID == ScreenID);

            //if (dpr.TDate > IsExpired.SELDate)
            //{
                if (dpr != null)
                {
                    var isExist = context.ReceivePaymentInfos.FirstOrDefault(x => x.RPID == dpr.RPID);
                    if (isExist != null)
                    {
                        context.ReceivePaymentInfos.Remove(isExist);
                    }

                    dpr.Purpose = "DPR";
                    context.ReceivePaymentInfos.Add(dpr);
                    UserLog.SaveUserLog(ref context, new UserLog(dpr.RPID.ToString(), dpr.TDate, "DPR", ScreenAction.Update));
                    AccountModuleBridge.InsertUpdateFromDPR(ref context, dpr);
                    CustomerCalculationBridge.InsertUpdateFromDPR(ref context, dpr);
                    context.SaveChanges();
                    return Json(dpr.RPID, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
            //else
            //{
            //    return Json(3, JsonRequestBehavior.AllowGet);
            //}

        

        public ActionResult DeleteDPR(int id)
        {

                if (id != 0 && id.ToString() != "")
                {
                    var isExist = context.ReceivePaymentInfos.FirstOrDefault(x => x.RPID == id);
                    if (isExist != null)
                    {
              
                    context.ReceivePaymentInfos.Remove(isExist);
                        AccountModuleBridge.DeleteFromDPR(ref context, isExist);
                        CustomerCalculationBridge.DeleteFromDPR(ref context, isExist);
                        UserLog.SaveUserLog(ref context, new UserLog(isExist.RPID.ToString(), isExist.TDate, "DPR", ScreenAction.Delete));
                        context.SaveChanges();
                        return Json(1, JsonRequestBehavior.AllowGet);
            
                    }
                    else
                    {
                        return Json(2, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            

        }

        public FileResult GetDPRPreview(int RPID)
        {
        
            string sql = string.Format("exec PreviewDPR " + RPID);
            var items = context.Database.SqlQuery<DPRPreviewReport>(sql).ToList();
            if (items.Count == 0)
            {
                DPRPreviewReport report = new DPRPreviewReport();
                items.Add(report);
            }

            DPRPreviewR rd = ShowReport(items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");

        }

        public DPRPreviewR ShowReport(List<DPRPreviewReport> data)
        {
            DPRPreviewR DPR = new DPRPreviewR();

            DPR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\DPRPreviewR.rpt");
            DPR.Load(APPPATH);
            DPR.SetDataSource(data);
            var comp = context.Database.SqlQuery<CompanyNameOne>("exec CompInfo").ToList();
            //RMC.SetParameterValue("postedBy", persister.PostedBy);
            DPR.SetParameterValue("compName", comp.FirstOrDefault().CompName);
            DPR.SetParameterValue("compAddress", comp.FirstOrDefault().CompAddress);
            //RMC.SetParameterValue("reportName", Session["reportName"]);
            return DPR;
        }
    }
}