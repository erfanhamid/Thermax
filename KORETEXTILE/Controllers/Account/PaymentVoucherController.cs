using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

namespace BEEERP.Controllers.Account
{
    [ShowNotification]
    //[Permission]
    //[AccountingModule]E:\Projects\LovelloErpNew\trunk\BEEERP\Controllers\Account\PaymentVoucherController.cs
    public class PaymentVoucherController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: PaymentVoucher
        public ActionResult PaymentVoucher(/*string type = "", int pvno = 0*/)    
        {
            ViewBag.Branch = LoadComboBox.LoadBranchInfo();
            ViewBag.CostGroup = LoadComboBox.LoadAllSection();
            ViewBag.CashBankID = LoadComboBox.LoadReceiveAcc();
            ViewBag.DebitAC = LoadComboBox.LoadAllAccountWithoutBankAndCash();
            ViewBag.SubLedger = LoadComboBox.LoadSubLedgerByAccId(null);
            ViewBag.ReceiverName = ReceiverName();


            //if (pvno != 0)
            //{
            //    var notificationE = context.Notification.FirstOrDefault(x => x.Type == "PVEntry" && x.TransactionNo == pvno.ToString() && x.NotificationDetails == "This Payment Voucher is approved.");
            //    ViewBag.PVNo = pvno;
            //    if(notificationE != null)
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

        public ActionResult SavePv(PaymentVoucherOne pv, List<PaymentVoucherOneLineItem> addedItems1, List<PaymentVoucherOneSubLdgrLine> addedItems)
        {
            var ScreenID = context.Screens.FirstOrDefault(x => x.Name == "Payment Voucher").ScreenID;
            var IsExpired = context.ScreenEntryLocks.FirstOrDefault(x => x.ScreenID == ScreenID);

            if (pv.PVDate > IsExpired.SELDate)
            {
                string pvNo = "";
                string yearLastTwoPart = pv.PVDate.Year.ToString().Substring(2, 2).ToString();
                var noOfInvoice = context.PaymentVoucherOne.Count();
                if (noOfInvoice > 0)
                {
                    var lastInvoice = context.PaymentVoucherOne.ToList().LastOrDefault(x => x.PVNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                    
                    if (lastInvoice == null)
                    {
                        pvNo = yearLastTwoPart + "00001";
                    }
                    else
                    {
                        pvNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.PVNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                    }
                }
                else
                {
                    pvNo = yearLastTwoPart + "00001";
                }

                pv.PVNo = Convert.ToInt32(pvNo);
                pv.PVDate = pv.PVDate.Add(DateTime.Now.TimeOfDay);

                addedItems1.ForEach(x =>
                {
                    x.PVNo = pv.PVNo;
                    context.PaymentVoucherOneLineItem.Add(x);
                });

                //for sub ledger line Item
                if (addedItems != null)
                {
                    addedItems.ForEach(x =>
                    {
                        x.PVNo = pv.PVNo;
                        context.PaymentVoucherOneSubLdgrLine.Add(x);
                    });
                }

                context.PaymentVoucherOne.Add(pv);

                //UserNotification notification = new UserNotification();
                //notification.UserId = User.Identity.GetUserId();
                //notification.Type = "PVEntry";
                //notification.TransactionNo = pv.PVNo.ToString();
                //notification.NotificationHead = "New Payment Voucher";
                //notification.NotificationDetails = "This Payment Voucher is pending for approved.";
                //notification.PostingDate = DateTime.Now;
                //notification.BranchId = pv.SalesCenterID;

                //context.Notification.Add(notification);

                AccountModuleBridge.InsertUpdateFromAccPaymentVoucher(ref context, pv, addedItems1);
                UserLog.SaveUserLog(ref context, new UserLog(pv.PVNo.ToString(), pv.PVDate, "PV", ScreenAction.Save));

                context.SaveChanges();
                return Json(new { pv }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(3, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult TransectionEffect(PaymentVoucherOne pv, List<PaymentVoucherOneLineItem> addedItems1, List<PaymentVoucherOneSubLdgrLine> addedItems)
        {
            var paymentV = context.PaymentVoucherOne.AsNoTracking().FirstOrDefault(x => x.PVNo == pv.PVNo);

            if (paymentV != null)
            {
                var notificationE = context.Notification.FirstOrDefault(x => x.Type == "PVEntry" && x.TransactionNo == pv.PVNo.ToString());
                if (notificationE != null)
                {
                    UserNotification notification = new UserNotification();
                    notification.UserId = User.Identity.GetUserId();
                    notification.Type = "PVEntry";
                    notification.TransactionNo = pv.PVNo.ToString();
                    notification.NotificationHead = "New Payment Voucher";
                    notification.NotificationDetails = "This Payment Voucher is approved.";
                    notification.PostingDate = DateTime.Now;
                    notification.BranchId = pv.SalesCenterID;

                    context.Notification.Add(notification);

                    context.ApprovalLog.Where(x => x.NotificationId == notificationE.Id).ToList().ForEach(x => { x.IsApproved = true; context.Entry<ApprovalLog>(x).State = EntityState.Modified; });

                    pv.PVDate = pv.PVDate.Add(DateTime.Now.TimeOfDay);
                    context.Entry<PaymentVoucherOne>(pv).State = EntityState.Modified;
                    addedItems1.ForEach(x =>
                    {
                        x.PVNo = pv.PVNo;
                        context.Entry<PaymentVoucherOneLineItem>(x).State = EntityState.Modified;
                    });
                    if (addedItems != null)
                    {
                        addedItems.ForEach(x =>
                        {
                            x.PVNo = pv.PVNo;
                            context.Entry<PaymentVoucherOneSubLdgrLine>(x).State = EntityState.Modified;
                        });
                    }
                    
                }

                AccountModuleBridge.InsertUpdateFromAccPaymentVoucher(ref context, pv, addedItems1);
                UserLog.SaveUserLog(ref context, new UserLog(pv.PVNo.ToString(), pv.PVDate, "PV", ScreenAction.Approve));
                context.SaveChanges();
                return Json(pv, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetPvById(int id)   
        {
            var pvItem = context.PaymentVoucherOne.FirstOrDefault(x => x.PVNo == id);

            if (pvItem != null)
            {
                var pvLineItem = context.PaymentVoucherOneLineItem.Where(x => x.PVNo == id).ToList().Select(x =>
                {
                    var item = context.PaymentVoucherOneLineItem.FirstOrDefault(y => y.PVNo == x.PVNo && y.DebitAccID == x.DebitAccID);
                    var chartOfAcc = context.ChartOfAccount.FirstOrDefault(z => z.Id == x.DebitAccID);
                    x.DebitAccName = chartOfAcc.Name;
                    var cg = context.EmployeeSection.FirstOrDefault(a => a.ID == x.CostGroupID);
                    if(cg != null)
                    {
                        x.CostGroupName = cg.CGroup;
                    }
                    else
                    {
                        x.CostGroupName = "NA";
                    }
                    
                    return x;
                }).ToList();

                var pvSubledgerLineItem = context.PaymentVoucherOneSubLdgrLine.Where(x => x.PVNo == id).ToList().Select(x =>
                {
                    var item = context.PaymentVoucherOneSubLdgrLine.FirstOrDefault(y => y.PVNo == x.PVNo && y.SubLedgerID == x.SubLedgerID);
                    var subLedger = context.Subledger.FirstOrDefault(z => z.SubLedgerID == x.SubLedgerID);
                    x.SubLedgerName = subLedger.SubLdgerName;
                    return x;
                }).ToList();
                return Json(new { pvItem, pvLineItem, pvSubledgerLineItem }, JsonRequestBehavior.AllowGet); 
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdatePv(PaymentVoucherOne pv, List<PaymentVoucherOneLineItem> addedItems1, List<PaymentVoucherOneSubLdgrLine> addedItems)
        {
            var pvExist = context.PaymentVoucherOne.FirstOrDefault(x => x.PVNo == pv.PVNo);
            if (pvExist != null)
            {
                //var transectionE = context.Transection.Where(x => x.DocType == "pv" && x.DocID == pv.PVNo).ToList();
                //if (transectionE.Count() == 0)
                //{
                    context.PaymentVoucherOne.Remove(pvExist);

                    context.PaymentVoucherOneLineItem.Where(x => x.PVNo == pv.PVNo).ToList().ForEach(x => {
                        context.PaymentVoucherOneLineItem.Remove(x);
                    });

                    //sub ledger remove
                    context.PaymentVoucherOneSubLdgrLine.Where(x => x.PVNo == pv.PVNo).ToList().ForEach(x => {
                        context.PaymentVoucherOneSubLdgrLine.Remove(x);
                    });

                    pv.PVDate = pv.PVDate.Add(DateTime.Now.TimeOfDay);

                    //sub ledger add
                    if (addedItems != null)
                    {
                        addedItems.ForEach(x =>
                        {
                            x.PVNo = pv.PVNo;
                            context.PaymentVoucherOneSubLdgrLine.Add(x);
                        });
                    }

                    addedItems1.ForEach(x =>
                    {
                        x.PVNo = pv.PVNo;
                        context.PaymentVoucherOneLineItem.Add(x);
                    });

                    context.PaymentVoucherOne.Add(pv);
                    AccountModuleBridge.InsertUpdateFromAccPaymentVoucher(ref context, pv, addedItems1);
                    UserLog.SaveUserLog(ref context, new UserLog(pv.PVNo.ToString(), pv.PVDate, "PV", ScreenAction.Update));
                    context.SaveChanges();
                    return Json(new { pv }, JsonRequestBehavior.AllowGet);
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

        public ActionResult DeletePvByid(int id)    
        {
            var pvExist = context.PaymentVoucherOne.FirstOrDefault(x => x.PVNo == id);
            if (pvExist != null)
            {
                //var transectionE = context.Transection.Where(x => x.DocType == "pv" && x.DocID == id).ToList();
                //if (transectionE.Count() == 0)
                //{
                    context.PaymentVoucherOne.Remove(pvExist);
                    context.PaymentVoucherOneLineItem.Where(x => x.PVNo == id).ToList().ForEach(x =>
                    {
                        context.PaymentVoucherOneLineItem.Remove(x);
                    });

                    context.PaymentVoucherOneSubLdgrLine.Where(x => x.PVNo == id).ToList().ForEach(x =>
                    {
                        context.PaymentVoucherOneSubLdgrLine.Remove(x);
                    });
                    UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), pvExist.PVDate, "PV", ScreenAction.Delete));
                    AccountModuleBridge.DeleteFromAccPaymentVoucher(ref context, pvExist);
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

        public FileResult GetPaymentVoucherPreview(int PVNo)
        {
            Session["ReportName"] = "PVPreviewR";
            ReportParmPersister param = new ReportParmPersister();         
            string sql = string.Format("exec PaymentVoucherPreview '" + PVNo + "'");
            var items = context.Database.SqlQuery<PVPreviewReport>(sql).ToList();
            if (items.Count == 0)
            {
                PVPreviewReport report = new PVPreviewReport();
                items.Add(report);
            }
            PVPreviewR rd = ShowReport(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");
        }

        public PVPreviewR ShowReport(ReportParmPersister persister, List<PVPreviewReport> data)
        {
            PVPreviewR pvPreviewR = new PVPreviewR();
            pvPreviewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\PVPreviewR.rpt");
            pvPreviewR.Load(APPPATH);
            pvPreviewR.SetDataSource(data);

            //pvPreviewR.SetParameterValue("compName", persister.CompName);
            //pvPreviewR.SetParameterValue("compAddress", persister.CompAddress);
            //pvPreviewR.SetParameterValue("compContact", persister.CompContact);
            //pvPreviewR.SetParameterValue("compFax", persister.Fax);
            //pvPreviewR.SetParameterValue("factAddress", persister.FactAddress);
            //pvPreviewR.SetParameterValue("factContact", persister.FactContact);
            return pvPreviewR;
        }

        public FileResult GetPaymentVoucherSubPreview(int PVNo)
        {
            Session["ReportName"] = "PVSPreviewR";
            ReportParmPersister param = new ReportParmPersister();
            string sql = string.Format("exec PaymentVoucherSubPreview '" + PVNo + "'");
            var items = context.Database.SqlQuery<PVSPreviewReport>(sql).ToList();
            if (items.Count == 0)
            {
                PVSPreviewReport report = new PVSPreviewReport();
                items.Add(report);
            }
            PVSPreviewR rd = ShowReport( items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");
        }

        public PVSPreviewR ShowReport(List<PVSPreviewReport> data)
        {
            PVSPreviewR pvsPreviewR = new PVSPreviewR();
            pvsPreviewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\PVSPreviewR.rpt");
            pvsPreviewR.Load(APPPATH);
            pvsPreviewR.SetDataSource(data);

            //pvsPreviewR.SetParameterValue("compName", persister.CompName);
            //pvsPreviewR.SetParameterValue("compAddress", persister.CompAddress);
            //pvsPreviewR.SetParameterValue("compContact", persister.CompContact);
            //pvsPreviewR.SetParameterValue("compFax", persister.Fax);
            //pvsPreviewR.SetParameterValue("factAddress", persister.FactAddress);
            //pvsPreviewR.SetParameterValue("factContact", persister.FactContact);
            return pvsPreviewR;
        }
        public List<receiverName> ReceiverName()
        {
            List<receiverName> Pl = new List<receiverName>();
            var payeeName = context.PaymentVoucherOne.ToList().Select(x => x.ReceiverName).Distinct().ToList();
            payeeName.ForEach(x => {
                receiverName p = new receiverName();
                p.Name = x;
                Pl.Add(p);
            });
            return Pl;
        }
        public ActionResult GetSubLedgerByLedger(int accId)
        {
            var item = (from s in context.Subledger
                        join l in context.ChartOfAccount on s.GLID equals l.Id
                        where l.Id == accId
                        select new { s.SubLdgerName, s.SubLedgerID }).ToList();
            var Acctype = context.ChartOfAccount.FirstOrDefault(x => x.Id == accId).RootAccountType;

            if (item.Count() > 0)
            {
                var subLed = LoadComboBox.LoadSubLedgerByAccId(accId);
                return Json(new { subLed, Message = 1, acctype = Acctype }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var subLed = LoadComboBox.LoadSubLedgerByAccId(null);
                return Json(new { subLed, Message = 0, acctype = Acctype }, JsonRequestBehavior.AllowGet);
            }
        }

    }

    public class receiverName
    {
        public string Name { get; set; }
    }
}