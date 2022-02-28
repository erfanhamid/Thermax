using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using BEEERP.Models.AccountModule;
using BEEERP.Models.Log;
using BEEERP.Models.Bridge;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.CrystalReport.ReportFormat;
using System.IO;
using BEEERP.Models.Notification;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace BEEERP.Controllers.Account
{
    [Permission]
    [AccountingModule]
    [ShowNotification]
    public class JournalVoucherController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: JournalVoucher
        public ActionResult JournalVoucher(/*string type = "", int jvno = 0*/)
        {
            //string jvNo = "";
            //string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
            //var noOfInvoice = context.JournalVoucherOne.Count();
            //if (noOfInvoice > 0)
            //{
            //    var lastInvoice = context.JournalVoucherOne.ToList().LastOrDefault(x => x.JVNO.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
            //    if (lastInvoice == null)
            //    {
            //        jvNo = yearLastTwoPart + "00001";
            //    }
            //    else
            //    {
            //        jvNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.JVNO.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
            //    }

            //}
            //else
            //{
            //    jvNo = yearLastTwoPart + "00001";
            //}

            //ViewBag.JVNO = Convert.ToInt32(jvNo);

            ViewBag.Branch = LoadComboBox.LoadBranchInfo();
            ViewBag.CostGroup = LoadComboBox.LoadAllSection();
            ViewBag.DebitOrCredit = LoadComboBox.LoadDebitOrCredit();
            ViewBag.GlAccount = LoadComboBox.LoadAllAccount();
            ViewBag.SubLedger = LoadComboBox.LoadSubLedgerByAccId(null);
            //ViewBag.BussinessUnit = LoadComboBox.LoadBussinessUnit();
            //ViewBag.Jobs = LoadComboBox.LoadAllJobs(0);
            ViewBag.DepotId = ScreenSessionData.GetEmployeeDepot();

            //if (jvno != 0)
            //{
            //    var notificationE = context.Notification.FirstOrDefault(x => x.Type == "JVEntry" && x.TransactionNo == jvno.ToString() && x.NotificationDetails == "This Journal Voucher is approved.");
            //    ViewBag.JVNo = jvno;
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

        public ActionResult GetSubLedgerByLedger(int accId)
        {
            var item = (from s in context.Subledger
                        join l in context.ChartOfAccount on s.GLID equals l.Id
                        where l.Id == accId
                        select new { s.SubLdgerName, s.SubLedgerID }).ToList();

            if (item.Count() > 0)
            {
                var subLed = LoadComboBox.LoadSubLedgerByAccId(accId);
                return Json(new { subLed, Message = 1 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var subLed = LoadComboBox.LoadSubLedgerByAccId(null);
                return Json(new { subLed, Message = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveJv(JournalVoucherOne jv, List<JournalVoucherOneLineItem> addedItems1, List<JVSubLedgerLine> addedItems)
        {
            var ScreenID = context.Screens.FirstOrDefault(x => x.Name == "Journal Voucher").ScreenID;
            var IsExpired = context.ScreenEntryLocks.FirstOrDefault(x => x.ScreenID == ScreenID);

            if (jv.JVDate > IsExpired.SELDate)
            {
                string jvNo = "";
                string yearLastTwoPart = jv.JVDate.Year.ToString().Substring(2, 2).ToString();
                var noOfInvoice = context.JournalVoucherOne.Count();
                if (noOfInvoice > 0)
                {
                    var lastInvoice = context.JournalVoucherOne.ToList().LastOrDefault(x => x.JVNO.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                    if (lastInvoice == null)
                    {
                        jvNo = yearLastTwoPart + "00001";
                    }
                    else
                    {
                        jvNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.JVNO.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                    }

                }
                else
                {
                    jvNo = yearLastTwoPart + "00001";
                }

                jv.JVNO = Convert.ToInt32(jvNo);
                jv.JVDate = jv.JVDate.Add(DateTime.Now.TimeOfDay);

                addedItems1.ForEach(x =>
                {
                    x.JVNO = jv.JVNO;
                    context.JournalVoucherOneLineItem.Add(x);
                });

                //for sub ledger line Item
                if (addedItems != null)
                {
                    addedItems.ForEach(x =>
                    {
                        x.JVNO = jv.JVNO;
                        context.JVSubLedgerLine.Add(x);
                    });
                }

                context.JournalVoucherOne.Add(jv);

                //UserNotification notification = new UserNotification();
                //notification.UserId = User.Identity.GetUserId();
                //notification.Type = "JVEntry";
                //notification.TransactionNo = jv.JVNO.ToString();
                //notification.NotificationHead = "New Journal Voucher";
                //notification.NotificationDetails = "This Journal Voucher is pending for approved.";
                //notification.PostingDate = DateTime.Now;
                //notification.BranchId = jv.BranchId;

                //context.Notification.Add(notification);

                AccountModuleBridge.InsertUpdateFromJournalVoucher(ref context, jv, addedItems1, addedItems);
                UserLog.SaveUserLog(ref context, new UserLog(jv.JVNO.ToString(), jv.JVDate, "JV", ScreenAction.Save));

                context.SaveChanges();
                return Json(new { jv }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(3, JsonRequestBehavior.AllowGet);
            }
        }

        //public ActionResult TransectionEffect(JournalVoucherOne jv, List<JournalVoucherOneLineItem> addedItems1, List<JVSubLedgerLine> addedItems)
        //{
        //    var journalV = context.JournalVoucherOne.AsNoTracking().FirstOrDefault(x => x.JVNO == jv.JVNO);

        //    if (journalV != null)
        //    {
        //        var notificationE = context.Notification.FirstOrDefault(x => x.Type == "JVEntry" && x.TransactionNo == jv.JVNO.ToString());
        //        if (notificationE != null)
        //        {
        //            UserNotification notification = new UserNotification();
        //            notification.UserId = User.Identity.GetUserId();
        //            notification.Type = "JVEntry";
        //            notification.TransactionNo = jv.JVNO.ToString();
        //            notification.NotificationHead = "New Journal Voucher";
        //            notification.NotificationDetails = "This Journal Voucher is approved.";
        //            notification.PostingDate = DateTime.Now;
        //            notification.BranchId = jv.BranchId;

        //            context.Notification.Add(notification);
        //            //notificationE.NotificationDetails = "This Journal Voucher is approved.";
        //            //notificationE.UserId = User.Identity.GetUserId();
        //            //context.Entry<UserNotification>(notificationE).State = EntityState.Modified;

        //            context.ApprovalLog.Where(x => x.NotificationId == notificationE.Id).ToList().ForEach(x => { x.IsApproved = true; context.Entry<ApprovalLog>(x).State = EntityState.Modified; });

        //            jv.JVDate = jv.JVDate.Add(DateTime.Now.TimeOfDay);
        //            context.Entry<JournalVoucherOne>(jv).State = EntityState.Modified;
        //            addedItems1.ForEach(x =>
        //            {
        //                x.JVNO = jv.JVNO;
        //                context.Entry<JournalVoucherOneLineItem>(x).State = EntityState.Modified;
        //            });
        //            if (addedItems != null)
        //            {
        //                addedItems.ForEach(x =>
        //                {
        //                    x.JVNO = jv.JVNO;
        //                    context.Entry<JVSubLedgerLine>(x).State = EntityState.Modified;
        //                });
        //            }

        //        }

        //        AccountModuleBridge.InsertUpdateFromJournalVoucher(ref context, jv, addedItems1, addedItems);
        //        UserLog.SaveUserLog(ref context, new UserLog(jv.JVNO.ToString(), DateTime.Now, "JournalVoucher", ScreenAction.Approve));
        //        context.SaveChanges();
        //        return Json(jv, JsonRequestBehavior.AllowGet);
        //    }

        //    else
        //    {
        //        return Json(0, JsonRequestBehavior.AllowGet);
        //    }
        //}

        public ActionResult GetJvById(int id)
        {
            var jvItem = context.JournalVoucherOne.FirstOrDefault(x => x.JVNO == id);

            if (jvItem != null)
            {
                var jvLineItem = context.JournalVoucherOneLineItem.Where(x => x.JVNO == id).ToList().Select(x =>
                {
                    var item = context.JournalVoucherOneLineItem.FirstOrDefault(y => y.JVNO == x.JVNO && y.AccountHeadID == x.AccountHeadID);
                    var chartOfAcc = context.ChartOfAccount.FirstOrDefault(z => z.Id == x.AccountHeadID);
                    x.AccountHeadName = chartOfAcc.Name;
                    x.CostGroupName = context.EmployeeSection.FirstOrDefault(m => m.ID == x.CostGroupId).CGroup;
                    return x;
                }).ToList();

                var jvSubledgerLineItem = context.JVSubLedgerLine.Where(x => x.JVNO == id).ToList().Select(x =>
                {
                    var item = context.JVSubLedgerLine.FirstOrDefault(y => y.JVNO == x.JVNO && y.SubLedgerID == x.SubLedgerID);
                    var subLedger = context.Subledger.FirstOrDefault(z => z.SubLedgerID == x.SubLedgerID);
                    x.SubLedgerName = subLedger.SubLdgerName;
                    var cg = context.EmployeeSection.FirstOrDefault(m => m.ID == x.CostGroupId);
                    if(cg != null)
                    {
                        x.CostGroupName = cg.CGroup;
                    }
                    else
                    {
                        x.CostGroupName = "";
                    }
                   
                    return x;
                }).ToList();
                return Json(new { jvItem, jvLineItem, jvSubledgerLineItem }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateJv(JournalVoucherOne jv, List<JournalVoucherOneLineItem> addedItems1, List<JVSubLedgerLine> addedItems)
        {
            var jvExist = context.JournalVoucherOne.FirstOrDefault(x => x.JVNO == jv.JVNO);
            if (jvExist != null)
            {
                //var transectionE = context.Transection.Where(x => x.DocType == "jv" && x.DocID == jv.JVNO).ToList();
                //if (transectionE.Count() == 0)
                //{
                context.JournalVoucherOne.Remove(jvExist);

                context.JournalVoucherOneLineItem.Where(x => x.JVNO == jv.JVNO).ToList().ForEach(x => {
                    context.JournalVoucherOneLineItem.Remove(x);
                });

                jv.JVDate = jv.JVDate.Add(DateTime.Now.TimeOfDay);

                addedItems1.ForEach(x =>
                {
                    x.JVNO = jv.JVNO;
                    context.JournalVoucherOneLineItem.Add(x);
                });
                context.JVSubLedgerLine.Where(x => x.JVNO == jv.JVNO).ToList().ForEach(x => {
                    context.JVSubLedgerLine.Remove(x);
                });
                //sub ledger add
                if (addedItems != null)
                {
                    addedItems.ForEach(x =>
                    {
                        x.JVNO = jv.JVNO;
                        context.JVSubLedgerLine.Add(x);
                    });
                }

                context.JournalVoucherOne.Add(jv);

                AccountModuleBridge.InsertUpdateFromJournalVoucher(ref context, jv, addedItems1, addedItems);
                UserLog.SaveUserLog(ref context, new UserLog(jv.JVNO.ToString(), jv.JVDate, "JV", ScreenAction.Update));
                context.SaveChanges();
                return Json(new { jv }, JsonRequestBehavior.AllowGet);
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

        public ActionResult DeleteJvByid(int id)
        {
            var jvExist = context.JournalVoucherOne.FirstOrDefault(x => x.JVNO == id);
            if (jvExist != null)
            {
                //var transectionE = context.Transection.Where(x => x.DocType == "jv" && x.DocID == id).ToList();
                //if (transectionE.Count() == 0)
                //{
                    context.JournalVoucherOne.Remove(jvExist);
                    context.JournalVoucherOneLineItem.Where(x => x.JVNO == id).ToList().ForEach(x =>
                    {
                        context.JournalVoucherOneLineItem.Remove(x);
                    });

                    AccountModuleBridge.DeleteFromJournalVoucher(ref context, jvExist);
                    UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), jvExist.JVDate, "JV", ScreenAction.Delete));
                    context.SaveChanges();
                    return Json(id, JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                //    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                //}

            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        public FileResult GetJournalVoucherPreview(int JVNO)
        {
            Session["ReportName"] = "JVPreviewR";
            ReportParmPersister param = new ReportParmPersister();
            string sql = string.Format("exec PreviewJournalVoucher '" + JVNO + "'");
            var items = context.Database.SqlQuery<JVPreviewReport>(sql).ToList();
            if (items.Count == 0)
            {
                JVPreviewReport report = new JVPreviewReport();
                items.Add(report);
            }
            JVPreviewR rd = ShowReport(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");
        }

        public JVPreviewR ShowReport(ReportParmPersister persister, List<JVPreviewReport> data)
        {
            JVPreviewR jvPreviewR = new JVPreviewR();
            jvPreviewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\JVPreviewR.rpt");
            jvPreviewR.Load(APPPATH);
            jvPreviewR.SetDataSource(data);

            //jvPreviewR.SetParameterValue("compName", persister.CompName);
            //jvPreviewR.SetParameterValue("compAddress", persister.CompAddress);
            //jvPreviewR.SetParameterValue("compContact", persister.CompContact);
            //jvPreviewR.SetParameterValue("compFax", persister.Fax);
            //jvPreviewR.SetParameterValue("factAddress", persister.FactAddress);
            //jvPreviewR.SetParameterValue("factContact", persister.FactContact);
            return jvPreviewR;
        }



        public FileResult GetJournalVoucherSubLedgerPreview(int JVNO)
        {
            Session["ReportName"] = "JVSLPreviewR";
            ReportParmPersister param = new ReportParmPersister();
            string sql = string.Format("exec PreviewJournalVoucherSubLedger '" + JVNO + "'");
            var items = context.Database.SqlQuery<JVSLPreviewReport>(sql).ToList();
            if (items.Count == 0)
            {
                JVSLPreviewReport report = new JVSLPreviewReport();
                items.Add(report);
            }
            JVSLPreviewR rd = ShowReport(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");
        }

        public JVSLPreviewR ShowReport(ReportParmPersister persister, List<JVSLPreviewReport> data)
        {
            JVSLPreviewR jvslPreviewR = new JVSLPreviewR();
            jvslPreviewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\JVSLPreviewR.rpt");
            jvslPreviewR.Load(APPPATH);
            jvslPreviewR.SetDataSource(data);

            //jvslPreviewR.SetParameterValue("compName", persister.CompName);
            //jvslPreviewR.SetParameterValue("compAddress", persister.CompAddress);
            //jvslPreviewR.SetParameterValue("compContact", persister.CompContact);
            //jvslPreviewR.SetParameterValue("compFax", persister.Fax);
            //jvslPreviewR.SetParameterValue("factAddress", persister.FactAddress);
            //jvslPreviewR.SetParameterValue("factContact", persister.FactContact);
            return jvslPreviewR;
        }


    }
}