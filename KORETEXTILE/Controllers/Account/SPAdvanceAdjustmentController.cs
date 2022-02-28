using BEEERP.Models.AccountModule;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using BEEERP.Models.Procurement;
using BEEERP.Models.ViewModel.CommercialVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Account
{
    [ShowNotification]
    public class SPAdvanceAdjustmentController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: SPAdvanceAdjustment
        public ActionResult SPAdvanceAdjustment()
        {
            ViewBag.SGroup = LoadComboBox.LoadAllSupplierGroup();
            ViewBag.Supplier = LoadComboBox.LoadSupplier(null);
            ViewBag.DepotId = LoadComboBox.LoadBranchInfo();
            return View();
        }

        public ActionResult GetDueAndAdvanceAmountBySupplierId(int supplierId)
        {
            var supplier = context.Supplier.FirstOrDefault(x => x.Id == supplierId);
            if (supplier != null)
            {
                dynamic items = new List<dynamic>();
                dynamic items1 = new List<dynamic>();

                var gbe = context.GeneralBills.Where(x => x.SupplierId == supplierId).ToList();

                foreach (var item in gbe)
                {
                    //decimal voucharAmount = context.PayBillLines.Where(x => x.BillNo == item.GBENo && x.BillType=="GBE").ToList().Select(x => x.Amount).DefaultIfEmpty(0).Sum();
                    //decimal advanceAdjustment = context.SPAdvanceAmountLineItems.Where(x => x.DocNo == item.GBENo && x.DocType == "GBE").ToList().Select(x => x.AdjustedAmount).DefaultIfEmpty(0).Sum();
                    //decimal dueAmount = item.NetAmount - voucharAmount - advanceAdjustment;
                    //decimal dueAmount = LoadComboBox.GBEDueAmountByGbeNo(item.GBENo);
                    decimal dueAmount = LoadComboBox.GSBDueAmountByGbeNo(item.GBENo);

                    if (dueAmount > 0)
                    {
                        var supplyItem = new { DocNo = item.GBENo, TotalAmount = item.NetAmount, DueAmount = dueAmount, DocType = "GSB", Date = item.GBEDate };
                        items.Add(supplyItem);
                    }
                }

                var gpb = context.PurchaseEntry.Where(x => x.SupplierId == supplierId).ToList();

                foreach (var item in gpb)
                {
                    //decimal voucharAmount = context.PayBillLines.Where(x => x.BillNo == item.BillNo && x.BillType == "GPB").ToList().Select(x => x.Amount).DefaultIfEmpty(0).Sum();
                    //decimal advanceAdjustment = context.SPAdvanceAmountLineItems.Where(x => x.DocNo == item.BillNo && x.DocType == "GPB").ToList().Select(x => x.AdjustedAmount).DefaultIfEmpty(0).Sum();
                    //decimal dueAmount = item.TotalPayable - voucharAmount - advanceAdjustment;
                    decimal dueAmount = LoadComboBox.GPBDueAmountByGpbNo(item.BillNo);

                    if (dueAmount > 0)
                    {
                        var supplyItem = new { DocNo = item.BillNo, TotalAmount = item.TotalPayable, DueAmount = dueAmount, DocType = "GPB", Date = item.PDate };
                        items.Add(supplyItem);
                    }
                }

                //var dp = context.DirectPurchase.Where(x => x.SupplierId == supplierId).ToList();

                //foreach (var item in dp)
                //{
                //    //decimal dpAmountTotal = (from pbl in context.PayBillLines where pbl.BillNo == item.DPNo && pbl.BillType == "DP" select pbl.Amount).ToList().Sum();
                //    //double billAmount = (from d in context.DirectPurchase
                //    //                     join dl in context.DirectPurchaseLineItem on d.DPNo equals dl.DPNo
                //    //                     where d.DPNo == item.DPNo
                //    //                     select dl.Total).DefaultIfEmpty(0).Sum();
                //    //double dueAmount = billAmount - Convert.ToDouble(dpAmountTotal);

                //    decimal dueAmount = LoadComboBox.DPDueAmountByDpNo(item.DPNo);

                //    //if (dueAmount > 0)
                //    //{
                //    //    var supplyItem = new { DocNo = item.DPNo, TotalAmount = item.TotalAmount, DueAmount = dueAmount, DocType = "DP", Date = item.DPDate };
                //    //    items.Add(supplyItem);
                //    //}
                //}

                //code fro ilcb
                var ilcb = context.ILCB.Where(x => x.SupplierID == supplierId).ToList();
                ilcb.ForEach(x =>
                {
                    List<DueILCBBill> bills = LoadComboBox.GetDueILCBBill(x.SupplierID).ToList();
                    bills.ForEach(y =>
                    {
                        if (x.ILCBNo == y.ILCBNo)
                        {
                            decimal dueAmount = y.DueAmount;
                            if (dueAmount > 0)
                            {
                                var supplyItem = new { DocNo = x.ILCBNo, TotalAmount = x.BillTotalValue, DueAmount = dueAmount, DocType = "ILCB", Date = x.Date };
                                items.Add(supplyItem);
                            }
                        }

                    });

                });

                var advInfo = context.PayBillInfo.Where(x => x.CreditAdvance == "Advance" && x.SupplierId == supplierId).ToList();
                foreach (var item in advInfo)
                {
                    var sumOfAAomunt = context.PayBillLines.Where(x => x.PBID == item.PBID).ToList().Select(x => x.PaymentAmount).DefaultIfEmpty(0).Sum();
                   // var sumOfAAomunt = context.PayBillAdvances.Where(x => x.PBID == item.PBID).ToList().Select(x => x.AdjustedAmount).DefaultIfEmpty(0).Sum();
                    if (sumOfAAomunt < item.PaidAmt)
                    {
                        var amount = item.PaidAmt - sumOfAAomunt;
                        var advance = new { SPVNo = item.PBID, Amount = amount, Date = item.TDate };
                        items1.Add(advance);
                    }

                }

                var supOB = context.SupplierBalanceCalculation.FirstOrDefault(x => x.SupplierID == supplierId && x.DocumentType == "SOB");
                if (supOB != null)
                {
                    decimal oBAdjustment = context.SPAdvanceAmountLineItems.Where(x => x.DocNo == supplierId && x.DocType == "OB").ToList().Select(x => x.AdjustedAmount).DefaultIfEmpty(0).Sum();

                    decimal obAPayvau = (from r in context.PayBillInfo
                                         join pv in context.PayBillLines on
                                         r.PBID equals pv.PBID
                                         where pv.BillType == "OB" && r.SupplierId == supplierId
                                         select pv.Amount).ToList().DefaultIfEmpty(0).Sum();

                    decimal totalAdjustedOb = oBAdjustment + obAPayvau;
                    decimal DueOb = supOB.Amount - totalAdjustedOb;
                    if (DueOb > 0)
                    {
                        var supplyItem = new { DocNo = supOB.SupplierID, TotalAmount = supOB.Amount, DueAmount = DueOb, DocType = "OB", Date = supOB.TDate };
                        items.Add(supplyItem);
                    }
                }
                return Json(new { item = items, adv = items1, Name = supplier.SupplierName, SupplierDue = FindObjectById.SupplierDueAmount(supplier.Id) }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        //public ActionResult SaveSPAdvanceAdjustment(List<SelectedBill> selectedBill, List<AdvanceAmount> selectedAdvance, SPAdvanceAdjustment sPAInfo)
        //{
        //    //try
        //    //{
        //    //    using (context)
        //    //    {
        //    string spaANo = "";
        //    string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
        //    var noOfSPAA = context.SPAdvanceAdjustments.Count();
        //    if (noOfSPAA > 0)
        //    {
        //        var lastAA = context.SPAdvanceAdjustments.ToList().LastOrDefault(x => x.SPAANo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
        //        if (lastAA == null)
        //        {
        //            spaANo = yearLastTwoPart + "00001";
        //        }
        //        else
        //        {
        //            spaANo = yearLastTwoPart + (Convert.ToInt32(lastAA.SPAANo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
        //        }
        //    }
        //    else
        //    {
        //        spaANo = yearLastTwoPart + "00001";
        //    }
        //    sPAInfo.SPAANo = Convert.ToInt32(spaANo);
        //    sPAInfo.Date = sPAInfo.Date.Add(DateTime.Now.TimeOfDay);
        //    context.SPAdvanceAdjustments.Add(sPAInfo);

        //    List<SPAdvanceAmountLineItem> sPAAline = new List<SPAdvanceAmountLineItem>();

        //    var dueAmount = sPAInfo.AdjustedAmount;
        //    var advAmount = sPAInfo.AdjustedAmount;
        //    if (advAmount > 0 && dueAmount > 0)
        //    {
        //        foreach (var item in selectedBill)
        //        {
        //            if (item.DueAmount < dueAmount)
        //            {
        //                item.DueAmount = item.DueAmount;
        //            }
        //            else
        //            {
        //                item.DueAmount = dueAmount;
        //            }
        //            foreach (var item2 in selectedAdvance)
        //            {
        //                if (item2.Amount < advAmount)
        //                {
        //                    item2.Amount = item2.Amount;
        //                }
        //                else
        //                {
        //                    item2.Amount = advAmount;
        //                }
        //                if (item2.Amount != 0 && advAmount > 0 && dueAmount > 0)
        //                {
        //                    if (item.DueAmount > item2.Amount)
        //                    {
        //                        SPAdvanceAmountLineItem spa = new SPAdvanceAmountLineItem();
        //                        spa.SPAANo = sPAInfo.SPAANo;
        //                        spa.DocNo = item.DocNo;
        //                        spa.DocType = item.DocType;
        //                        spa.SPVNo = item2.SPVNo;
        //                        spa.AdjustedAmount = item2.Amount;
        //                        sPAAline.Add(spa);
        //                        item.DueAmount = item.DueAmount - item2.Amount;
        //                        advAmount -= item2.Amount;
        //                        item2.Amount = 0;
        //                        continue;
        //                    }
        //                    else if (item.DueAmount < item2.Amount)
        //                    {
        //                        SPAdvanceAmountLineItem spa1 = new SPAdvanceAmountLineItem();
        //                        spa1.SPAANo = sPAInfo.SPAANo;
        //                        spa1.DocNo = item.DocNo;
        //                        spa1.DocType = item.DocType;
        //                        spa1.SPVNo = item2.SPVNo;
        //                        spa1.AdjustedAmount = item.DueAmount;
        //                        sPAAline.Add(spa1);
        //                        item2.Amount = item2.Amount - item.DueAmount;
        //                        dueAmount -= item.DueAmount;
        //                        break;
        //                    }
        //                    else
        //                    {
        //                        SPAdvanceAmountLineItem spa2 = new SPAdvanceAmountLineItem();
        //                        spa2.SPAANo = sPAInfo.SPAANo;
        //                        spa2.DocNo = item.DocNo;
        //                        spa2.DocType = item.DocType;
        //                        spa2.SPVNo = item2.SPVNo;
        //                        spa2.AdjustedAmount = item.DueAmount;
        //                        sPAAline.Add(spa2);
        //                        item2.Amount = 0;
        //                        dueAmount -= item.DueAmount;
        //                    }
        //                }
        //                else
        //                {
        //                    continue;
        //                }

        //            }
        //        }
        //    }
        //    else
        //    {
        //        return Json(0, JsonRequestBehavior.AllowGet);
        //    }
        //    sPAAline.ForEach(x =>
        //    {
        //        context.SPAdvanceAmountLineItems.Add(x);
        //    });

        //    UserLog.SaveUserLog(ref context, new UserLog(sPAInfo.SPAANo.ToString(), DateTime.Now, "SPAdvanceAdjustment", ScreenAction.Save));
        //    context.SaveChanges();
        //    return Json(new { aaNo = sPAInfo.SPAANo }, JsonRequestBehavior.AllowGet);
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    return Json(0, JsonRequestBehavior.AllowGet);
        //    //}
        //}

        public ActionResult SaveSPAdvanceAdjustment(List<SelectedBill> selectedBill, AdvanceAmount selectedAdvance, SPAdvanceAdjustment sPAInfo)
        {
            //try
            //{
            //    using (context)
            //    {
            string spaANo = "";
            string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
            var noOfSPAA = context.SPAdvanceAdjustments.Count();
            if (noOfSPAA > 0)
            {
                var lastAA = context.SPAdvanceAdjustments.ToList().LastOrDefault(x => x.SPAANo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                if (lastAA == null)
                {
                    spaANo = yearLastTwoPart + "00001";
                }
                else
                {
                    spaANo = yearLastTwoPart + (Convert.ToInt32(lastAA.SPAANo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                }
            }
            else
            {
                spaANo = yearLastTwoPart + "00001";
            }
            sPAInfo.SPAANo = Convert.ToInt32(spaANo);
            sPAInfo.Date = sPAInfo.Date.Add(DateTime.Now.TimeOfDay);
            //context.SPAdvanceAdjustments.Add(sPAInfo);

            //List<SPAdvanceAmountLineItem> sPAAline = new List<SPAdvanceAmountLineItem>();
            List<PayBillLine> sPAAline = new List<PayBillLine>();

            var dueAmount = sPAInfo.AdjustedAmount;
            var advAmount = sPAInfo.AdjustedAmount;

            //selectedBill.ForEach(x =>
            //{
            //    PayBillLine spa2 = new PayBillLine();
            //    spa2.PBID =selectedAdvance ;
            //    spa2.BillNo = item.DocNo;
            //    spa2.BillType = item.DocType;
            //    spa2.Amount = 0;
            //    spa2.Balance = 0;
            //    spa2.Remaining = 0;
            //    spa2.PaymentAmount = item2.Amount;
            //    sPAAline.Add(spa2);
            //    context.PayBillLines.Add(x);
            //});

            UserLog.SaveUserLog(ref context, new UserLog(sPAInfo.SPAANo.ToString(), DateTime.Now, "SPAdvanceAdjustment", ScreenAction.Save));
            context.SaveChanges();
            return Json(new { aaNo = sPAInfo.SPAANo }, JsonRequestBehavior.AllowGet);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return Json(0, JsonRequestBehavior.AllowGet);
            //}
        }

        public ActionResult GetSpAAdjustmentBySPAANo(int spaaNo)
        {
            var getSpAAInfo = context.SPAdvanceAdjustments.FirstOrDefault(x => x.SPAANo == spaaNo);

            if (getSpAAInfo == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<SelectedBill> selectedBills = new List<SelectedBill>();
                List<AdvanceAmount> advanceAmounts = new List<AdvanceAmount>();

                var spaaline = context.SPAdvanceAmountLineItems.Where(x => x.SPAANo == spaaNo).ToList();
                var sameBillDue = spaaline.GroupBy(x => new { x.DocNo, x.DocType }).Select(y => new { DocNo = y.First().DocNo, DocType = y.First().DocType, AdjustedAmount = y.Sum(z => z.AdjustedAmount) }).ToList();
                var sameAdvanceAmount = spaaline.GroupBy(x => x.SPVNo).Select(y => new { SPVNo = y.First().SPVNo, AdjustedAmount = y.Sum(z => z.AdjustedAmount) }).ToList();

                sameBillDue.ForEach(x =>
                {
                    SelectedBill sb = new SelectedBill();
                    sb.DocNo = x.DocNo;
                    sb.DueAmount = x.AdjustedAmount;
                    sb.DocType = x.DocType;

                    if (x.DocType == "GPB")
                    {
                        var bill = context.PurchaseEntry.Where(y => y.BillNo == x.DocNo).FirstOrDefault();

                        if (bill != null)
                        {
                            sb.TotalAmount = bill.TotalPayable;
                            sb.Date = bill.PDate;
                        }
                        selectedBills.Add(sb);
                    }
                    else if (x.DocType == "GBE")
                    {
                        var bill = context.GeneralBills.Where(y => y.GBENo == x.DocNo).FirstOrDefault();

                        if (bill != null)
                        {
                            sb.TotalAmount = bill.NetAmount;
                            sb.Date = bill.GBEDate;
                        }
                        selectedBills.Add(sb);
                    }
                    //else if (x.DocType == "DP")
                    //{
                    //    var bill = context.DirectPurchase.Where(y => y.DPNo == x.DocNo).FirstOrDefault();

                    //    if (bill != null)
                    //    {
                    //        sb.TotalAmount = bill.TotalAmount;
                    //        sb.Date = bill.DPDate;
                    //    }
                    //    selectedBills.Add(sb);
                    //}
                    else if (x.DocType == "ILCB")
                    {
                        List<DueILCBBill> bills = LoadComboBox.GetDueILCBBill(getSpAAInfo.SupplierID).ToList();
                        bills.ForEach(y =>
                        {
                            if (y.ILCBNo == x.DocNo)
                            {
                                sb.TotalAmount = y.DueAmount + x.AdjustedAmount;
                                sb.Date = context.ILCB.FirstOrDefault(z => z.ILCBNo == y.ILCBNo).Date;
                                selectedBills.Add(sb);
                            }
                        });
                    }
                    else
                    {
                        var supOB = context.SupplierBalanceCalculation.FirstOrDefault(y => y.SupplierID == getSpAAInfo.SupplierID && y.DocumentType == "SOB");
                        sb.TotalAmount = supOB.Amount;
                        sb.Date = supOB.TDate;
                        selectedBills.Add(sb);
                    }
                });

                sameAdvanceAmount.ForEach(x =>
                {
                    AdvanceAmount aA = new AdvanceAmount();
                    aA.SPVNo = x.SPVNo;
                    aA.Amount = x.AdjustedAmount;

                    var advInfo = context.PayBillInfo.Where(y => y.PBID == x.SPVNo).FirstOrDefault();
                    aA.Date = advInfo.TDate;
                    advanceAmounts.Add(aA);
                });

                return Json(new { SelectedBill = selectedBills, AdvanceAmounts = advanceAmounts, GetSPAAInfo = getSpAAInfo }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteSAdvAdjustmentByid(int id)
        {
            var spadAdjustExist = context.SPAdvanceAdjustments.FirstOrDefault(x => x.SPAANo == id);
            if (spadAdjustExist != null)
            {
                context.SPAdvanceAdjustments.Remove(spadAdjustExist);
                var spadAdjustLineItem = context.SPAdvanceAmountLineItems.Where(x => x.SPAANo == id).ToList();
                spadAdjustLineItem.ForEach(x =>
                {
                    context.SPAdvanceAmountLineItems.Remove(x);
                });

                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "SPAdvanceAdjustment", ScreenAction.Delete));
                //InventoryTransactionBridge.InsertFromIFGSEntry(ref context, issuFgLineItem, ifgsExist);
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }

            return Json(0, JsonRequestBehavior.AllowGet);
        }


        public sealed class SelectedBill
        {
            public int DocNo { get; set; }
            public decimal TotalAmount { get; set; }
            public decimal DueAmount { get; set; }
            public string DocType { get; set; }
            public DateTime Date { get; set; }
        }

        public sealed class AdvanceAmount
        {
            public int SPVNo { get; set; }
            public decimal Amount { get; set; }
            public DateTime Date { get; set; }
        }
    }
}