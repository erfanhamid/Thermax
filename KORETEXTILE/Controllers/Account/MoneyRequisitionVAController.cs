using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.AccountModule;
using BEEERP.Models.Database;
using System.Data.Entity;
using BEEERP.Models.Log;
using BEEERP.Models.Notification;
using Microsoft.AspNet.Identity;
using BEEERP.Models.Bridge;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using System.IO;

namespace BEEERP.Controllers.Account

{
    [ShowNotification]
    //[Authorize(Roles = "ProAdmin,ProOperator,ProViewer,ProApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    public class MoneyRequisitionVAController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: MoneyRequisitionVA
        public ActionResult MoneyRequisitionVA(string type = "", int mrvano = 0)
        {
            ViewBag.costcenter = LoadComboBox.LoadBranchInfo();
            ViewBag.costgroup = LoadComboBox.LoadALLCostGroup(); 
            //ViewBag.debitAcount = LoadComboBox.LoadAllPaymentAccount();
            ViewBag.debitAcount = LoadComboBox.LoadAllAccountWithoutBankAndCash();
            ViewBag.PaymentAc = LoadComboBox.LoadReceiveAcc();

            if (mrvano != 0)
            {
                var notificationE = context.Notification.FirstOrDefault(x => x.Type == "MRVAEntry" && x.TransactionNo == mrvano.ToString() && x.NotificationDetails == "This Money Requisition Voucher Adjustment is approved.");
                ViewBag.MRVNO = mrvano;
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
        public ActionResult GetEmployeeById(int empId)
        {
            var employee = FindObjectById.GetEmployeeById(empId);
            if (employee != null)
            {
                return Json(new { Name = employee.Name, Designation = FindObjectById.SearchDesignation(employee.Designation).Name, PrevBal = FindObjectById.PrevBalance(employee.Id) }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveMonyRequisationVA(RequisitionMoneyVoucher moneyRequisitionVoucherAdjust,List<RequisitionMoneyVoucherLineItem> moneyRequisitionVoucherAdjustLineItems)
        {
            string id = "";
            string yearLastTwoPart = moneyRequisitionVoucherAdjust.Date.Year.ToString().Substring(2, 2).ToString();
            var noOfMonyReqVoucher = context.RequisitionMoneyVoucher.Count();


            if (noOfMonyReqVoucher > 0)
            {
                var lastMonyRequisationV = context.RequisitionMoneyVoucher.ToList().LastOrDefault(x => x.RSLNO.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                if (lastMonyRequisationV == null)
                {
                    id = yearLastTwoPart + "00001";
                }
                else
                {
                    id = yearLastTwoPart + (Convert.ToInt32(lastMonyRequisationV.RSLNO.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                }
            }
            else
            {
                id = yearLastTwoPart + "00001";
            }


            //if (noOfMonyReqVoucher > 0)
            //{
            //    var lastMonyRequisationV = context.RequisitionMoneyVoucher.ToList().LastOrDefault(x => x.RSLNO.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
            //    id = yearLastTwoPart + (Convert.ToInt32(lastMonyRequisationV.RSLNO.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
            //}
            //else
            //{
            //    id = yearLastTwoPart + "00001";
            //}
            moneyRequisitionVoucherAdjust.RSLNO = Convert.ToInt32(id);
            //moneyRequisitionVoucherAdjust.Date = moneyRequisitionVoucherAdjust.Date.Add(DateTime.Now.TimeOfDay);
            //try
            //{
               // var moneyRequisitionBalanceCalculation = new List<MoneyRequisitionBalanceCalculation>();
                moneyRequisitionVoucherAdjustLineItems.ForEach(m =>
                {
                    var mrbc = new MoneyRequisitionBalanceCalculation();
                    mrbc.Date = moneyRequisitionVoucherAdjust.Date;
                    mrbc.EmpID = moneyRequisitionVoucherAdjust.EmpID;
                    mrbc.DocumentType = "MRVA";
                    mrbc.AccountId = m.DebitAC;
                    mrbc.Description = m.Description;
                    mrbc.RefNo = m.RefNo;
                    mrbc.Amount = m.Amount*(-1);
                    mrbc.DocID = moneyRequisitionVoucherAdjust.RSLNO;
                    context.MoneyRequisitionBalanceCalculations.Add(mrbc);
                    
                });

                AccountModuleBridge.InsertUpdateFromMoneyRequisitionVouAdjust(context, moneyRequisitionVoucherAdjust,moneyRequisitionVoucherAdjustLineItems);
                context.RequisitionMoneyVoucher.Add(moneyRequisitionVoucherAdjust);
                moneyRequisitionVoucherAdjustLineItems.ForEach(x => { x.Slno = moneyRequisitionVoucherAdjust.RSLNO; context.RequisitionMoneyVoucherLineItem.Add(x); });
                UserLog.SaveUserLog(ref context, new UserLog(moneyRequisitionVoucherAdjust.RSLNO.ToString(), DateTime.Now, "MoneyRequisitionVoucherAdjust", ScreenAction.Save));

                UserNotification notification = new UserNotification();
                notification.UserId = User.Identity.GetUserId();
                notification.Type = "MRVAEntry";
                notification.TransactionNo = moneyRequisitionVoucherAdjust.RSLNO.ToString();
                notification.NotificationHead = "New Money Requisition Voucher Adjustment";
                notification.NotificationDetails = "This Money Requisition Voucher Adjustment is pending for approved.";
                notification.PostingDate = DateTime.Now;
                notification.BranchId = moneyRequisitionVoucherAdjust.CostCenterID;

                context.Notification.Add(notification);

                context.SaveChanges();
                return Json(new { Message = 1, moneyRequisitionVoucherAdjust }, JsonRequestBehavior.AllowGet);
            //}
            //catch (Exception ex)
            //{
            //    return Json(new { Message = 0, JsonRequestBehavior.AllowGet });
            //}

        }

        //public ActionResult TransectionEffect(MoneyRequisitionVoucherAdjust moneyRequisitionVoucherAdjust, List<MoneyRequisitionVoucherAdjustLineItem> moneyRequisitionVoucherAdjustLineItems)
        //{
        //    var mrV = context.MoneyRequisitionVoucherAdjusts.AsNoTracking().FirstOrDefault(x => x.MRVANo == moneyRequisitionVoucherAdjust.MRVANo);

        //    if (mrV != null)
        //    {
        //        var notificationE = context.Notification.FirstOrDefault(x => x.Type == "MRVAEntry" && x.TransactionNo == mrV.MRVANo.ToString());
        //        if (notificationE != null)
        //        {
        //            UserNotification notification = new UserNotification();
        //            notification.UserId = User.Identity.GetUserId();
        //            notification.Type = "MRVAEntry";
        //            notification.TransactionNo = moneyRequisitionVoucherAdjust.MRVANo.ToString();
        //            notification.NotificationHead = "New Money Requisition Voucher Adjustment";
        //            notification.NotificationDetails = "This Money Requisition Voucher Adjustment is approved.";
        //            notification.PostingDate = DateTime.Now;
        //            notification.BranchId = moneyRequisitionVoucherAdjust.CostCenterId;

        //            context.Notification.Add(notification);
        //            //notificationE.NotificationDetails = "This Fund Transfer Voucher is approved.";
        //            //notificationE.UserId = User.Identity.GetUserId();
        //            //context.Entry<UserNotification>(notificationE).State = EntityState.Modified;

        //            context.ApprovalLog.Where(x => x.NotificationId == notificationE.Id).ToList().ForEach(x => { x.IsApproved = true; context.Entry<ApprovalLog>(x).State = EntityState.Modified; });

        //            moneyRequisitionVoucherAdjust.Date = moneyRequisitionVoucherAdjust.Date.Add(DateTime.Now.TimeOfDay);
        //            context.Entry<MoneyRequisitionVoucherAdjust>(moneyRequisitionVoucherAdjust).State = EntityState.Modified;
        //            moneyRequisitionVoucherAdjustLineItems.ForEach(x => {
        //                x.MRVANo = moneyRequisitionVoucherAdjust.MRVANo;
        //                context.Entry<MoneyRequisitionVoucherAdjustLineItem>(x).State = EntityState.Modified;

        //                var mrbcal = context.MoneyRequisitionBalanceCalculations.FirstOrDefault(y => y.DocID == moneyRequisitionVoucherAdjust.MRVANo && y.DocumentType == "MRVA" && y.AccountId == x.DebitAcId);
        //                if (mrbcal != null)
        //                {
        //                    context.MoneyRequisitionBalanceCalculations.Remove(mrbcal);
        //                }

        //                var mrbc = new MoneyRequisitionBalanceCalculation();
        //                mrbc.Date = moneyRequisitionVoucherAdjust.Date;
        //                mrbc.EmpID = moneyRequisitionVoucherAdjust.EmpID;
        //                mrbc.DocumentType = "MRVA";
        //                mrbc.AccountId = x.DebitAcId;
        //                mrbc.Description = x.MRVADescription;
        //                mrbc.RefNo = x.RefNo;
        //                mrbc.Amount = x.MRVAAmount * (-1);
        //                mrbc.DocID = moneyRequisitionVoucherAdjust.MRVANo;
        //                context.MoneyRequisitionBalanceCalculations.Add(mrbc);

        //            });
        //        }
                
        //        AccountModuleBridge.InsertUpdateFromMoneyRequisitionVouAdjust(context, moneyRequisitionVoucherAdjust, moneyRequisitionVoucherAdjustLineItems);
        //        UserLog.SaveUserLog(ref context, new UserLog(moneyRequisitionVoucherAdjust.MRVANo.ToString(), DateTime.Now, "MoneyRequisitionVoucherAdjust", ScreenAction.Approve));
        //        context.SaveChanges();
        //        return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
        //    }

        //    else
        //    {
        //        return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
        //    }

        //}

        public ActionResult UpdateMonyRequisationVA(RequisitionMoneyVoucher moneyRequisitionVoucherAdjust, List<RequisitionMoneyVoucherLineItem> moneyRequisitionVoucherAdjustLineItems)
        {
            //var transectionE = context.Transection.Where(x => x.DocID == moneyRequisitionVoucherAdjust.RSLNO && x.DocType == "MRVA").ToList();
            //if (transectionE.Count() == 0)
            //{
                //moneyRequisitionVoucherAdjust.Date = moneyRequisitionVoucherAdjust.Date.Add(DateTime.Now.TimeOfDay);
                //try
                //{

                    // var moneyRequisitionBalanceCalculation = new List<MoneyRequisitionBalanceCalculation>();
                    var prevMRVABalances = context.MoneyRequisitionBalanceCalculations.Where(d => d.DocID == moneyRequisitionVoucherAdjust.RSLNO).ToList();
                    prevMRVABalances.ForEach(n => { context.MoneyRequisitionBalanceCalculations.Remove(n); });
                    moneyRequisitionVoucherAdjustLineItems.ForEach(m =>
                    {
                        var mrbc = new MoneyRequisitionBalanceCalculation();
                        mrbc.Date = moneyRequisitionVoucherAdjust.Date;
                        mrbc.EmpID = moneyRequisitionVoucherAdjust.EmpID;
                        mrbc.DocumentType = "MRVA";
                        mrbc.AccountId = m.DebitAC;
                        mrbc.Description = m.Description;
                        mrbc.RefNo = m.RefNo;
                        mrbc.Amount = m.Amount * (-1);
                        mrbc.DocID = moneyRequisitionVoucherAdjust.RSLNO;
                        context.MoneyRequisitionBalanceCalculations.Add(mrbc);



                    });
                    //if (moneyRequisitionVoucherAdjust.NegativeBalanceCheckbox == true)
                    //{
                    //    var mrv = new MoneyRequisitionVoucher();
                    //    mrv.MRVNo = moneyRequisitionVoucherAdjust.MRVNo;
                    //    mrv.EmpID = moneyRequisitionVoucherAdjust.EmpID;
                    //    mrv.Date = moneyRequisitionVoucherAdjust.Date;
                    //    mrv.AccountId = moneyRequisitionVoucherAdjust.AccountId;
                    //    mrv.Amount = moneyRequisitionVoucherAdjust.Amount;
                    //    mrv.Description = moneyRequisitionVoucherAdjust.Description;
                    //    mrv.PrevBalance = Convert.ToDecimal(moneyRequisitionVoucherAdjust.PreviousBalance);

                    //    var mrbc = new MoneyRequisitionBalanceCalculation();
                    //    mrbc.Date = moneyRequisitionVoucherAdjust.Date;
                    //    mrbc.EmpID = moneyRequisitionVoucherAdjust.EmpID;
                    //    mrbc.DocumentType = "MRV";
                    //    mrbc.AccountId = mrv.AccountId;
                    //    mrbc.Description = mrv.Description;
                    //    mrbc.Amount = mrv.Amount;
                    //    mrbc.MRVNo = mrv.MRVNo;

                    //    //context.Entry<MoneyRequisitionVoucher>(mrv).State = EntityState.Modified;

                    //    var prevMrv = context.MoneyRequisitionVouchers.FirstOrDefault(m => m.MRVNo == mrv.MRVNo);
                    //    var prevMrvBalance = context.MoneyRequisitionBalanceCalculations.FirstOrDefault(m => m.MRVNo == mrbc.MRVNo);
                    //    context.MoneyRequisitionVouchers.Remove(prevMrv);
                    //    context.MoneyRequisitionBalanceCalculations.Remove(prevMrvBalance);
                    //    context.MoneyRequisitionVouchers.Add(mrv);
                    //    context.MoneyRequisitionBalanceCalculations.Add(mrbc);
                    //    //context.Entry<MoneyRequisitionBalanceCalculation>(mrbc).State = EntityState.Modified;
                    //    context.SaveChanges();

                    //}
                    //context.Entry<RequisitionMoneyVoucher>(moneyRequisitionVoucherAdjust).State = EntityState.Modified;
                    var mrva = context.RequisitionMoneyVoucher.FirstOrDefault(m => m.RSLNO == moneyRequisitionVoucherAdjust.RSLNO);
                    context.RequisitionMoneyVoucher.Remove(mrva);
                    context.RequisitionMoneyVoucher.Add(moneyRequisitionVoucherAdjust);



                    context.SaveChanges();
                    var preLineItems = context.RequisitionMoneyVoucherLineItem.Where(m => m.Slno == moneyRequisitionVoucherAdjust.RSLNO).ToList();
                    preLineItems.ForEach(d =>
                    {
                        context.RequisitionMoneyVoucherLineItem.Remove(d);
                    });
                    moneyRequisitionVoucherAdjustLineItems.ForEach(x =>
                    { x.Slno = moneyRequisitionVoucherAdjust.RSLNO; context.RequisitionMoneyVoucherLineItem.Add(x); });
                    AccountModuleBridge.InsertUpdateFromMoneyRequisitionVouAdjust(context, moneyRequisitionVoucherAdjust, moneyRequisitionVoucherAdjustLineItems);
                    UserLog.SaveUserLog(ref context, new UserLog(moneyRequisitionVoucherAdjust.RSLNO.ToString(), DateTime.Now, "MoneyRequisitionVoucherAdjust", ScreenAction.Update));
                    context.SaveChanges();
                    return Json(new { Message = 1, JsonRequestBehavior.AllowGet });
                //}
                //catch (Exception ex)
                //{
                //    return Json(new { Message = 0, JsonRequestBehavior.AllowGet });
                //}
            //}
            //else
            //{
            //    return Json(new { Message = 5 }, JsonRequestBehavior.AllowGet);
            //}
        }

        public ActionResult DeleteMonyRequisationVA(RequisitionMoneyVoucher moneyRequisitionVoucherAdjust, List<RequisitionMoneyVoucherLineItem> moneyRequisitionVoucherAdjustLineItems)
        {
            //var transectionE = context.Transection.Where(x => x.DocID == moneyRequisitionVoucherAdjust.RSLNO && x.DocType == "MRVA").ToList();
            //if (transectionE.Count() == 0)
            //{
            try
            {
                var mrva = context.RequisitionMoneyVoucher.FirstOrDefault(m => m.RSLNO == moneyRequisitionVoucherAdjust.RSLNO);
                    context.RequisitionMoneyVoucher.Remove(mrva);
                    var preLineItems = context.RequisitionMoneyVoucherLineItem.Where(m => m.Slno == moneyRequisitionVoucherAdjust.RSLNO).ToList();
                    preLineItems.ForEach(d =>
                    {
                        context.RequisitionMoneyVoucherLineItem.Remove(d);
                    });

                    var prevMRVABalances = context.MoneyRequisitionBalanceCalculations.Where(d => d.DocID == moneyRequisitionVoucherAdjust.RSLNO).ToList();
                    prevMRVABalances.ForEach(n => { context.MoneyRequisitionBalanceCalculations.Remove(n); });

                    if (moneyRequisitionVoucherAdjust.NegativeBalanceCheckbox == true)
                    {
                        var prevMrv = context.MoneyRequisitionVouchers.FirstOrDefault(m => m.MRVNo == moneyRequisitionVoucherAdjust.MRVNo);
                        var prevMrvBalance = context.MoneyRequisitionBalanceCalculations.FirstOrDefault(m => m.DocID == moneyRequisitionVoucherAdjust.MRVNo);
                        var prevRef = context.MRVAReferences.FirstOrDefault(p => p.MRVAId == moneyRequisitionVoucherAdjust.RSLNO);
                        context.MoneyRequisitionVouchers.Remove(prevMrv);
                        context.MoneyRequisitionBalanceCalculations.Remove(prevMrvBalance);
                        context.MRVAReferences.Remove(prevRef);

                        context.SaveChanges();
                    }
                AccountModuleBridge.DeleteFromMoneyRequisitionVouAdjust(context, moneyRequisitionVoucherAdjust, moneyRequisitionVoucherAdjustLineItems);
                UserLog.SaveUserLog(ref context, new UserLog(moneyRequisitionVoucherAdjust.RSLNO.ToString(), DateTime.Now, "MoneyRequisitionVoucherAdjust", ScreenAction.Delete));
                    context.SaveChanges();
                    return Json(new { Message = 1, JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                return Json(new { Message = 0, JsonRequestBehavior.AllowGet });
            }
            //}
            //else
            //{
            //    return Json(new { Message = 5 }, JsonRequestBehavior.AllowGet);
            //}
        }

        public ActionResult GetMRVA(int mrvaNo)
        {
            //try
            //{
                var isMRVAExists = context.RequisitionMoneyVoucher.FirstOrDefault(m => m.RSLNO == mrvaNo);
                if (isMRVAExists == null)
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
                else
                {   
                    //var PreviousTotal= context.MoneyRequisitionBalanceCalculations.Sum(x=>x.EmpID== isMRVAExists.EmpID).
                    isMRVAExists.EmployeeName = context.Employees.FirstOrDefault(m => m.Id == isMRVAExists.EmpID).Name;
                    isMRVAExists.EmployeeDesignation = context.Designation.FirstOrDefault(d=>d.Id== context.Employees.FirstOrDefault(m => m.Id == isMRVAExists.EmpID).Designation).Name;
                    var mrvaLItems = context.RequisitionMoneyVoucherLineItem.Where(m => m.Slno == mrvaNo).ToList();
                    mrvaLItems.ForEach(m =>
                    {
                        m.CostGroupName = context.EmployeeSection.FirstOrDefault(d => d.ID == m.CostGroupID).CGroup;
                        m.DebitACName = context.ChartOfAccount.FirstOrDefault(d => d.Id == m.DebitAC).Name;
                    });
                    var mrvReferance = context.MRVAReferences.FirstOrDefault(m => m.MRVAId == mrvaNo);
                    if (mrvReferance != null)
                    {
                        var mrv = context.RequisitionMoneyVoucher.FirstOrDefault(m => m.RSLNO == mrvReferance.MRVId);
                        mrv.AccountName=context.ChartOfAccount.FirstOrDefault(d => d.Id ==mrv.AccountId).Name;
                        return Json(new { Message = 1, mrv = mrv, mrva = isMRVAExists, mrvaLinesItems = mrvaLItems }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Message = 1, mrva = isMRVAExists, mrvaLinesItems = mrvaLItems ,PrevBal = FindObjectById.PrevBalance(isMRVAExists.EmpID) }, JsonRequestBehavior.AllowGet);
                    }
                    
                   
                }
            //}
            //catch(Exception ex)
            //{
            //    return Json(new { Message = 2, JsonRequestBehavior.AllowGet });
            //}           
        }


        public ActionResult GetMoneyRequisitionVAPreview(int mRVANo)
        {
            ReportParmPersister param = new ReportParmPersister();
            var postedBy = (from p in context.UserLog
                            join c in context.Employees on p.LogInName equals c.LogInUserName
                            where p.ScreenName == "MoneyRequisitionVoucherAdjust" && p.TrnasId == mRVANo.ToString()
                            select c).ToList().FirstOrDefault();
            if (postedBy != null)
            {
                param.PostedBy = postedBy.Name;
            }
            else
            {
                param.PostedBy = "";
            }
            
            string sql = string.Format("exec spPrintMRVA " + mRVANo + "");
            var items = context.Database.SqlQuery<MRVAPreviewReport>(sql).ToList();
            if (items.Count == 0)
            {
                MRVAPreviewReport report = new MRVAPreviewReport();
                items.Add(report);
            }
            MRVAPreviewR rd = ShowReport(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");

        }

        public MRVAPreviewR ShowReport(ReportParmPersister persister, List<MRVAPreviewReport> data)
        {
            MRVAPreviewR mRVAPreviewR = new MRVAPreviewR();

            mRVAPreviewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\MRVAPreviewR.rpt");
            mRVAPreviewR.Load(APPPATH);
            mRVAPreviewR.SetDataSource(data);
            return mRVAPreviewR;
        }

    }
}