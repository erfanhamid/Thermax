using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.Bridge;
using BEEERP.Models.CommercialModule.Import;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using BEEERP.Models.Procurement;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace BEEERP.Controllers.Account
{
    [ShowNotification]
    //[Authorize(Roles = "ProAdmin,ProOperator,ProViewer,ProApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    public class SupplierPaymentVoucherController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: SupplierPaymentVoucher
        public ActionResult SupplierPaymentVoucher(int spv = 0, string type = "")
        {
           
            ViewBag.CreditAdvance = LoadComboBox.LoadCreditAdvance();
            ViewBag.SGroup = LoadComboBox.LoadAllSupplierGroup();
            ViewBag.Supplier = LoadComboBox.LoadSupplier(null);
            ViewBag.PaymentAccount = LoadComboBox.LoadReceiveAcc();
            ViewBag.GSB = LoadComboBox.LoadGSBNo(null);
            ViewBag.GPB = LoadComboBox.LoadGPBNo(null);
            ViewBag.GBE = LoadComboBox.LoadGBENo(null);
            ViewBag.gbpgsbfpb = LoadComboBox.LoadGBPGSBFPBB();
            ViewBag.billno = LoadComboBox.LoadGBENo(null);


            ViewBag.AdjustableSPVNo = LoadComboBox.LoadAdjustSPVNo(null);
            if (spv != 0)
            {
                var notificationE = context.Notification.FirstOrDefault(x => x.Type == "RVEntry" && x.TransactionNo == spv.ToString() && x.NotificationDetails == "This Receive Voucher is approved.");
                ViewBag.spvno = spv;
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
            }
            return View();
        }

        public ActionResult GetSupplierByGroupId(int groupId)
        {
            return Json(LoadComboBox.LoadSupplier(groupId), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDueDetailsBySupplierId(int supplierId)
        {
            var gbpgsbfpb = LoadComboBox.LoadGBPGSBFPBB();
            var gsb = LoadComboBox.LoadGSBNo(supplierId);
            var gpb = LoadComboBox.LoadGPBNo(supplierId);
            var fpb = LoadComboBox.LoadFPBNo(supplierId);
            var ilcb = LoadComboBox.LoadILCBNo(supplierId);
            return Json(new { gsb, gpb, fpb, ilcb, gbpgsbfpb }, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult GetDueDetailsByBillType(int suppid)
        //{
        //    var gbpgsbfpb = LoadComboBox.LoadGBPGSBFPBB();
        //    var gsb = LoadComboBox.LoadGSBNo(suppid);
        //    var gpb = LoadComboBox.LoadGPBNo(suppid);
        //    var fpb = LoadComboBox.LoadFPBNo(suppid);
        //    var ilcb = LoadComboBox.LoadILCBNo(suppid);
        //    return Json(new { gsb, gpb, ilcb, gbpgsbfpb, fpb }, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetDueAmount(int supplierId, int ilcbNo)
        {
            if (supplierId.ToString() != "" && ilcbNo.ToString() != "")
            {
                var data = LoadComboBox.GetDueILCBBill(supplierId);
                var due = data.FirstOrDefault(x => x.ILCBNo == ilcbNo).DueAmount;
                return Json(new { val = due }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetGPBAmount(int gpb)
        {
            //var gpbAmountTotal = (from pbl in context.PayBillLines where pbl.BillNo == gpbNo && pbl.BillType == "GPB" select pbl.Amount).ToList().Sum();
            //var remainingBillAmount = ((context.PurchaseEntry.FirstOrDefault(m => m.BillNo == gpbNo).TotalPayable) - gpbAmountTotal);
            ////var remainingBillAmount = gpbOb.TotalPayable - gpbOb.PaidAmount;           
            //return Json(new { gpbAmount = remainingBillAmount }, JsonRequestBehavior.AllowGet);
            return Json(new { gpbAmount = LoadComboBox.GPBDueAmountByGpbNo(gpb) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetGSBAmount(int gsb)
        {
            //var gbeAmountTotal = (from pbl in context.PayBillLines where pbl.BillNo == gbeNo && pbl.BillType == "GBE" select pbl.Amount).ToList().Sum();
            //var remainingBillAmount = ((context.GeneralBills.FirstOrDefault(m => m.GBENo == gbeNo).NetAmount) - gbeAmountTotal);
            //return Json(new { gbeAmount = remainingBillAmount }, JsonRequestBehavior.AllowGet);
            
            return Json(new { gsbAmount = LoadComboBox.GSBDueAmountByGbeNo(gsb) }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetFPBAmount(int fpb)
        {
            //var gbeAmountTotal = (from pbl in context.PayBillLines where pbl.BillNo == gbeNo && pbl.BillType == "GBE" select pbl.Amount).ToList().Sum();
            //var remainingBillAmount = ((context.GeneralBills.FirstOrDefault(m => m.GBENo == gbeNo).NetAmount) - gbeAmountTotal);
            //return Json(new { gbeAmount = remainingBillAmount }, JsonRequestBehavior.AllowGet);
            return Json(new { fpbAmount = LoadComboBox.FPBDueAmountByGbeNo(fpb) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDpAmount(int dpNo)
        {
            decimal remainingBillAmount = LoadComboBox.DPDueAmountByDpNo(dpNo);
            return Json(new { dpAmount = remainingBillAmount }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveSPV(PayBillInfo spvoucher, List<PayBillLine> addedItems)
        {
            string id = "";
            string yearLastTwoPart = spvoucher.TDate.Year.ToString().Substring(2, 2).ToString();
            var noOfSPV = context.PayBillInfo.Count();
            if (noOfSPV > 0)
            {
                var lastSPV = context.PayBillInfo.ToList().LastOrDefault(x => x.PBID.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                if (lastSPV == null)
                {
                    id = yearLastTwoPart + "00001";
                }
                else
                {
                    id = yearLastTwoPart + (Convert.ToInt32(lastSPV.PBID.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                }
            }
            else
            {
                id = yearLastTwoPart + "00001";
            }
            spvoucher.PBID = Convert.ToInt32(id);
            spvoucher.TDate = spvoucher.TDate.Add(DateTime.Now.TimeOfDay);
            if (spvoucher.CreditAdvance == "Advance")
            {
                //PayBillLine pbl = new PayBillLine();
                //pbl.PBID = Convert.ToInt32(id);
                //pbl.BillNo = 0;
                //pbl.BillType = "";
                //pbl.Amount = spvoucher.Amount;

                PayBillAdvance pba = new PayBillAdvance();
                pba.PBID = spvoucher.PBID;
                pba.AdvBillNo =0 /*(Convert.ToInt32(context.PayBillAdvances.Select(x=>x.AdvBillNo).Max())) + 1*/;
                pba.BillType = "ASP";
                pba.BillAmount = 0;
                pba.Balance = 0;
                pba.PaymentAmount =Convert.ToDecimal(spvoucher.Amount);
                pba.Remaining = 0;
                pba.IDP = 0;
                pba.YearPart = 0;
                //try
                //{
                    spvoucher.PaidAmt = spvoucher.Amount;
                    context.PayBillInfo.Add(spvoucher);
                    context.PayBillAdvances.Add(pba);
                    AccountModuleBridge.InsertFromSupplierPayment(ref context, spvoucher, addedItems);
                    SupplierBalanceCalculationBridge.InsertUpdateFromSupplierPaymentVoucher(ref context, spvoucher, new List<PayBillLine>());
                    UserLog.SaveUserLog(ref context, new UserLog(spvoucher.PBID.ToString(), DateTime.Now, "SPV", ScreenAction.Save));
                    context.SaveChanges();
                    return Json(spvoucher, JsonRequestBehavior.AllowGet);
                //}
                //catch (Exception ex)
                //{
                //    return Json(0, JsonRequestBehavior.AllowGet);
                //}
            }
            else
            {
                spvoucher.DocType = addedItems.FirstOrDefault().BillType;
                addedItems.ForEach(x =>
                {
                    if (x.BillType == "ILCB")
                    {
                        InvoicePayment(x.BillNo, x.Amount, spvoucher.PaymentAccId, spvoucher.Description, spvoucher.PBID, spvoucher.DocType);
                    }

                });


                try
                {
                    context.PayBillInfo.Add(spvoucher);
                    addedItems.ForEach(x =>
                    {
                        x.PBID = spvoucher.PBID;
                        x.SPAANo = 0;
                        context.PayBillLines.Add(x);
                        //if (x.BillType=="GPB")
                        //{
                        //    PurchaseEntry purchaseentry = context.PurchaseEntry.Single(y => y.BillNo == x.BillNo);
                        //    var prevbillamount = purchaseentry.PaidAmount;
                        //    var newPayamount = prevbillamount + x.PaymentAmount;
                        //    purchaseentry.PaidAmount = newPayamount;
                        //}
                        //else if (x.BillType == "GSB")
                        //{
                        //    GeneralBill generalbill = context.GeneralBills.Single(y => y.GBENo == x.BillNo);
                        //    var prevbillamount = generalbill.PaidAmount;
                        //    var newPayamount = prevbillamount + x.PaymentAmount;
                        //    generalbill.PaidAmount = newPayamount;
                        //}
                        //else
                        //{
                        //    FPB fpb = context.FPB.Single(y => y.BillNo == x.BillNo);
                        //    var prevbillamount = fpb.PaidAmount;
                        //    var newPayamount = prevbillamount + x.PaymentAmount;
                        //    fpb.PaidAmount = newPayamount;
                        //}
                    });

                    
                    SupplierBalanceCalculationBridge.InsertUpdateFromSupplierPaymentVoucher(ref context, spvoucher, addedItems);
                    AccountModuleBridge.InsertFromSupplierPayment(ref context, spvoucher, addedItems);
                    UserLog.SaveUserLog(ref context, new UserLog(spvoucher.PBID.ToString(), DateTime.Now, "SPV", ScreenAction.Save));
                    context.SaveChanges();
                    return Json(spvoucher, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }

        }




        private void InvoicePayment()
        {
            throw new NotImplementedException();
        }

        public ActionResult GetSPVById(int spv)
        {
            var sPV = context.PayBillInfo.FirstOrDefault(x => x.PBID == spv);
            if (sPV != null)
            {
                if (sPV.CreditAdvance == "Advance")
                {
                    try
                    {
                        //sPV.Amount = context.PayBillAdvances.FirstOrDefault(x => x.PBID == spv).PaymentAmount;
                        return Json(new { sPV }, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception)
                    {
                        return Json(0, JsonRequestBehavior.AllowGet);
                    }
                    
                }
                else
                {
                    var payBillLine = context.PayBillLines.Where(x => x.PBID == sPV.PBID).ToList();
                    foreach (var item in payBillLine)
                    {
                        if (item.BillType == "GSB")
                        {
                            item.Remaining = LoadComboBox.GSBDueAmountByGbeNo(item.BillNo);
                        }
                        else if (item.BillType == "FPB")
                        {
                            item.Remaining = LoadComboBox.FPBDueAmountByGbeNo(item.BillNo);
                        }
                        else if (item.BillType == "GPB")

                        {
                            item.Remaining = LoadComboBox.GPBDueAmountByGpbNo(item.BillNo);
                        }
                        else
                        {

                        }

                    }
                    return Json(new { sPV, payBillLine }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateSPV(PayBillInfo spvoucher, List<PayBillLine> addedItems)
        {
            var paybillInfoE = context.PayBillInfo.FirstOrDefault(x => x.PBID == spvoucher.PBID);
            context.PayBillInfo.Remove(paybillInfoE);

            var paybilllineE = context.PayBillLines.Where(x => x.PBID == spvoucher.PBID).ToList();
            paybilllineE.ForEach(x =>
            {
                context.PayBillLines.Remove(x);
            });


            spvoucher.TDate = spvoucher.TDate.Add(DateTime.Now.TimeOfDay);
            if (spvoucher.CreditAdvance == "Advance")
            {
                var paybillAdvanceE = context.PayBillAdvances.FirstOrDefault(x => x.PBID == spvoucher.PBID);
                context.PayBillAdvances.Remove(paybillAdvanceE);

                PayBillLine pbl = new PayBillLine();
                pbl.PBID = spvoucher.PBID;
                pbl.BillNo = 0;
                pbl.BillType = "";
                pbl.Amount = spvoucher.Amount;

                PayBillAdvance pba = new PayBillAdvance();
                pba.PBID = spvoucher.PBID;
                pba.AdvBillNo = 0;
                pba.BillType = "ASP";
                pba.BillAmount = 0;
                pba.Balance = 0;
                pba.PaymentAmount = spvoucher.Amount;
                pba.Remaining = 0;
                pba.IDP = 0;
                pba.YearPart = 0;
                try
                {
                    spvoucher.PaidAmt = spvoucher.Amount;
                    context.PayBillInfo.Add(spvoucher);
                    //context.PayBillLines.Add(pbl);
                    context.PayBillAdvances.Add(pba);
                    AccountModuleBridge.InsertFromSupplierPayment(ref context, spvoucher, addedItems);
                    SupplierBalanceCalculationBridge.InsertUpdateFromSupplierPaymentVoucher(ref context, spvoucher, new List<PayBillLine>());
                    UserLog.SaveUserLog(ref context, new UserLog(spvoucher.PBID.ToString(), DateTime.Now, "SPV", ScreenAction.Save));
                    context.SaveChanges();
                    return Json(spvoucher, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                spvoucher.DocType = addedItems.FirstOrDefault().BillType;
                addedItems.ForEach(x =>
                {
                    var ILCBExist = context.ILCBSubLdgrLine.Where(y => y.ILCBNo == spvoucher.PBID).ToList();
                    ILCBExist.ForEach(y =>
                    {
                        context.ILCBSubLdgrLine.Remove(y);
                    });

                    if (x.BillType == "ILCB")
                    {
                        InvoicePayment(x.BillNo, x.Amount, spvoucher.PaymentAccId, spvoucher.Description, spvoucher.PBID, spvoucher.DocType);
                    }

                });


                try
                {
                    context.PayBillInfo.Add(spvoucher);
                    addedItems.ForEach(x =>
                    {
                        x.PBID = spvoucher.PBID;
                        x.SPAANo = 0;
                        context.PayBillLines.Add(x);
                    });
                    SupplierBalanceCalculationBridge.InsertUpdateFromSupplierPaymentVoucher(ref context, spvoucher, addedItems);
                    AccountModuleBridge.InsertFromSupplierPayment(ref context, spvoucher, addedItems);
                    UserLog.SaveUserLog(ref context, new UserLog(spvoucher.PBID.ToString(), DateTime.Now, "SPV", ScreenAction.Save));
                    context.SaveChanges();
                    return Json(spvoucher, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult DeleteSPVById(int id)
        {
            var spvExist = context.PayBillInfo.FirstOrDefault(x => x.PBID == id);

            if (spvExist != null)
            {
                context.PayBillInfo.Remove(spvExist);

                var pbLine = context.PayBillLines.Where(x => x.PBID == spvExist.PBID).ToList();
                pbLine.ForEach(x =>
                {
                    context.PayBillLines.Remove(x);
                });

                //if (spvExist.DocType == "ILCB")
                //{
                //    var ILCBExist = context.ILCBSubLdgrLine.Where(x => x.DocNo == spvExist.PBID && x.DocType == "SPV").ToList();
                //    ILCBExist.ForEach(x =>
                //    {
                //        context.ILCBSubLdgrLine.Remove(x);
                //    });
                //}

                if (spvExist.CreditAdvance == "Advance")
                {
                    var pbaExist = context.PayBillAdvances.FirstOrDefault(x => x.PBID == spvExist.PBID);
                    context.PayBillAdvances.Remove(pbaExist);
                }

                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "SPV", ScreenAction.Delete));
                SupplierBalanceCalculationBridge.DeleteFromSupplierPaymentVoucher(ref context, spvExist);
                AccountModuleBridge.DeleteFromSupplierPayment(ref context, spvExist);
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public void InvoicePayment(int invoiceNo, decimal AdjustedAmount, int account, string Description, int spvNo, string DocType)
        {
            BEEContext context = new BEEContext();
            var count = 0;
            decimal Due = 0;
            var Balance = LoadComboBox.GetAllDueSubLedgerByBillId(invoiceNo);
            Balance.ForEach(y =>
            {
                if (count == 0)
                {
                    if (AdjustedAmount > 0)
                    {


                        if (y.DueAmount >= AdjustedAmount)
                        {
                            ILCBSubLdgrLine sl = new ILCBSubLdgrLine();
                            sl.Amount = -AdjustedAmount;
                            sl.Description = Description;
                            sl.DebitAccID = account;
                            sl.SubLedgerID = y.SubLedgerId;
                            sl.ILCBNo = invoiceNo;
                            //sl.DocNo = spvNo;
                            //sl.DocType = "SPV";
                            context.ILCBSubLdgrLine.Add(sl);
                            context.SaveChanges();

                        }
                        else
                        {
                            ILCBSubLdgrLine sl = new ILCBSubLdgrLine();
                            sl.Amount = -y.DueAmount;
                            sl.Description = Description;
                            sl.DebitAccID = account;
                            sl.SubLedgerID = y.SubLedgerId;
                            sl.ILCBNo = invoiceNo;
                            //sl.DocNo = spvNo;
                            //sl.DocType = "SPV";
                            context.ILCBSubLdgrLine.Add(sl);
                            context.SaveChanges();

                            Due = AdjustedAmount - y.DueAmount;
                        }
                    }
                }
                if (count > 0)
                {
                    if (Due > 0)
                    {
                        if (y.DueAmount >= Due)
                        {
                            ILCBSubLdgrLine sl = new ILCBSubLdgrLine();
                            sl.Amount = -Due;
                            sl.Description = Description;
                            sl.DebitAccID = account;
                            sl.SubLedgerID = y.SubLedgerId;
                            sl.ILCBNo = invoiceNo;
                            //sl.DocNo = spvNo;
                            //sl.DocType = "SPV";
                            context.ILCBSubLdgrLine.Add(sl);
                            context.SaveChanges();
                        }
                        else
                        {
                            ILCBSubLdgrLine sl = new ILCBSubLdgrLine();
                            sl.Amount = -y.DueAmount;
                            sl.Description = Description;
                            sl.DebitAccID = account;
                            sl.SubLedgerID = y.SubLedgerId;
                            sl.ILCBNo = invoiceNo;
                            //sl.DocNo = spvNo;
                            //sl.DocType = "SPV";
                            context.ILCBSubLdgrLine.Add(sl);
                            context.SaveChanges();
                            Due = Due - y.DueAmount;
                        }
                    }
                }
                count++;
            });
            context.SaveChanges();
        }

        //public double GetDPDue(int dpNo)
        //{
        //    decimal dpAmountTotal = (from pbl in context.PayBillLines where pbl.BillNo == dpNo && pbl.BillType == "DP" select pbl.Amount).ToList().Sum();
        //    double billAmount = (from d in context.DirectPurchase
        //                         join dl in context.DirectPurchaseLineItem on d.DPNo equals dl.DPNo
        //                         where d.DPNo == dpNo
        //                         select dl.Total).DefaultIfEmpty(0).Sum();
        //    double remainingBillAmount = billAmount - Convert.ToDouble(dpAmountTotal);

        //    return remainingBillAmount;
        //}

        public ActionResult GetSPVoucherPreview(int spvNo)
        {
            ReportParmPersister param = new ReportParmPersister();
            var postedBy = (from p in context.UserLog
                            join c in context.Employees on p.LogInName equals c.LogInUserName
                            where p.ScreenName == "SPV" && p.TrnasId == spvNo.ToString()
                            select c).ToList().FirstOrDefault();
            if (postedBy != null)
            {
                param.PostedBy = postedBy.Name;
            }
            else
            {
                param.PostedBy = "";
            }

            string sql = string.Format("exec spPrintSPV " + spvNo + "");
            var items = context.Database.SqlQuery<SPVoucherPreviewReport>(sql).ToList();
            if (items.Count == 0)
            {
                SPVoucherPreviewReport report = new SPVoucherPreviewReport();
                items.Add(report);
            }
            SPVoucherPreviewR rd = ShowReport(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");
        }

        public SPVoucherPreviewR ShowReport(ReportParmPersister persister, List<SPVoucherPreviewReport> data)
        {
            SPVoucherPreviewR previewR = new SPVoucherPreviewR();

            previewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SPVoucherPreviewR.rpt");
            previewR.Load(APPPATH);
            previewR.SetDataSource(data);
            return previewR;
        }

        public ActionResult GetSPVoucherAdvancePreview(int spvNo)
        {
            ReportParmPersister param = new ReportParmPersister();
            var postedBy = (from p in context.UserLog
                            join c in context.Employees on p.LogInName equals c.LogInUserName
                            where p.ScreenName == "SPV" && p.TrnasId == spvNo.ToString()
                            select c).ToList().FirstOrDefault();
            if (postedBy != null)
            {
                param.PostedBy = postedBy.Name;
            }
            else
            {
                param.PostedBy = "";
            }

            string sql = string.Format("exec spSPVAdvance " + spvNo + "");
            var items = context.Database.SqlQuery<SPAdvancePreviewReport>(sql).ToList();
            if (items.Count == 0)
            {
                SPAdvancePreviewReport report = new SPAdvancePreviewReport();
                items.Add(report);
            }
            SPAdvancePreviewR rd = ShowReportAdvance(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");
        }

        public SPAdvancePreviewR ShowReportAdvance(ReportParmPersister persister, List<SPAdvancePreviewReport> data)
        {
            SPAdvancePreviewR previewR = new SPAdvancePreviewR();

            previewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SPAdvancePreviewR.rpt");
            previewR.Load(APPPATH);
            previewR.SetDataSource(data);
            return previewR;
        }
    }
}