using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.Database;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.AccountModule;
using BEEERP.Models.Notification;
using Microsoft.AspNet.Identity;
using BEEERP.Models.Log;
using System.Data.Entity;
using BEEERP.Models.Bridge;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using System.IO;

namespace BEEERP.Controllers.Account
{
    [ShowNotification]
    public class MRRefundController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: MRRefund
        public ActionResult MRRefund(string type = "", int mrrno = 0)   
        {
            ViewBag.PaymentAc = LoadComboBox.LoadReceiveAcc();

            if (mrrno != 0)
            {
                var notificationE = context.Notification.FirstOrDefault(x => x.Type == "MRREntry" && x.TransactionNo == mrrno.ToString() && x.NotificationDetails == "This Money Requisition Refund Voucher is approved.");
                ViewBag.MRRNo = mrrno;
                ViewBag.IsNotify = 1;
                if (notificationE != null)
                {
                    ViewBag.Approve = 1;
                }
                else
                {
                    ViewBag.Approve = 0;
                }
            }
            else
            {
                ViewBag.Approve = 0;
                ViewBag.IsNotify = 0;
            }

            return View();
        }

        public ActionResult GetEmployeeDetails(int empId)
        {
            var empExist = context.Employees.FirstOrDefault(x => x.Id == empId);
            
            if (empExist != null)
            {
                string EmployeeName = empExist.Name;
                string Designation = context.Designation.FirstOrDefault(x => x.Id == empExist.Designation).Name;
                decimal Balance = context.MoneyRequisitionBalanceCalculations.Where(x => x.EmpID == empId).Select(y => y.Amount).DefaultIfEmpty(0).Sum();

                return Json(new { EmployeeName, Designation, Balance }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveMRR(MoneyRequisitionRefund mrr, MoneyRequisitionBalanceCalculation mRBCalculation)
        {
            using (context)
            {


                string id = "";
                string yearLastTwoPart = mrr.Date.Year.ToString().Substring(2, 2).ToString();
                var noOfMRVoucher = context.MoneyRequisitionRefund.Count();

                if (noOfMRVoucher > 0)
                {
                    var lastMRV = context.MoneyRequisitionRefund.ToList().LastOrDefault(x => x.MRRNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                    if (lastMRV == null)
                    {
                        id = yearLastTwoPart + "00001";
                    }
                    else
                    {
                        id = yearLastTwoPart + (Convert.ToInt32(lastMRV.MRRNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                    }
                }
                else
                {
                    id = yearLastTwoPart + "00001";
                }


                //if (noOfMRVoucher > 0)
                //{
                //    var lastMRV = context.MoneyRequisitionRefund.ToList().LastOrDefault(x => x.MRRNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                //    id = yearLastTwoPart + (Convert.ToInt32(lastMRV.MRRNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');

                //}
                //else
                //{
                //    id = yearLastTwoPart + "00001";
                //}

                mrr.MRRNo = Convert.ToInt32(id);
                mrr.Date = mrr.Date.Add(DateTime.Now.TimeOfDay);

                UserNotification notification = new UserNotification();
                notification.UserId = User.Identity.GetUserId();
                notification.Type = "MRREntry";
                notification.TransactionNo = mrr.MRRNo.ToString();
                notification.NotificationHead = "New Money Requisition Refund Voucher.";
                notification.NotificationDetails = "This Money Requisition Refund Voucher is pending for approved.";
                notification.PostingDate = DateTime.Now;
                notification.BranchId = 1;

                context.Notification.Add(notification);

                try
                {
                    context.MoneyRequisitionRefund.Add(mrr);

                    mRBCalculation.DocID = mrr.MRRNo;
                    mRBCalculation.Date = mrr.Date;
                    mRBCalculation.DocumentType = "MRR";
                    mRBCalculation.Amount = mrr.Amount * (-1);
                    context.MoneyRequisitionBalanceCalculations.Add(mRBCalculation);
                    AccountModuleBridge.InsertUpdateFromMoneyRequisitionVouRefund(ref context,mrr);
                    UserLog.SaveUserLog(ref context, new UserLog(mrr.MRRNo.ToString(), DateTime.Now, "MRRVoucher", ScreenAction.Save));
                    context.SaveChanges();
                    return Json(mrr, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { Message = 0, JsonRequestBehavior.AllowGet });
                }
            }
        }

        public ActionResult TransectionEffect(MoneyRequisitionRefund mrr)
        {
            var mrrV = context.MoneyRequisitionRefund.AsNoTracking().FirstOrDefault(x => x.MRRNo == mrr.MRRNo);

            if (mrrV != null)
            {
                var notificationE = context.Notification.FirstOrDefault(x => x.Type == "MRREntry" && x.TransactionNo == mrrV.MRRNo.ToString());
                if (notificationE != null)
                {
                    UserNotification notification = new UserNotification();
                    notification.UserId = User.Identity.GetUserId();
                    notification.Type = "MRREntry";
                    notification.TransactionNo = mrr.MRRNo.ToString();
                    notification.NotificationHead = "New Money Requisition Refund Voucher.";
                    notification.NotificationDetails = "This Money Requisition Refund Voucher is approved.";
                    notification.PostingDate = DateTime.Now;
                    notification.BranchId = 1;

                    context.Notification.Add(notification);
                    //notificationE.NotificationDetails = "This Fund Transfer Voucher is approved.";
                    //notificationE.UserId = User.Identity.GetUserId();
                    //context.Entry<UserNotification>(notificationE).State = EntityState.Modified;

                    context.ApprovalLog.Where(x => x.NotificationId == notificationE.Id).ToList().ForEach(x => { x.IsApproved = true; context.Entry<ApprovalLog>(x).State = EntityState.Modified; });

                    mrr.Date = mrr.Date.Add(DateTime.Now.TimeOfDay);
                    context.Entry<MoneyRequisitionRefund>(mrr).State = EntityState.Modified;
                }
                
                AccountModuleBridge.InsertUpdateFromMoneyRequisitionRefundVoucher(ref context, mrr);
                UserLog.SaveUserLog(ref context, new UserLog(mrr.MRRNo.ToString(), DateTime.Now, "MRRVoucher", ScreenAction.Approve));
                context.SaveChanges();
                return Json(mrr, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetMRRyId(int id)
        {
            var mrrExist = context.MoneyRequisitionRefund.FirstOrDefault(x => x.MRRNo == id);

            if(mrrExist != null)
            {
                return Json(mrrExist, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateMRR(MoneyRequisitionRefund mrr, MoneyRequisitionBalanceCalculation mRBCalculation)
        {
            var mRRExist = context.MoneyRequisitionRefund.FirstOrDefault(m => m.MRRNo == mrr.MRRNo);
            if (mRRExist != null)
            {
                try
                {
                    //var transectionE = context.Transection.Where(x => x.DocID == mrr.MRRNo && x.DocType == "MRR").ToList();
                    //if (transectionE.Count() == 0)
                    //{
                        context.MoneyRequisitionRefund.Remove(mRRExist);
                        mrr.Date = mrr.Date.Add(DateTime.Now.TimeOfDay);
                        context.MoneyRequisitionRefund.Add(mrr);
                        var moneryRbal = context.MoneyRequisitionBalanceCalculations.FirstOrDefault(x => x.DocID == mrr.MRRNo && x.DocumentType == "MRR");
                        context.MoneyRequisitionBalanceCalculations.Remove(moneryRbal);

                        mRBCalculation.DocID = mrr.MRRNo;
                        mRBCalculation.Date = mrr.Date;
                        mRBCalculation.DocumentType = "MRR";
                        mRBCalculation.Amount = mrr.Amount * (-1);
                        context.MoneyRequisitionBalanceCalculations.Add(mRBCalculation);
                        AccountModuleBridge.InsertUpdateFromMoneyRequisitionVouRefund(ref context, mrr);

                        UserLog.SaveUserLog(ref context, new UserLog(mrr.MRRNo.ToString(), DateTime.Now, "MRRVoucher", ScreenAction.Update));
                        context.SaveChanges();
                        return Json(mrr, JsonRequestBehavior.AllowGet);
                    //}
                    //else
                    //{
                    //    return Json(new { Message = 5 }, JsonRequestBehavior.AllowGet);
                    //}

                }
                catch (Exception ex)
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteMRRByid(int id)
        {
            var mRRExist = context.MoneyRequisitionRefund.FirstOrDefault(m => m.MRRNo == id);

            if(mRRExist != null)
            {
                //var transectionE = context.Transection.Where(x => x.DocID == id && x.DocType == "MRR").ToList();
                //if(transectionE.Count() == 0)
                //{
                    context.MoneyRequisitionRefund.Remove(mRRExist);
                    var mrbc = context.MoneyRequisitionBalanceCalculations.FirstOrDefault(x => x.DocID == id&& x.DocumentType=="MRR");
                    context.MoneyRequisitionBalanceCalculations.Remove(mrbc);
                    AccountModuleBridge.DeleteFromMoneyRequisitionRefund(ref context, mRRExist);
                    UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "MoneyRequisitionVoucher", ScreenAction.Delete));

                    context.SaveChanges();
                    return Json(mRRExist, JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                //    return Json(new { Message = 5 }, JsonRequestBehavior.AllowGet);
                //}
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            
        }

        public ActionResult PrintReviewMRR(int mRRNo)
        {
            ReportParmPersister param = new ReportParmPersister();
            var postedBy = (from p in context.UserLog
                            join c in context.Employees on p.LogInName equals c.LogInUserName
                            where p.ScreenName == "MoneyRequisitionRefund" && p.TrnasId == mRRNo.ToString()
                            select c).ToList().FirstOrDefault();
            if (postedBy != null)
            {
                param.PostedBy = postedBy.Name;
            }
            else
            {
                param.PostedBy = "";
            }

            string sql = string.Format("exec spPrintMRRefund " + mRRNo + "");
            var items = context.Database.SqlQuery<MRRPreviewReport>(sql).ToList();
            if (items.Count == 0)
            {
                MRRPreviewReport report = new MRRPreviewReport();
                items.Add(report);
            }
            MRRPreviewR rd = ShowReport(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");

        }

        public MRRPreviewR ShowReport(ReportParmPersister persister, List<MRRPreviewReport> data)
        {
            MRRPreviewR mRRPreviewR = new MRRPreviewR();

            mRRPreviewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\MRRPreviewR.rpt");
            mRRPreviewR.Load(APPPATH);
            mRRPreviewR.SetDataSource(data);
            return mRRPreviewR;
        }
    }
}