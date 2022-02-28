using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using BEEERP.Models.PaymentReceiveInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.SalesModule.Transaction
{
    [ShowNotification]
    public class AdvanceAdjustmentController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: AdvanceAdjustment
        public ActionResult AdvanceAdjustment() 
        {
            ViewBag.DepotId = LoadComboBox.LoadBranchInfo();
            return View();
        }

        public ActionResult GetDueAndAdvanceAmountByCustomerId(int custId, int depot)
        {
            var custExist = context.Customers.FirstOrDefault(x => x.Id == custId && x.DepotId == depot);
            if (custExist != null)
            {
                dynamic items = new List<dynamic>();
                dynamic items1 = new List<dynamic>();
                var saleMain = context.SalesEntryNew.Where(x => x.CustomerID == custId).ToList();
                foreach (var item in saleMain)
                {
                    decimal vaucharAmount = context.PayVouLineItem.Where(x => x.InvoiceNo == item.InvoiceNo).ToList().Select(x => x.AdjustedAmount).DefaultIfEmpty(0).Sum();
                    decimal advanceAdjustment = context.AdvanceAmountLine.Where(x => x.InvoiceNo == item.InvoiceNo).ToList().Select(x => x.AdjustedAmount).DefaultIfEmpty(0).Sum();
                    decimal customerBalanceAdjustment = context.CustomerBalanceAdjustmentLine.Where(x => x.InvoiceNo == item.InvoiceNo).Select(x => x.Amount).DefaultIfEmpty(0).Sum();

                    decimal paidAmount = vaucharAmount + advanceAdjustment + customerBalanceAdjustment;
                    var invoice = context.SalesEntryNew.FirstOrDefault(x => x.InvoiceNo == item.InvoiceNo);
                    decimal saleAmount = invoice.InvoiceAmount - invoice.DiscountAmt + invoice.VatAmount;
                    var dueAmount = saleAmount - paidAmount;
                    if (dueAmount > 0)
                    {
                        var salesItem = new { InvoiceNo = item.InvoiceNo, InvoiceAmount = saleAmount, DueAmount = dueAmount, Date = item.SalesDate };
                        items.Add(salesItem);
                    }
                }

                var recPayInfo = context.ReceivePaymentInfos.Where(x => x.PayMode == "advance" && x.CustomerID == custId).ToList();
                foreach (var item in recPayInfo)
                {
                    var sumOfAAomunt = context.AdvanceAmountLine.Where(x => x.RPID == item.RPID).ToList().Select(x => x.AdjustedAmount).DefaultIfEmpty(0).Sum();
                    if (sumOfAAomunt < item.ReceiveAmt)
                    {
                        var ReceiveAmt = item.ReceiveAmt - sumOfAAomunt;
                        var prvItem = new { item.RPID, ReceiveAmt, item.TDate };
                        items1.Add(prvItem);
                    }
                }


                var custOb = context.CustomerBalanceCalculations.FirstOrDefault(x => x.CustomerID == custId && x.DocumentType == "COB");

                if (custOb != null)
                {
                    decimal oBAdjustment = context.AdvanceAmountLine.Where(x => x.InvoiceNo == custOb.CustomerID).ToList().Select(x => x.AdjustedAmount).DefaultIfEmpty(0).Sum();
                    decimal obAPayvau = (from r in context.ReceivePaymentInfos
                                         join pv in context.PayVouLineItem on
                                         r.RPID equals pv.RPID
                                         where pv.DocType == "OB" && r.CustomerID == custId
                                         select pv.AdjustedAmount).ToList().DefaultIfEmpty(0).Sum();

                    decimal obCustBalAdjust = (from c in context.CustomerBalanceAdjustment join 
                                           cl in context.CustomerBalanceAdjustmentLine on c.CAANo equals cl.CAANo
                                           where cl.InvoiceNo == 0
                                           select cl.Amount).ToList().DefaultIfEmpty(0).Sum();

                    decimal totalAdjustedOb = oBAdjustment + obAPayvau + obCustBalAdjust;
                    decimal remainingOb = custOb.Amount - totalAdjustedOb;
                    if (remainingOb > 0)
                    {
                        var salesItem1 = new { InvoiceNo = custOb.CustomerID, InvoiceAmount = "OB", DueAmount = remainingOb, Date = custOb.TDate };
                        items.Add(salesItem1);
                    }
                }

                return Json(new { item = items, PrvItem = items1 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveAAInfo(List<SelectedInvice> selectedInvoice, List<AdvanceAmount> selectedInvoice1, AdvanceAmountInfo aaInfo)
        {
            try
            {
                using (context)
                {
                    string aANo = "";
                    string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
                    var noOfRPID = context.AdvanceAmountInfo.Count();
                    if (noOfRPID > 0)
                    {
                        var lastnoOfRPID = context.AdvanceAmountInfo.ToList().LastOrDefault(x => x.AANo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                        aANo = yearLastTwoPart + (Convert.ToInt32(lastnoOfRPID.AANo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                    }
                    else
                    {
                        aANo = yearLastTwoPart + "00001";
                    }

                    aaInfo.AANo = Convert.ToInt32(aANo);
                    context.AdvanceAmountInfo.Add(aaInfo);

                    List<AdvanceAmountLineItem> aAline = new List<AdvanceAmountLineItem>();

                    var dueAmount = aaInfo.TotalAmount;
                    var adAmount = aaInfo.TotalAmount;
                    if (adAmount > 0 && dueAmount > 0)
                    {
                        foreach (var item in selectedInvoice)
                        {
                            if (item.DueAmount < dueAmount)
                            {
                                item.DueAmount = item.DueAmount;
                            }
                            else
                            {
                                item.DueAmount = dueAmount;
                            }
                            foreach (var item2 in selectedInvoice1)
                            {
                                if (item2.ReceiveAmt < adAmount)
                                {
                                    item2.ReceiveAmt = item2.ReceiveAmt;
                                }
                                else
                                {
                                    item2.ReceiveAmt = adAmount;
                                }
                                if (item2.ReceiveAmt != 0 && adAmount > 0 && dueAmount > 0)
                                {
                                    if (item.DueAmount > item2.ReceiveAmt)
                                    {
                                        AdvanceAmountLineItem ad = new AdvanceAmountLineItem();
                                        ad.InvoiceNo = item.InvoiceNo;
                                        ad.RPID = item2.RPID;
                                        ad.AdjustedAmount = item2.ReceiveAmt;
                                        ad.AANo = aaInfo.AANo;
                                        aAline.Add(ad);
                                        item.DueAmount = item.DueAmount - item2.ReceiveAmt;
                                        adAmount -= item2.ReceiveAmt;
                                        item2.ReceiveAmt = 0;
                                        continue;
                                    }
                                    else if (item.DueAmount < item2.ReceiveAmt)
                                    {
                                        AdvanceAmountLineItem ad1 = new AdvanceAmountLineItem();
                                        ad1.InvoiceNo = item.InvoiceNo;
                                        ad1.RPID = item2.RPID;
                                        ad1.AdjustedAmount = item.DueAmount;
                                        ad1.AANo = aaInfo.AANo;
                                        aAline.Add(ad1);
                                        item2.ReceiveAmt = item2.ReceiveAmt - item.DueAmount;
                                        dueAmount -= item.DueAmount;
                                        break;
                                    }
                                    else
                                    {
                                        AdvanceAmountLineItem ad2 = new AdvanceAmountLineItem();
                                        ad2.InvoiceNo = item.InvoiceNo;
                                        ad2.RPID = item2.RPID;
                                        ad2.AdjustedAmount = item.DueAmount;
                                        ad2.AANo = aaInfo.AANo;
                                        aAline.Add(ad2);
                                        item2.ReceiveAmt = 0;
                                        dueAmount -= item.DueAmount;
                                    }
                                }
                                else
                                {
                                    continue;
                                }

                            }
                        }
                    }
                    else
                    {
                        return Json(0, JsonRequestBehavior.AllowGet);
                    }
                    aAline.ForEach(x =>
                    {
                        context.AdvanceAmountLine.Add(x);
                    });
                    UserLog.SaveUserLog(ref context, new UserLog(aaInfo.AANo.ToString(), DateTime.Now, "AdvanceAdjustment", ScreenAction.Save));
                    context.SaveChanges();
                    return Json( new { aaNo = aaInfo.AANo }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAAdjustmentByAANo(int aaNo)
        {
            var getAAInfo = context.AdvanceAmountInfo.FirstOrDefault(x => x.AANo == aaNo);

            if (getAAInfo == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<SelectedInvice> selectedInvices = new List<SelectedInvice>();
                List<AdvanceAmount> advanceAmounts = new List<AdvanceAmount>();
                var aaline = context.AdvanceAmountLine.Where(x => x.AANo == aaNo).ToList();
                var sameInvoiceDue = aaline.GroupBy(x => x.InvoiceNo).Select(y => new { InvoiceNo = y.First().InvoiceNo, AdjustedAmount = y.Sum(z => z.AdjustedAmount) }).ToList();
                var sameRpidAmount = aaline.GroupBy(x => x.RPID).Select(y => new { RPID = y.First().RPID, AdjustedAmount = y.Sum(z => z.AdjustedAmount) }).ToList();
                sameInvoiceDue.ForEach(x =>
                {
                    SelectedInvice si = new SelectedInvice();
                    si.InvoiceNo = x.InvoiceNo;
                    si.DueAmount = x.AdjustedAmount;
                    var invoice = context.SalesEntryNew.FirstOrDefault(y => y.InvoiceNo == x.InvoiceNo);
                    if (invoice != null)
                    {
                        decimal saleAmount = invoice.InvoiceAmount - invoice.DiscountAmt + invoice.VatAmount;
                        si.InvoiceAmount = saleAmount.ToString();
                        si.Date = invoice.SalesDate;
                    }
                    else
                    {
                        var CustBaCal = context.CustomerBalanceCalculations.FirstOrDefault(y => y.CustomerID == x.InvoiceNo && y.DocumentType == "COB");
                        si.InvoiceAmount = "OB";
                        si.Date = CustBaCal.TDate;
                    }
                    
                    selectedInvices.Add(si);
                });

                sameRpidAmount.ForEach(x =>
                {
                    AdvanceAmount aA = new AdvanceAmount();
                    aA.RPID = x.RPID;
                    aA.ReceiveAmt = x.AdjustedAmount;
                    var item = context.ReceivePaymentInfos.FirstOrDefault(y => y.RPID == x.RPID);
                    aA.Date = item.TDate;
                    advanceAmounts.Add(aA);
                });

                return Json(new { SelectedInvices = selectedInvices, AdvanceAmounts = advanceAmounts, GetAAInfo = getAAInfo }, JsonRequestBehavior.AllowGet);   
            }
            
        }

        public ActionResult DeleteAdvAdjustmentByid(int id)
        {
            var adAdjustExist = context.AdvanceAmountInfo.FirstOrDefault(x => x.AANo == id);    
            if (adAdjustExist != null)
            {
                context.AdvanceAmountInfo.Remove(adAdjustExist);
                var adAdjustFgLineItem = context.AdvanceAmountLine.Where(x => x.AANo == id).ToList();
                adAdjustFgLineItem.ForEach(x =>
                {
                    context.AdvanceAmountLine.Remove(x);
                });
                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "AdvanceAdjustment", ScreenAction.Delete));
                //InventoryTransactionBridge.InsertFromIFGSEntry(ref context, issuFgLineItem, ifgsExist);
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }

            return Json(0, JsonRequestBehavior.AllowGet);
        }
    }
    public sealed class SelectedInvice
    {
        public int InvoiceNo { get; set; }
        public string InvoiceAmount { get; set; }
        public decimal DueAmount { get; set; }
        public DateTime Date { get; set; }
    }

    public sealed class AdvanceAmount
    {
        public int RPID { get; set; }
        public decimal ReceiveAmt { get; set; }
        public DateTime Date { get; set; }
    }
}