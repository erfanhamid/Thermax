using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.Database;
using BEEERP.Models.AccountModule;
using BEEERP.Models.CommonInformation;
using System.Data.Entity;
using BEEERP.Models.Log;
using BEEERP.Models.Bridge;
using BEEERP.Models.Notification;
using Microsoft.AspNet.Identity;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using System.IO;

namespace BEEERP.Controllers.Account
{
    [ShowNotification]
   // [Authorize(Roles = "ProAdmin,ProOperator,ProViewer,ProApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    public class FundTransferVoucherController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: FundTransferVoucher
        public ActionResult FundTransferVoucher(/*string type = "", int ftvno = 0*/)
        {
            //string id = "";
            //string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
            //var noOfFundTranVoucher = context.FundTransferVouchers.Count();
            //if (noOfFundTranVoucher > 0)
            //{
            //    var lastFund = context.FundTransferVouchers.ToList().LastOrDefault(x => x.TransferId.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
            //    id = yearLastTwoPart + (Convert.ToInt32(lastFund.TransferId.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
            //}
            //else
            //{
            //    id = yearLastTwoPart + "00001";
            //}
            //ViewBag.Id = Convert.ToInt32(id);
            ViewBag.banks = LoadComboBox.LoadAllCashBankAccount();

            //if (ftvno != 0)
            //{
            //    var notificationE = context.Notification.FirstOrDefault(x => x.Type == "FTVEntry" && x.TransactionNo == ftvno.ToString() && x.NotificationDetails == "This Fund Transfer Voucher is approved.");
            //    ViewBag.FTVNo = ftvno;
            //    ViewBag.IsNotify = 1;
            //    if (notificationE != null)
            //    {
            //        ViewBag.Approve = 1;
            //    }
            //    else
            //    {
            //        ViewBag.Approve = 0;
            //    }
            //}
            //else
            //{
            //    ViewBag.Approve = 0;
            //    ViewBag.IsNotify = 0;
            //}

            return View();
        }
        public ActionResult SaveFundTransVoucher(FundTransferVoucher fundTrancferVoucher)
        {

            var ScreenID = context.Screens.FirstOrDefault(x => x.Name == "FT Voucher").ScreenID;
            var IsExpired = context.ScreenEntryLocks.FirstOrDefault(x => x.ScreenID == ScreenID);

            if (fundTrancferVoucher.Date > IsExpired.SELDate)
            {
                try
                {
                    string id = "";
                    string yearLastTwoPart = fundTrancferVoucher.Date.Year.ToString().Substring(2, 2).ToString();
                    var noOfFundTranVoucher = context.FundTransferVouchers.Count();
                    if (noOfFundTranVoucher > 0)
                    {
                        var lastFund = context.FundTransferVouchers.ToList().LastOrDefault(x => x.TransferId.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                       
                        if (lastFund == null)
                        {
                            id = yearLastTwoPart + "00001";
                        }
                        else
                        {
                            id = yearLastTwoPart + (Convert.ToInt32(lastFund.TransferId.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                        }
                    }
                    else
                    {
                        id = yearLastTwoPart + "00001";
                    }
                    fundTrancferVoucher.TransferId = Convert.ToInt32(id);
                    fundTrancferVoucher.Date = fundTrancferVoucher.Date.Add(DateTime.Now.TimeOfDay);
                    context.FundTransferVouchers.Add(fundTrancferVoucher);

                    //UserNotification notification = new UserNotification();
                    //notification.UserId = User.Identity.GetUserId();
                    //notification.Type = "FTVEntry";
                    //notification.TransactionNo = fundTrancferVoucher.TransferId.ToString();
                    //notification.NotificationHead = "New Fund Transfer Voucher";
                    //notification.NotificationDetails = "This Fund Transfer Voucher is pending for approved.";
                    //notification.PostingDate = DateTime.Now;
                    //notification.BranchId = 0;

                    //context.Notification.Add(notification);

                    AccountModuleBridge.InsertUpdateFromFTVVoucher(ref context, fundTrancferVoucher);
                    UserLog.SaveUserLog(ref context, new UserLog(fundTrancferVoucher.TransferId.ToString(), fundTrancferVoucher.Date, "FTV", ScreenAction.Save));
                    context.SaveChanges();
                    return Json(new { Message = "1", FundTrancferVoucher = fundTrancferVoucher }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { Message = 0, JsonRequestBehavior.AllowGet });
                }
            }
            else
            {
                return Json(3, JsonRequestBehavior.AllowGet);
            }
            
        }

        //public ActionResult TransectionEffect(FundTransferVoucher fundTrancferVoucher)
        //{
        //    var ftV = context.FundTransferVouchers.AsNoTracking().FirstOrDefault(x => x.TransferId == fundTrancferVoucher.TransferId);

        //    if (ftV != null)
        //    {
        //        var notificationE = context.Notification.FirstOrDefault(x => x.Type == "FTVEntry" && x.TransactionNo == ftV.TransferId.ToString());
        //        if (notificationE != null)
        //        {
        //            UserNotification notification = new UserNotification();
        //            notification.UserId = User.Identity.GetUserId();
        //            notification.Type = "FTVEntry";
        //            notification.TransactionNo = fundTrancferVoucher.TransferId.ToString();
        //            notification.NotificationHead = "New Fund Transfer Voucher";
        //            notification.NotificationDetails = "This Fund Transfer Voucher is approved.";
        //            notification.PostingDate = DateTime.Now;
        //            notification.BranchId = 0;

        //            context.Notification.Add(notification);

        //            //notificationE.NotificationDetails = "This Fund Transfer Voucher is approved.";
        //            //notificationE.UserId = User.Identity.GetUserId();
        //            //context.Entry<UserNotification>(notificationE).State = EntityState.Modified;

        //            context.ApprovalLog.Where(x => x.NotificationId == notificationE.Id).ToList().ForEach(x => { x.IsApproved = true; context.Entry<ApprovalLog>(x).State = EntityState.Modified; });

        //            fundTrancferVoucher.Date = fundTrancferVoucher.Date.Add(DateTime.Now.TimeOfDay);
        //            context.Entry<FundTransferVoucher>(fundTrancferVoucher).State = EntityState.Modified;
        //        }

        //        //working
        //        AccountModuleBridge.InsertUpdateFromFTVVoucher(ref context, fundTrancferVoucher);
        //        UserLog.SaveUserLog(ref context, new UserLog(fundTrancferVoucher.TransferId.ToString(), DateTime.Now, "FundTransferVoucher", ScreenAction.Approve));
        //        context.SaveChanges();
        //        return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
        //    }

        //    else
        //    {
        //        return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        public ActionResult GetFundTransVById(int ftvid)
        {
            if (ftvid == 0)
            {
                return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var funfTrans = context.FundTransferVouchers.FirstOrDefault(m => m.TransferId == ftvid);
                if (funfTrans == null)
                {
                    return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var fundtransvouchers = context.FundTransferVouchers.FirstOrDefault(m => m.TransferId == funfTrans.TransferId);                    
                    return Json(new { Message = 1, fundtransvouchers = fundtransvouchers }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult UpdateFundTransVoucher(FundTransferVoucher fundTransferVoucher)
        {
            try
            {
                var isExist = context.FundTransferVouchers.AsNoTracking().FirstOrDefault(x => x.TransferId == fundTransferVoucher.TransferId);
                if (isExist == null)
                {
                    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //var transectionE = context.Transection.Where(x => x.DocID == fundTransferVoucher.TransferId && x.DocType == "FTV").ToList();
                    //if (transectionE.Count() == 0)
                    //{
                        fundTransferVoucher.Date = fundTransferVoucher.Date.Add(DateTime.Now.TimeOfDay);
                        context.Entry<FundTransferVoucher>(fundTransferVoucher).State = EntityState.Modified;
                        AccountModuleBridge.InsertUpdateFromFTVVoucher(ref context, fundTransferVoucher);
                        UserLog.SaveUserLog(ref context, new UserLog(fundTransferVoucher.TransferId.ToString(), fundTransferVoucher.Date, "FTV", ScreenAction.Update));
                        context.SaveChanges();
                    //}
                    //else
                    //{
                    //    return Json(new { Message = 5 }, JsonRequestBehavior.AllowGet);
                    //}
                        
                }
                return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteFundTransVoucher(int id)  
        {
            try
            {
                var isExist = context.FundTransferVouchers.FirstOrDefault(x => x.TransferId == id);
                if (isExist == null)
                {
                    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //var transectionE = context.Transection.Where(x => x.DocID == id && x.DocType == "FTV").ToList();
                    //if (transectionE.Count() == 0)
                    //{
                        context.FundTransferVouchers.Remove(isExist);
                        AccountModuleBridge.DeleteFromFTVVoucher(ref context, isExist);
                        UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), isExist.Date, "FTV", ScreenAction.Delete));
                        context.SaveChanges();
                        return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
                    //}
                    //else
                    //{
                    //    return Json(new { Message = 5 }, JsonRequestBehavior.AllowGet);
                    //}
                }
                

            }
            catch (Exception ex)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }
        }


        public FileResult GetFundTransferVoucherPreview(int transferId)
        {
            Session["ReportName"] = "FTVPreviewR";
            ReportParmPersister param = new ReportParmPersister();

            var ftv = context.FundTransferVouchers.FirstOrDefault(x => x.TransferId == transferId);
            if (ftv != null)
            {
                param.SampleNo = ftv.TransferId;
                param.SampleDate = ftv.Date;
                if(ftv.RefNo != null)
                {
                    param.RefNo = ftv.RefNo;
                }
                else
                {
                    param.RefNo = "";
                }
                
            }
            else
            {
                param.SampleNo = 0;
                param.RefNo = "";
            }
            param.ShiftFrom = context.ChartOfAccount.FirstOrDefault(x => x.Id == ftv.TransFrom).Name;
            param.ShiftTo = context.ChartOfAccount.FirstOrDefault(x => x.Id == ftv.TransTo).Name;

            //param.PostedBy = (from e in context.Employees.AsEnumerable()
            //                  join u in context.UserLog.AsEnumerable() on e.LogInUserName equals u.LogInName
            //                  where u.ScreenName == "FundTransferVoucher" && u.Action == "Save" && u.TrnasId == ftv.TransferId.ToString()
            //                  select e.Name).FirstOrDefault();

            //param.ApprovedBy = (from e in context.Employees.AsEnumerable()
            //                    join u in context.UserLog.AsEnumerable() on e.LogInUserName equals u.LogInName
            //                    where u.ScreenName == "FundTransferVoucher" && u.Action == "Approve" && u.TrnasId == ftv.TransferId.ToString()
            //                    select e.Name).FirstOrDefault();

            if (param.ApprovedBy == null)
            {
                param.ApprovedBy = "";
            }

            string sql = string.Format("exec PreviewFundTransferVoucher '" + transferId + "'");
            var items = context.Database.SqlQuery<FTVPreviewReport>(sql).ToList();
            if (items.Count == 0)
            {
                FTVPreviewReport report = new FTVPreviewReport();
                items.Add(report);
            }

            FTVPreviewR rd = ShowReport(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");

        }

        public FTVPreviewR ShowReport(ReportParmPersister persister, List<FTVPreviewReport> data)
        {
            FTVPreviewR ftvPreviewR = new FTVPreviewR();

            ftvPreviewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\FTVPreviewR.rpt");
            ftvPreviewR.Load(APPPATH);
            ftvPreviewR.SetDataSource(data);

            ftvPreviewR.SetParameterValue("ftvId", persister.SampleNo);
            ftvPreviewR.SetParameterValue("sampleDate", persister.SampleDate.ToString("dd-MM-yyyy"));
            ftvPreviewR.SetParameterValue("refNo", persister.RefNo);
            ftvPreviewR.SetParameterValue("tFrom", persister.ShiftFrom);
            ftvPreviewR.SetParameterValue("tTo", persister.ShiftTo);

            //ftvPreviewR.SetParameterValue("postedBy", persister.PostedBy);
            //ftvPreviewR.SetParameterValue("approvedBy", persister.ApprovedBy);

            //ftvPreviewR.SetParameterValue("compName", persister.CompName);
            //ftvPreviewR.SetParameterValue("compAddress", persister.CompAddress);
            //ftvPreviewR.SetParameterValue("compContact", persister.CompContact);
            //ftvPreviewR.SetParameterValue("compFax", persister.Fax);
            //ftvPreviewR.SetParameterValue("factAddress", persister.FactAddress);
            //ftvPreviewR.SetParameterValue("factContact", persister.FactContact);
            return ftvPreviewR;
        }

    }
}