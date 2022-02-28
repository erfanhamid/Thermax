using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.AccountModule;
using BEEERP.Models.Bridge;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using BEEERP.Models.Notification;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Account
{
    [ShowNotification]
   // [Authorize(Roles = "ProAdmin,ProOperator,ProViewer,ProApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    public class MoneyRequisitionVoucherController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: MoneyRequisitionVoucher
        public ActionResult MoneyRequisitionVoucher(string type = "", int mrvno = 0)    
        {
            ViewBag.PaymentAc = LoadComboBox.LoadReceiveAcc();
            
            if (mrvno != 0)
            {
                var notificationE = context.Notification.FirstOrDefault(x => x.Type == "MRVEntry" && x.TransactionNo == mrvno.ToString() && x.NotificationDetails == "This Fund Transfer Voucher is approved.");
                ViewBag.MRVNo = mrvno;
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
            
            if (employee != null )
            {
                return Json(new { Name = employee.Name, Designation = FindObjectById.SearchDesignation(employee.Designation).Name, PrevBal=FindObjectById.PrevBalance(employee.Id) }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveMoneyRequisitionVoucher(MoneyRequisitionVoucher mRVoucher, MoneyRequisitionBalanceCalculation mRBCalculation)
        {
            string id = "";
            string yearLastTwoPart = mRVoucher.Date.Year.ToString().Substring(2, 2).ToString();
            var noOfMRVoucher = context.MoneyRequisitionVouchers.Count();
            if (noOfMRVoucher > 0)
            {
                var lastMRV = context.MoneyRequisitionVouchers.ToList().LastOrDefault(x => x.MRVNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                if (lastMRV == null)
                {
                    id = yearLastTwoPart + "00001";
                }
                else
                {
                    id = yearLastTwoPart + (Convert.ToInt32(lastMRV.MRVNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0'); ;
                }
            }
            else
            {
                id = yearLastTwoPart + "00001";
            }

            mRVoucher.MRVNo = Convert.ToInt32(id);
            //mRVoucher.Date = mRVoucher.Date.Add(DateTime.Now.TimeOfDay);

            //UserNotification notification = new UserNotification();
            //notification.UserId = User.Identity.GetUserId();
            //notification.Type = "MRVEntry";
            //notification.TransactionNo = mRVoucher.MRVNo.ToString();
            //notification.NotificationHead = "New Money Requisition Voucher";
            //notification.NotificationDetails = "This Money Requisition Voucher is pending for approved.";
            //notification.PostingDate = DateTime.Now;
            //notification.BranchId = 0;

            //context.Notification.Add(notification);

           
            
            try
            {
                context.MoneyRequisitionVouchers.Add(mRVoucher);

                mRBCalculation.DocID = mRVoucher.MRVNo;
                mRBCalculation.Date = mRVoucher.Date;
                mRBCalculation.DocumentType = "MRV";
                context.MoneyRequisitionBalanceCalculations.Add(mRBCalculation);
                AccountModuleBridge.InsertUpdateFromMoneyRequisitionVoucher(ref context, mRVoucher);
                UserLog.SaveUserLog(ref context, new UserLog(mRVoucher.MRVNo.ToString(), DateTime.Now, "MoneyRequisitionVoucher", ScreenAction.Save));
                context.SaveChanges();
                return Json(new { Message = 1, mrvNo=mRVoucher.MRVNo}, JsonRequestBehavior.AllowGet );
            }
            catch (Exception ex)
            {
                return Json(new { Message = 0, JsonRequestBehavior.AllowGet });
            }
        }

        public ActionResult TransectionEffect(MoneyRequisitionVoucher mRVoucher, MoneyRequisitionBalanceCalculation mRBCalculation)
        {
            var mrV = context.MoneyRequisitionVouchers.AsNoTracking().FirstOrDefault(x => x.MRVNo == mRVoucher.MRVNo);

            if (mrV != null)
            {
                var notificationE = context.Notification.FirstOrDefault(x => x.Type == "MRVEntry" && x.TransactionNo == mrV.MRVNo.ToString());
                if (notificationE != null)
                {
                    UserNotification notification = new UserNotification();
                    notification.UserId = User.Identity.GetUserId();
                    notification.Type = "MRVEntry";
                    notification.TransactionNo = mRVoucher.MRVNo.ToString();
                    notification.NotificationHead = "New Money Requisition Voucher";
                    notification.NotificationDetails = "This Money Requisition Voucher is approved.";
                    notification.PostingDate = DateTime.Now;
                    notification.BranchId = 0;

                    context.Notification.Add(notification);
                    //notificationE.NotificationDetails = "This Fund Transfer Voucher is approved.";
                    //notificationE.UserId = User.Identity.GetUserId();
                    //context.Entry<UserNotification>(notificationE).State = EntityState.Modified;

                    context.ApprovalLog.Where(x => x.NotificationId == notificationE.Id).ToList().ForEach(x => { x.IsApproved = true; context.Entry<ApprovalLog>(x).State = EntityState.Modified; });

                    mRVoucher.Date = mRVoucher.Date.Add(DateTime.Now.TimeOfDay);
                    context.Entry<MoneyRequisitionVoucher>(mRVoucher).State = EntityState.Modified;
                }


                mRBCalculation.DocID = mRVoucher.MRVNo;
                mRBCalculation.Date = mRVoucher.Date;
                mRBCalculation.DocumentType = "MRV";
                context.MoneyRequisitionBalanceCalculations.Add(mRBCalculation);

                AccountModuleBridge.InsertUpdateFromMoneyRequisitionVoucher(ref context, mRVoucher);
                UserLog.SaveUserLog(ref context, new UserLog(mRVoucher.MRVNo.ToString(), DateTime.Now, "MoneyRequisitionVoucher", ScreenAction.Approve));
                context.SaveChanges();
                return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetMoneyRequisitionVoucherById(int mrv)
        {
            var moneyRV = context.MoneyRequisitionVouchers.FirstOrDefault(x => x.MRVNo == mrv);
            //var moneyreq = context.UserLog.FirstOrDefault(x => x.TrnasId==mrv.ToString() &&  x.ScreenName == "MoneyRequisitionVoucher" && x.Action == "Save");
            if (moneyRV == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(moneyRV, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult UpdateMoneyRequisitionVoucher(MoneyRequisitionVoucher mRVoucher, MoneyRequisitionBalanceCalculation mRBCalculation)
        {
            var MRVExist = context.MoneyRequisitionVouchers.FirstOrDefault(m => m.MRVNo == mRVoucher.MRVNo);
            if (MRVExist != null)
            {
                mRBCalculation.DocumentType = "MRV";
                mRVoucher.Date = mRVoucher.Date.Add(DateTime.Now.TimeOfDay);
                try
                {

                    var moneryRbal = context.MoneyRequisitionBalanceCalculations.FirstOrDefault(x => x.DocID == mRVoucher.MRVNo && x.DocumentType == "MRV");
                    context.MoneyRequisitionVouchers.Remove(MRVExist);
                    if (moneryRbal!=null)
                    {
                        context.MoneyRequisitionBalanceCalculations.Remove(moneryRbal);
                    }
                    context.MoneyRequisitionVouchers.Add(mRVoucher);
                    mRBCalculation.DocID = mRVoucher.MRVNo;
                    mRBCalculation.Date = mRVoucher.Date;
                    mRBCalculation.DocumentType = "MRV";
                    context.MoneyRequisitionBalanceCalculations.Add(mRBCalculation);
                    AccountModuleBridge.InsertUpdateFromMoneyRequisitionVoucher(ref context, mRVoucher);
                    UserLog.SaveUserLog(ref context, new UserLog(mRVoucher.MRVNo.ToString(), DateTime.Now, "MoneyRequisitionVoucher", ScreenAction.Update));
                    context.SaveChanges();
                    return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);


                }
                catch (Exception ex)
                {
                    return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
                }
            }              
            else
            {
                return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteMRVById(int id)
        {
            var mrvExist = context.MoneyRequisitionVouchers.FirstOrDefault(x => x.MRVNo == id);
            if (mrvExist != null)
            {
                //var transectionE = context.Transection.Where(x => x.DocID == id && x.DocType == "MRV").ToList();
                //if (transectionE.Count() == 0)
                //{
                    context.MoneyRequisitionVouchers.Remove(mrvExist);
                    var mrbc = context.MoneyRequisitionBalanceCalculations.FirstOrDefault(x => x.DocID == id&& x.DocumentType=="MRV");
                    context.MoneyRequisitionBalanceCalculations.Remove(mrbc);
                    AccountModuleBridge.DeleteFromMoneyRequisitionVoucher(ref context, mrvExist);
                    UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "MoneyRequisitionVoucher", ScreenAction.Delete));
                    context.SaveChanges();
                    return Json(id, JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                //    return Json(new { Message = 5 }, JsonRequestBehavior.AllowGet);
                //}
                
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        //public FileResult GetMoneyRequisitionVoucherPreview1(int mRVId)
        //{
        //    Session["ReportName"] = "MRVPreviewR";
        //    ReportParmPersister param = new ReportParmPersister();

        //    var mrv = context.MoneyRequisitionVouchers.FirstOrDefault(x => x.MRVNo == mRVId);
        //    if (mrv != null)
        //    {
        //        param.SampleNo = mrv.MRVNo;
        //        param.SampleDate = mrv.Date;
        //        param.RefNo = mrv.RefNo;
        //        param.ID = mrv.EmpID;
        //    }
        //    else
        //    {
        //        param.SampleNo = 0;
        //        param.RefNo = "";
        //        param.ID = 0;
        //    }

        //    param.EmpName = context.Employees.FirstOrDefault(x => x.Id == mrv.EmpID).Name;
        //    param.PostedBy = (from e in context.Employees.AsEnumerable()
        //                      join u in context.UserLog.AsEnumerable() on e.LogInUserName equals u.LogInName
        //                      where u.ScreenName == "MoneyRequisitionVoucher" && u.Action == "Save" && u.TrnasId == mrv.MRVNo.ToString()
        //                      select e.Name).FirstOrDefault();

        //    param.ApprovedBy = (from e in context.Employees.AsEnumerable()
        //                        join u in context.UserLog.AsEnumerable() on e.LogInUserName equals u.LogInName
        //                        where u.ScreenName == "MoneyRequisitionVoucher" && u.Action == "Approve" && u.TrnasId == mrv.MRVNo.ToString()
        //                        select e.Name).FirstOrDefault();

        //    if (param.ApprovedBy == null)
        //    {
        //        param.ApprovedBy = "";
        //    }

        //    string sql = string.Format("exec spPrintMRV'" + mRVId + "'");
        //    var items = context.Database.SqlQuery<MRVPreviewReport>(sql).ToList();
        //    if (items.Count == 0)
        //    {
        //        MRVPreviewReport report = new MRVPreviewReport();
        //        items.Add(report);
        //    }

        //    MRVPreviewR rd = ShowReport(param, items);
        //    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //    stream.Seek(0, SeekOrigin.Begin);
        //    rd.Close();
        //    return new FileStreamResult(stream, "application/pdf");

        //}

        //public MRVPreviewR ShowReport(ReportParmPersister persister, List<MRVPreviewReport> data)
        //{
        //    MRVPreviewR mRVPreviewR = new MRVPreviewR();

        //    mRVPreviewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
        //    String APPPATH = Server.MapPath(@"\CrystalReport\MRVPreviewR.rpt");
        //    mRVPreviewR.Load(APPPATH);
        //    mRVPreviewR.SetDataSource(data);

        //    mRVPreviewR.SetParameterValue("mrvId", persister.SampleNo);
        //    mRVPreviewR.SetParameterValue("sampleDate", persister.SampleDate.ToString("dd-MM-yyyy"));
        //    mRVPreviewR.SetParameterValue("refNo", persister.RefNo);
        //    mRVPreviewR.SetParameterValue("empId", persister.ID);
        //    mRVPreviewR.SetParameterValue("empName", persister.EmpName);

        //    mRVPreviewR.SetParameterValue("postedBy", persister.PostedBy);
        //    mRVPreviewR.SetParameterValue("approvedBy", persister.ApprovedBy);

        //    mRVPreviewR.SetParameterValue("compName", persister.CompName);
        //    mRVPreviewR.SetParameterValue("compAddress", persister.CompAddress);
        //    mRVPreviewR.SetParameterValue("compContact", persister.CompContact);
        //    mRVPreviewR.SetParameterValue("compFax", persister.Fax);
        //    mRVPreviewR.SetParameterValue("factAddress", persister.FactAddress);
        //    mRVPreviewR.SetParameterValue("factContact", persister.FactContact);
        //    return mRVPreviewR;
        //}

        public ActionResult GetMoneyRequisitionVoucherPreview(int mRVId)
        {
            if (mRVId > 0)
            {
                ReportParmPersister param = new ReportParmPersister();
                var postedBy = (from p in context.UserLog
                                join c in context.Employees on p.LogInName equals c.LogInUserName
                                where p.ScreenName == "MoneyRequisitionVoucher" && p.TrnasId == mRVId.ToString()
                                select c).ToList().FirstOrDefault();
                if (postedBy != null)
                {
                    param.PostedBy = postedBy.Name;
                }
                else
                {
                    param.PostedBy = "";
                }

                //param.PostedBy = (from p in context.UserLog
                //                  join c in context.Employees on p.LogInName equals c.LogInUserName
                //                  where p.ScreenName == "GPB" && p.TrnasId == billNo.ToString()
                //                  select c).ToList().FirstOrDefault().Name;

                string sql = string.Format("exec spPrintMRV " + mRVId + "");
                var items = context.Database.SqlQuery<MRVPreviewReport>(sql).ToList();
                if (items.Count == 0)
                {
                    MRVPreviewReport report = new MRVPreviewReport();
                    items.Add(report);
                }
                MRVPreviewR rd = ShowReport(param, items);
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                return new FileStreamResult(stream, "application/pdf");
            }
            else
                return Json("Invalid Bill No", JsonRequestBehavior.AllowGet);
        }
        public MRVPreviewR ShowReport(ReportParmPersister persister, List<MRVPreviewReport> data)
        {
            MRVPreviewR MRVPreview = new MRVPreviewR();
            MRVPreview.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\MRVPreviewR.rpt");
            MRVPreview.Load(APPPATH);
            MRVPreview.SetDataSource(data);
            MRVPreview.SetParameterValue("postedBy", persister.PostedBy);
            //GoodsPurchaseBillPreview.SetParameterValue("compName", persister.CompName);
            //GoodsPurchaseBillPreview.SetParameterValue("compAddress", persister.CompAddress);
            //if (persister.Fax != "" || persister.Fax != null)
            //    GoodsPurchaseBillPreview.SetParameterValue("compContact", "Phone : " + persister.CompContact + " Fax : " + persister.Fax);
            //else
            //    GoodsPurchaseBillPreview.SetParameterValue("compContact", "Phone : " + persister.CompContact);
            //if (persister.FactAddress != "" || persister.FactAddress != null)
            //    GoodsPurchaseBillPreview.SetParameterValue("factoryAddress", "Factory : " + persister.FactAddress + " Phone : " + persister.FactContact);
            //else
            //    GoodsPurchaseBillPreview.SetParameterValue("factoryAddress", " ");
            return MRVPreview;
        }


    }
}