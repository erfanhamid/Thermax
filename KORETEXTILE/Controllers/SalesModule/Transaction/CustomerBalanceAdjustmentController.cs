using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using BEEERP.Models.ViewModel.Sales;
using BEEERP.Models.SalesModule;
using BEEERP.Models.Log;
using BEEERP.Models.Bridge;

namespace BEEERP.Controllers.SalesModule.Transaction
{
    [ShowNotification]
    public class CustomerBalanceAdjustmentController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: CustomerBalanceAdjustment
        public ActionResult CustomerBalanceAdjustment() 
        {
            var depot = ScreenSessionData.GetEmployeeDepot();
            ViewBag.DepotId = depot;
            ViewBag.Depot = LoadComboBox.LoadBranchInfo();
            ViewBag.Invoice = LoadDueInvoice(0, 0);

            return View();
        }

        public ActionResult GetDueCustomerId(int custId, int depot)
        {
            var item = LoadDueInvoice(custId, depot);

            if(item.Count() > 0)
            {
                var custExist = context.Customers.FirstOrDefault(x => x.Id == custId);
                if(custExist != null)
                {
                    string custName = custExist.CustomerName;
                    return Json(new { Item = item, CustomerName = custName }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
                }
                
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetDueAmount(string invoice, int custId, int depot)
        {
            var item = DueInvoiceList(custId, depot);

            if(item.Count() > 0)
            {
                decimal dueAmount = item.FirstOrDefault(x => x.InvoiceNo == invoice).DueAmount;
                return Json(dueAmount, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveCBA(CustomerBalanceAdjustment cba, List<CustomerBalanceAdjustmentLine> addedItems)
        {
            string cbaNo = "";
            string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
            var noOfInvoice = context.CustomerBalanceAdjustment.Count();
            if (noOfInvoice > 0)
            {
                var lastInvoice = context.CustomerBalanceAdjustment.ToList().LastOrDefault(x => x.CAANo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                cbaNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.CAANo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
            }
            else
            {
                cbaNo = yearLastTwoPart + "00001";
            }

            cba.CAANo = Convert.ToInt32(cbaNo);
            cba.Date = cba.Date.Add(DateTime.Now.TimeOfDay);
            addedItems.ForEach(x =>
            {
                x.CAANo = cba.CAANo;
                context.CustomerBalanceAdjustmentLine.Add(x);
            });

            context.CustomerBalanceAdjustment.Add(cba);

            UserLog.SaveUserLog(ref context, new UserLog(cba.CAANo.ToString(), DateTime.Now, "CBA", ScreenAction.Save));

            AccountModuleBridge.InsertFromCustomerBalAdustment(ref context, cba);

            context.SaveChanges();
            return Json(new { cba }, JsonRequestBehavior.AllowGet); 
        }

        public ActionResult GetCBAById(int id)
        {
            var cbaItem = context.CustomerBalanceAdjustment.FirstOrDefault(x => x.CAANo == id);

            if (cbaItem != null)
            {
                var cbaLineItem = context.CustomerBalanceAdjustmentLine.Where(x => x.CAANo == id).ToList();

                return Json(new { cbaItem, cbaLineItem }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateCBA(CustomerBalanceAdjustment cba, List<CustomerBalanceAdjustmentLine> addedItems)
        {
            var cbaExist = context.CustomerBalanceAdjustment.FirstOrDefault(x => x.CAANo == cba.CAANo);
            if (cbaExist != null)
            {
                context.CustomerBalanceAdjustment.Remove(cbaExist);

                context.CustomerBalanceAdjustmentLine.Where(x => x.CAANo == cba.CAANo).ToList().ForEach(x => {
                    context.CustomerBalanceAdjustmentLine.Remove(x);
                });

                addedItems.ForEach(x =>
                {
                    x.CAANo = cba.CAANo;
                    context.CustomerBalanceAdjustmentLine.Add(x);
                });

                cba.Date = cba.Date.Add(DateTime.Now.TimeOfDay);
                context.CustomerBalanceAdjustment.Add(cba);
                UserLog.SaveUserLog(ref context, new UserLog(cba.CAANo.ToString(), DateTime.Now, "CBA", ScreenAction.Update));
                AccountModuleBridge.InsertFromCustomerBalAdustment(ref context, cba);

                context.SaveChanges();
                return Json(new { cba }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteCBAById(int id)
        {
            var cbaExist = context.CustomerBalanceAdjustment.FirstOrDefault(x => x.CAANo == id);
            if (cbaExist != null)
            {
                context.CustomerBalanceAdjustment.Remove(cbaExist);
                var cbaLineItem = context.CustomerBalanceAdjustmentLine.Where(x => x.CAANo == id).ToList();
                context.CustomerBalanceAdjustmentLine.Where(x => x.CAANo == id).ToList().ForEach(x =>
                {
                    context.CustomerBalanceAdjustmentLine.Remove(x);
                });
                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "CBA", ScreenAction.Delete));
                AccountModuleBridge.DeleteFromCustomerBalAdjustment(ref context, cbaExist);
                
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        //public ActionResult GetCBAPreview(int CAANo)
        //{
        //    return View();
        //}

        public List<DueInvoice> DueInvoiceList(int custId, int depot)
        {
            List<DueInvoice> DueInvoiceList = new List<DueInvoice>();

            var custOb = context.CustomerBalanceCalculations.FirstOrDefault(x => x.CustomerID == custId && x.DocumentType == "COB");

            if (custOb != null)
            {
                decimal oBAdjustment = context.AdvanceAmountLine.Where(x => x.InvoiceNo == custOb.CustomerID).Select(x => x.AdjustedAmount).DefaultIfEmpty(0).Sum();
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
                remainingOb = Math.Round(remainingOb, 2);

                if (remainingOb > 0)
                {
                    var salesItem1 = new DueInvoice { InvoiceNo = "OB", InvoiceAmount = 0, DueAmount = remainingOb, Date = custOb.TDate };
                    DueInvoiceList.Add(salesItem1);

                }
            }

            var custExist = context.Customers.FirstOrDefault(x => x.Id == custId && x.DepotId == depot);
            if (custExist != null)
            {
                var saleMain = context.SalesEntryNew.Where(x => x.CustomerID == custId).ToList();
                foreach (var item in saleMain)
                {
                    decimal vaucharAmount = context.PayVouLineItem.Where(x => x.InvoiceNo == item.InvoiceNo).Select(x => x.AdjustedAmount).DefaultIfEmpty(0).Sum();
                    decimal advanceAdjustment = context.AdvanceAmountLine.Where(x => x.InvoiceNo == item.InvoiceNo).Select(x => x.AdjustedAmount).DefaultIfEmpty(0).Sum();
                    decimal customerBalanceAdjustment = context.CustomerBalanceAdjustmentLine.Where(x => x.InvoiceNo == item.InvoiceNo).Select(x=>x.Amount).DefaultIfEmpty(0).Sum();
                    decimal paidAmount = vaucharAmount + advanceAdjustment + customerBalanceAdjustment;
                    var invoice = context.SalesEntryNew.FirstOrDefault(x => x.InvoiceNo == item.InvoiceNo);
                    decimal saleAmount = invoice.InvoiceAmount - invoice.DiscountAmt + invoice.VatAmount;
                    var dueAmount = saleAmount - paidAmount;
                    dueAmount = Math.Round(dueAmount, 2);
                    if (dueAmount > 0)
                    {
                        var salesItem = new DueInvoice { InvoiceNo = item.InvoiceNo.ToString(), InvoiceAmount = saleAmount, DueAmount = dueAmount, Date = item.SalesDate };
                        DueInvoiceList.Add(salesItem);

                    }
                }
            }
            
            return DueInvoiceList;
        }

        public SelectList LoadDueInvoice(int custId, int depot) 
        {
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Invoice ---");
            var item = DueInvoiceList(custId, depot);
            item.ForEach(x => { items.Add(x.InvoiceNo.ToString(), x.InvoiceNo.ToString()); });
            return new SelectList(items, "Key", "Value");
        }
    }
}