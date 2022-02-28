using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.AccountModule;
using System.Data.Entity;
using BEEERP.Models.Log;
using BEEERP.CrystalReport.ReportRdlc;
using System.IO;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.Models.Bridge;
using BEEERP.Models.Notification;
using Microsoft.AspNet.Identity;

namespace BEEERP.Controllers.Account
{
    [ShowNotification]
    
    //[Authorize(Roles = "ProAdmin,ProOperator,ProViewer,ProApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    public class ReceiveVoucherController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: ReceiveVoucher
        public ActionResult ReceiveVoucher(/*string type = "", int rvno = 0*/)
        {
            //string id = "";
            //string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
            //var noOfReceiveVs = context.ReceiveVouchers.Count();
            //if (noOfReceiveVs > 0)
            //{
            //    var lastRV = context.ReceiveVouchers.ToList().LastOrDefault(x => x.RVNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
            //    id = yearLastTwoPart + (Convert.ToInt32(lastRV.RVNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
            //}
            //else
            //{
            //    id = yearLastTwoPart + "00001";
            //}
            //ViewBag.RVNo = Convert.ToInt32(id);
            ViewBag.CostCenter = LoadComboBox.LoadBranchInfo();
            ViewBag.CostGroup = LoadComboBox.LoadALLCostGroup();
            ViewBag.ReceiveAccount = LoadComboBox.LoadReceiveAcc();
            ViewBag.CreditAcc = LoadComboBox.LoadAllAccount();
            ViewBag.PayeeName = LoadPayeeName();
            //ViewBag.BussinessUnit = LoadComboBox.LoadBussinessUnit();

            //if(rvno != 0)
            //{
            //    var notificationE = context.Notification.FirstOrDefault(x => x.Type == "RVEntry" && x.TransactionNo == rvno.ToString() && x.NotificationDetails == "This Receive Voucher is approved.");
            //    ViewBag.RVNo = rvno;
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
            //}
            return View();
        }
        public ActionResult SaveReceiveVoucher(ReceiveVoucher receiveVoucher)
        {
            var ScreenID = context.Screens.FirstOrDefault(x => x.Name == "Receive Voucher").ScreenID;
            var IsExpired = context.ScreenEntryLocks.FirstOrDefault(x => x.ScreenID == ScreenID);

            if (receiveVoucher.RVDate > IsExpired.SELDate)
            {
                string id = "";
                string yearLastTwoPart = receiveVoucher.RVDate.Year.ToString().Substring(2, 2).ToString();
                var noOfReceiveVs = context.ReceiveVouchers.Count();
                if (noOfReceiveVs > 0)
                {
                    var lastRV = context.ReceiveVouchers.ToList().LastOrDefault(x => x.RVNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                    if (lastRV == null)
                    {
                        id = yearLastTwoPart + "00001";
                    }
                    else
                    {
                        id = yearLastTwoPart + (Convert.ToInt32(lastRV.RVNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                    }
                }
                else
                {
                    id = yearLastTwoPart + "00001";
                }
                receiveVoucher.RVNo = Convert.ToInt32(id);

                receiveVoucher.RVDate = receiveVoucher.RVDate.Add(DateTime.Now.TimeOfDay);
                try
                {
                    receiveVoucher.CostGroupID = 1;
                    context.ReceiveVouchers.Add(receiveVoucher);

                    //UserNotification notification = new UserNotification();
                    //notification.UserId = User.Identity.GetUserId();
                    //notification.Type = "RVEntry";
                    //notification.TransactionNo = receiveVoucher.RVNo.ToString();
                    //notification.NotificationHead = "New Receive Voucher";
                    //notification.NotificationDetails = "This Receive Voucher is pending for approved.";
                    //notification.PostingDate = DateTime.Now;
                    //notification.BranchId = receiveVoucher.BranchID;

                    //context.Notification.Add(notification);

                    AccountModuleBridge.InsertUpdateFromReceiveVoucher(ref context, receiveVoucher);
                    UserLog.SaveUserLog(ref context, new UserLog(receiveVoucher.RVNo.ToString(), receiveVoucher.RVDate, "RV", ScreenAction.Save));
                    context.SaveChanges();
                    return Json(new { Message = 1, rVNo = receiveVoucher.RVNo }, JsonRequestBehavior.AllowGet);
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

        public ActionResult GetReceiveVoucherById(int rv)
        {
            var receiveV = context.ReceiveVouchers.FirstOrDefault(x => x.RVNo == rv);
            if (receiveV == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(receiveV, JsonRequestBehavior.AllowGet);

            }
        }

        //public ActionResult TransectionEffect(ReceiveVoucher receiveVoucher)
        //{
        //    var receiveV = context.ReceiveVouchers.AsNoTracking().FirstOrDefault(x => x.RVNo == receiveVoucher.RVNo);

        //    if(receiveV != null)
        //    {
        //        var notificationE = context.Notification.FirstOrDefault(x => x.Type == "RVEntry" && x.TransactionNo == receiveV.RVNo.ToString());
        //        if (notificationE != null)
        //        {
        //            UserNotification notification = new UserNotification();
        //            notification.UserId = User.Identity.GetUserId();
        //            notification.Type = "RVEntry";
        //            notification.TransactionNo = receiveVoucher.RVNo.ToString();
        //            notification.NotificationHead = "New Receive Voucher";
        //            notification.NotificationDetails = "This Receive Voucher is approved.";
        //            notification.PostingDate = DateTime.Now;
        //            notification.BranchId = receiveVoucher.BranchID;

        //            context.Notification.Add(notification);

        //            //notificationE.NotificationDetails = "This Receive Voucher is approved.";
        //            //notificationE.UserId = User.Identity.GetUserId();
        //            //context.Entry<UserNotification>(notificationE).State = EntityState.Modified;

        //            context.ApprovalLog.Where(x => x.NotificationId == notificationE.Id).ToList().ForEach(x => { x.IsApproved = true; context.Entry<ApprovalLog>(x).State = EntityState.Modified; });

        //            receiveVoucher.RVDate = receiveVoucher.RVDate.Add(DateTime.Now.TimeOfDay);
        //            context.Entry<ReceiveVoucher>(receiveVoucher).State = EntityState.Modified;
        //        }
                
        //        AccountModuleBridge.InsertUpdateFromReceiveVoucher(ref context, receiveV);
        //        UserLog.SaveUserLog(ref context, new UserLog(receiveVoucher.RVNo.ToString(), DateTime.Now, "ReceiveVoucher", ScreenAction.Approve));
        //        context.SaveChanges();
        //        return Json(new { Message = 1}, JsonRequestBehavior.AllowGet);
        //    }

        //    else
        //    {
        //        return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        public ActionResult UpdateReceiveVoucher(ReceiveVoucher receiveVoucher)
        {
            receiveVoucher.RVDate = receiveVoucher.RVDate.Add(DateTime.Now.TimeOfDay);
            try
            {
                var exsg = context.ReceiveVouchers.AsNoTracking().FirstOrDefault(m => m.RVNo == receiveVoucher.RVNo);
                if (exsg == null)
                {
                    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //var transectionE = context.Transection.Where(x => x.DocID == receiveVoucher.RVNo && x.DocType == "RV").ToList();
                    //if (transectionE.Count() == 0)
                    //{
                    receiveVoucher.CostGroupID = 1;
                    context.Entry<ReceiveVoucher>(receiveVoucher).State = EntityState.Modified;
                        AccountModuleBridge.InsertUpdateFromReceiveVoucher(ref context, receiveVoucher);
                        UserLog.SaveUserLog(ref context, new UserLog(receiveVoucher.RVNo.ToString(), receiveVoucher.RVDate, "RV", ScreenAction.Update));
                        context.SaveChanges();
                    //}
                    //else
                    //{
                    //    return Json(new { Message = 5}, JsonRequestBehavior.AllowGet);
                    //}
                    
                }
                return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteRVById(int id)
        {
            var rvExist = context.ReceiveVouchers.FirstOrDefault(x => x.RVNo == id);
            if (rvExist != null)
            {
                //var transectionE = context.Transection.Where(x => x.DocID == id && x.DocType == "RV").ToList();
                //if (transectionE.Count() == 0) {
                    context.ReceiveVouchers.Remove(rvExist);
                    AccountModuleBridge.DeleteFromReceiveVoucher(ref context, rvExist);
                    UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), rvExist.RVDate, "RV", ScreenAction.Delete));
                    context.SaveChanges();
                    return Json(id, JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                //    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                //}
                
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public FileResult GetReceiveVoucherPreview(int RVNo)
        {
            Session["ReportName"] = "RVPreviewR";
            ReportParmPersister param = new ReportParmPersister();

            var sample = context.ReceiveVouchers.FirstOrDefault(x => x.RVNo == RVNo);
            if (sample != null)
            {
                param.ReceiverName = sample.PayeeName;
                param.RVNo = sample.RVNo;
                param.SampleDate = sample.RVDate;
                if(sample.RefNo != null)
                {
                    param.RefNo = sample.RefNo;
                }
                else
                {
                    param.RefNo = "";
                }
                
            }
            else
            {
                param.ReceiverName = "";
                param.RVNo = 0;
                param.RefNo = "";
            }
            var rvac = context.ChartOfAccount.FirstOrDefault(x => x.Id == sample.ReceiveAccountID);
            if(rvac != null)
            {
                param.ReceiveAccount = rvac.Name;
            }
            else
            {
                param.ReceiveAccount = "";
            }
            var cg = context.EmployeeSection.FirstOrDefault(x => x.ID == sample.CostGroupID);
            if (cg != null)
            {
                param.CostGroup = cg.CGroup;
            }
            else
            {
                param.CostGroup = "";
            }
            var br = context.BranchInformation.FirstOrDefault(x => x.Slno == sample.BranchID);
            if(br != null)
            {
                param.BranchName = br.BrnachName;
            }
            else
            {
                param.BranchName = "";
            }


            

            //param.PostedBy = (from e in context.Employees.AsEnumerable()
            //                  join u in context.UserLog.AsEnumerable() on e.LogInUserName equals u.LogInName
            //                  where u.ScreenName == "ReceiveVoucher" && u.Action == "Save" && u.TrnasId == sample.RVNo.ToString()
            //                  select e.Name).FirstOrDefault();

            //param.ApprovedBy = (from e in context.Employees.AsEnumerable()
            //                  join u in context.UserLog.AsEnumerable() on e.LogInUserName equals u.LogInName
            //                  where u.ScreenName == "ReceiveVoucher" && u.Action == "Approve" && u.TrnasId == sample.RVNo.ToString()
            //                  select e.Name).FirstOrDefault();

            if(param.ApprovedBy==null)
            {
                param.ApprovedBy = "";
            }

            string sql = string.Format("exec ReceiveVoucherPreview '" + RVNo + "'");
            var items = context.Database.SqlQuery<RVPreviewReport>(sql).ToList();
            if (items.Count == 0)
            {
                RVPreviewReport report = new RVPreviewReport();
                items.Add(report);
            }

            RVPreviewR rd = ShowReport(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");

        }

        public RVPreviewR ShowReport(ReportParmPersister persister, List<RVPreviewReport> data)
        {
            RVPreviewR rvPreviewR = new RVPreviewR();

            rvPreviewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\RVPreviewR.rpt");
            rvPreviewR.Load(APPPATH);
            rvPreviewR.SetDataSource(data);

            rvPreviewR.SetParameterValue("receiveAccount", persister.ReceiveAccount);
            rvPreviewR.SetParameterValue("receiverName", persister.ReceiverName);
            rvPreviewR.SetParameterValue("costCenter", persister.BranchName);
            rvPreviewR.SetParameterValue("sampleDate", persister.SampleDate.ToString("dd-MM-yyyy"));
            rvPreviewR.SetParameterValue("costGroup", persister.CostGroup);
            rvPreviewR.SetParameterValue("rvNo", persister.RVNo);
            rvPreviewR.SetParameterValue("refNo", persister.RefNo);

            //rvPreviewR.SetParameterValue("postedBy", "");
            //rvPreviewR.SetParameterValue("approvedBy", "");

            rvPreviewR.SetParameterValue("compName", "");
            rvPreviewR.SetParameterValue("compAddress", "");
            rvPreviewR.SetParameterValue("compContact", "");
            rvPreviewR.SetParameterValue("compFax", "");
            rvPreviewR.SetParameterValue("factAddress", "");
            rvPreviewR.SetParameterValue("factContact", "");
            return rvPreviewR;
        }

        public List<PeyeeName> LoadPayeeName()
        {
            List<PeyeeName> Pl = new List<PeyeeName>();
            var payeeName =  context.ReceiveVouchers.ToList().Select(x => x.PayeeName).Distinct().ToList();
            payeeName.ForEach(x => {
                PeyeeName p = new PeyeeName();
                p.Name = x;
                Pl.Add(p);
            });
            return Pl;
        }
    }

    public class PeyeeName  
    {
        public string Name { get; set; }
    }
}