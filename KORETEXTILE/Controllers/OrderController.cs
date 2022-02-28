using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Notification;
using BEEERP.Models.OrderBooking;
using BEEERP.Models.ViewModel.Sales.Report;
using BEEERP.Models.ViewModel.Sales.Transaction;
using Microsoft.AspNet.Identity;
using Rotativa;
using BEEERP.Models.SalesModule;

namespace BEEERP.Controllers
{

    [ShowNotification]
    [Authorize(Roles = "SalAdmin,SalOperator,SalViewer,SalApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    [SaleModule]
    public class OrderController : Controller
    {
        BEEContext context = new BEEContext();
        public ActionResult OrderBooking(string invoiceNo)  
        {
            var depot = ScreenSessionData.GetEmployeeDepot();
            ViewBag.DepotId = depot;
            if (depot == null)
            {
                ViewBag.Disabled = "true";
            }
            else
            {
                ViewBag.Disabled = "false";
            }
            ViewBag.Depot = LoadComboBox.LoadBranchInfo();
            ViewBag.Store = LoadComboBox.LoadStore(depot);
            ViewBag.GroupId = LoadComboBox.LoadItemGroup();
            ViewBag.Item = LoadComboBox.LoadItem(null);
            SalesVModel model = new SalesVModel();
            if (!string.IsNullOrEmpty(invoiceNo))
            {
                model.InvoiceNo = Convert.ToInt32(invoiceNo);
            }
            return View();
        }

        public ActionResult GetEmployeeById(int empId, int depotId)
        {
            var employee = FindObjectById.GetEmpByDepotANdEmpId(depotId, empId);
            if (employee != null)
            {
                return Json(new { Name = employee.Name, Designation = FindObjectById.SearchDesignation(employee.Designation).Name }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetStoreByDepotId(int id)
        {
            return Json(LoadComboBox.LoadStore(id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetItemByGroupId(int groupId)
        {
            return Json(LoadComboBox.LoadItem(groupId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetItemRemainQty(int itemId, int storeId)
        {
            var item = FindObjectById.GetChartOfInventoryById(itemId);
            return Json(new { RemQty = SaleCommonInfo.GetRemainItemQty(itemId, storeId,null), Price = FindObjectById.GetChartOfInventoryById(itemId).RetailPrice, cartoncapacity = item.clmCartonCapacity }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveOrder(OrderBookings orderBooking, List<OrderBookingLineItem> orderBookingLineItem)      
        {
            try
            {
                using (context)
                {
                    string invoiceNo = "";
                    string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
                    var noOfInvoice = context.OrderBookings.Count();
                    if (noOfInvoice > 0)
                    {
                        var lastInvoice = context.OrderBookings.ToList().LastOrDefault(x => x.OrderNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                        invoiceNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.OrderNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                    }
                    else
                    {
                        invoiceNo = yearLastTwoPart + "00001";
                    }

                    orderBooking.OrderNo = Convert.ToInt32(invoiceNo);
                    

                    UserNotification notification = new UserNotification();
                    notification.UserId = User.Identity.GetUserId();
                    notification.Type = "OrderEntry";
                    notification.TransactionNo = invoiceNo;
                    notification.NotificationHead = "New Order";
                    notification.NotificationDetails = "This Order is pending for approved.";
                    notification.PostingDate = DateTime.Now;


                    orderBookingLineItem.ForEach(x =>
                    {
                        x.OrderNo = orderBooking.OrderNo;
                        orderBooking.DiscountAmt = x.DisAmount;
                        orderBooking.VatAmount = x.VatAmount;
                        context.OrderBookingLineItem.Add(x);
                    });
                    context.Notification.Add(notification);
                    context.OrderBookings.Add(orderBooking);
                    
                    context.SaveChanges();
                    return Json(orderBooking.OrderNo, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }


        }

        //public ActionResult UpdateOrder(OrderBookings orderBooking, List<OrderBookingLineItem> orderBookingLineItem)       
        //{
        //    try
        //    {
        //        using (context)
        //        {
        //            context.OrderBookingLineItem.Where(x => x.InvoiceNo == orderBooking.InvoiceNo).ToList().ForEach(x => context.OrderBookingLineItem.Remove(x));
        //            context.Entry<OrderBookings>(orderBooking).State = EntityState.Modified;
        //            //context.InventoryTransaction.Where(x => x.DocID == salesNewEntry.InvoiceNo && x.DocType == "Sales").ToList().ForEach(y => context.InventoryTransaction.Remove(y));
        //            //var custBal = context.CustomerBalanceCalculations.FirstOrDefault(x => x.DocumentType == "Sales" && x.CustomerID == salesNewEntry.CustomerID && x.DocNo == salesNewEntry.InvoiceNo);
        //            orderBookingLineItem.ForEach(x =>
        //            {
        //                x.InvoiceNo = orderBooking.InvoiceNo;

        //                context.OrderBookingLineItem.Add(x);
        //                //var transaction = new InventoryTransaction();
        //                //transaction.CIID = x.ItemID;
        //                //transaction.Qty = x.Qty + x.OfferQty;
        //                //transaction.Rate = x.Price;
        //                //transaction.Amount = x.SalesValue;
        //                //transaction.TRType = -1;
        //                //transaction.TDate = DateTime.Now;
        //                //transaction.StoreID = x.StoreID;
        //                //transaction.DocType = "Sales";
        //                //transaction.DocID = salesNewEntry.InvoiceNo;
        //                //context.InventoryTransaction.Add(transaction);
        //            });
        //            context.SaveChanges();
        //            return Json(orderBooking.InvoiceNo, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json("0", JsonRequestBehavior.AllowGet);
        //    }
        //}

        public ActionResult GetOrderByInvoiceNo(int invoiceNo)  
        {
            var orderMain = context.OrderBookings.FirstOrDefault(x => x.OrderNo == invoiceNo);    
            if (orderMain == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var details = context.OrderBookingLineItem.Where(x => x.OrderNo == invoiceNo).ToList();
                List<OrderBookingLineItem> itemDetails = new List<OrderBookingLineItem>();
                SalesVModel model = new SalesVModel();
                var customer = context.Customers.FirstOrDefault(x => x.Id == orderMain.CustomerID);
                model.CustName = customer.CustomerName;
                model.ZoneId = context.TblZones.FirstOrDefault(x => x.ZoneId == customer.ZoneId).ZoneName;
                model.Area = customer.AreaName;
                var sr = context.Employees.FirstOrDefault(x => x.Id == orderMain.SalesManID);
                model.SrName = sr.Name;
                model.CreditLimit = customer.Credit;
                model.CustomerID = customer.Id;
                model.SRId = sr.Id;
                model.DepotId = orderMain.CustBal;
                model.StoreId = details.FirstOrDefault().StoreID;
                model.OrderDate = orderMain.OrderDate;
                model.SrDesignation = context.Designation.FirstOrDefault(x => x.Id == sr.Designation).Name;
                model.CommissionAmt = orderMain.CommissionAmt;
                model.DiscountAmt = orderMain.DiscountAmt;
                model.NetAmount = orderMain.InvoiceAmount + orderMain.CommissionAmt;
                model.NetInvoice = orderMain.InvoiceAmount - orderMain.DiscountAmt + orderMain.CommissionAmt;
                model.Comission = Math.Round((100 * orderMain.CommissionAmt) / orderMain.InvoiceAmount, 2);
                model.Discount = Math.Round((((orderMain.DiscountAmt) * 100) / (orderMain.InvoiceAmount + orderMain.CommissionAmt)), 2);
                details.ForEach(x =>
                {
                    x.ItemName = context.ChartOfInventory.FirstOrDefault(y => y.Id == x.ItemID).Name;
                    itemDetails.Add(x);
                });
                return Json(new { item = model, itemDetails }, JsonRequestBehavior.AllowGet);

            }


        }
    }
}