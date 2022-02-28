using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Notification;
using BEEERP.Models.SalesModule;
using BEEERP.Models.ViewModel.Sales.Transaction;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using BEEERP.Models.ViewModel.Sales.Report;
using BEEERP.Models.OrderBooking;
using BEEERP.Models.Log;
using BEEERP.Models.Bridge;


namespace BEEERP.Controllers.SalesModule.Transaction
{
    [ShowNotification]
    [SaleModule]
    [Permission]
    public class SalesController : Controller
    {
        BEEContext context = new BEEContext();
        public ActionResult SalesEntry(string type, int invoiceNo = 0, int orderNo = 0)
        {
            var depot= ScreenSessionData.GetEmployeeDepot();
            ViewBag.DepotId = depot;
            if (depot==null)
            {
                ViewBag.Disabled = "true";
            }
            else
            {
                ViewBag.Disabled = "false";
            }
            ViewBag.messege = "";
            ViewBag.Depot = LoadComboBox.LoadBranchInfo();
            ViewBag.Store = LoadComboBox.LoadStore(depot);
            ViewBag.GroupId = LoadComboBox.LoadItemGroup();
            ViewBag.Item = LoadComboBox.LoadItem(null);
            ViewBag.Customer = LoadComboBox.LoadCustomer();
            SalesVModel model = new SalesVModel();
            if(invoiceNo != 0)
            {
                model.InvoiceNo = invoiceNo;
            }

            if (orderNo != 0)
            {
                ViewBag.OrderNumber = orderNo;
            }

            if(type != "")
            {
                ViewBag.Type = type;
            }
           
            return View();
        }
        //[HttpPost]
        //public ActionResult SalesEntry(SalesEntryNew salesMain,List<SalesEntryNewLineItem> salesDetail)
        //{
        //    return View();
        //}
        public ActionResult GetStoreByDepotId(int id)
        {
            return Json(LoadComboBox.LoadStore(id), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCustomerById(int custId, int depotId)
        {
            var customer = FindObjectById.GetCustByStoreAndCustId(depotId, custId);
            if (customer != null)
            {
                return Json(new { Name = customer.CustomerName, Zone = FindObjectById.ZoneSearch(customer.ZoneId).ZoneName, Area = customer.AreaName, customer.Credit, MobileNo = customer.MobileNo, Due = SaleCommonInfo.GetDueByCustomerId(custId), CreditDays = customer.CreditDays, TSOId = customer.TSOId, TSOName = FindObjectById.GetEmployeeById(customer.TSOId).Name, Designation = FindObjectById.SearchDesignation(FindObjectById.GetEmployeeById(customer.TSOId).Designation).Name }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

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
        public ActionResult GetItemByGroupId(int groupId)
        {
            return Json(LoadComboBox.LoadItem(groupId), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetItemByGroupIdForCal(int groupIdCal)
        {
            return Json(LoadComboBox.LoadItem(groupIdCal), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetItemRemainQty(int itemId, int storeId)   
        {
            var item = FindObjectById.GetChartOfInventoryById(itemId);
            //return Json(new { RemQty = SaleCommonInfo.GetRemainItemQty(itemId, storeId, null), Price = item.RetailPrice, SCost = item.clmStandardCost, UoM = FindObjectById.GetUomById(item.UoMID).Id, Vat = item.VatPerc, Discount = item.DisPerc, PacSize = item.PacSize, cartoncapacity = item.clmCartonCapacity }, JsonRequestBehavior.AllowGet);
            return Json(new { RemQty = SaleCommonInfo.GetRemainItemQty(itemId, storeId,null), Price = item.RetailPrice, SCost = item.clmStandardCost, UoM = FindObjectById.GetUomById(item.UoMID).Id,PacSize=item.PacSize, cartoncapacity = item.clmCartonCapacity }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetItemUnitPrice(int itemId)
        {

            var item = context.ChartOfInventory.FirstOrDefault(m => m.Id == itemId);
            //var itemunitPrice = item.RetailPrice;
            //var itemvatprice = item.VatPerc;
            //var packSize = item.PacSize;
            return Json(new { /*itemunitPrice= itemunitPrice, itemvatprice= itemvatprice, packSize= packSize*/ }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveOrder(OrderBookings orderBooking, List<OrderBookingLineItem> orderBookingLineItem)
        {
            try
            {
                using (context)
                {
                    
                    
                    string orderNo = "";
                    string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
                    var noOfInvoice = context.OrderBookings.Count();
                    if (noOfInvoice > 0)
                    {
                        var lastInvoice = context.OrderBookings.ToList().LastOrDefault(x => x.OrderNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                        orderNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.OrderNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                    }
                    else
                    {
                        orderNo = yearLastTwoPart + "00001";
                    }

                    orderBooking.OrderNo = Convert.ToInt32(orderNo);


                    UserNotification notification = new UserNotification();
                    notification.UserId = User.Identity.GetUserId();
                    notification.Type = "OrderEntry";
                    notification.TransactionNo = orderNo;
                    notification.NotificationHead = "New Order";
                    notification.NotificationDetails = "This Order is pending for approved.";
                    notification.PostingDate = DateTime.Now;
                    notification.BranchId = context.Employees.FirstOrDefault(x => x.Id == orderBooking.SalesManID).BranchID;


                    orderBookingLineItem.ForEach(x =>
                    {
                        x.OrderNo = orderBooking.OrderNo;
                        orderBooking.DiscountAmt = x.DisAmount;
                        orderBooking.VatAmount = x.VatAmount;
                        context.OrderBookingLineItem.Add(x);
                    });
                    context.Notification.Add(notification);
                    context.OrderBookings.Add(orderBooking);

                    UserLog.SaveUserLog(ref context, new UserLog(orderBooking.OrderNo.ToString(), orderBooking.OrderDate, "Order", ScreenAction.Save));
                context.SaveChanges();
                return Json(orderBooking.OrderNo, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveSale(SalesEntryNew salesNewEntry, List<SalesEntryNewLineItem> salesLineItemNew)
        {
            try
            {
                var orderExists = context.SalesEntryNew.FirstOrDefault(y => y.OrderNumber == salesNewEntry.OrderNumber);
                if (orderExists != null && (salesNewEntry.OrderNumber != 0))
                {
                    return Json(new { InvoiceNo = orderExists.InvoiceNo, Message = "3" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    using (context)
                    {

                        string invoiceNo = "";
                        string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
                        var noOfInvoice = context.SalesEntryNew.Count();
                        if (noOfInvoice > 0)
                        {
                            var lastInvoice = context.SalesEntryNew.ToList().LastOrDefault(x => x.InvoiceNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                            invoiceNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.InvoiceNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                        }
                        else
                        {
                            invoiceNo = yearLastTwoPart + "00001";
                        }

                        salesNewEntry.InvoiceNo = Convert.ToInt32(invoiceNo);

                        UserNotification notification = new UserNotification();
                        notification.UserId = User.Identity.GetUserId();
                        notification.Type = "SalesEntry";
                        notification.TransactionNo = invoiceNo;
                        notification.NotificationHead = "New Sale";
                        notification.NotificationDetails = "This sale is Approved.";
                        notification.PostingDate = DateTime.Now;
                        notification.BranchId = salesNewEntry.BranchId;
                        //notification.IsView = false;
                        decimal costAmount = 0;
                        salesLineItemNew.ForEach(x =>
                        {
                            var coi = context.ChartOfInventory.FirstOrDefault(y => y.Id == x.ItemID);
                            x.InvoiceNo = salesNewEntry.InvoiceNo;
                            //x.clmCOGSRate = (decimal)coi.clmStandardCost;
                            decimal qty = (decimal)x.Qty + (decimal)x.OfferQty;
                            x.clmCOGSRate = LoadComboBox.GetItemUnitCost(x.ItemID, "Sales", qty, salesNewEntry.InvoiceNo,x.StoreID,false);
                            x.clmCOGSValue =  x.clmCOGSRate * (decimal)(x.Qty + x.OfferQty);
                            context.SalesEntryNewLineItem.Add(x);
                            InventoryTransaction transaction = new InventoryTransaction();
                            transaction.ID = 0;
                            transaction.CIID = x.ItemID;
                            transaction.Qty = Convert.ToDecimal( x.Qty + x.OfferQty);
                            transaction.Rate = Convert.ToDecimal(x.Price);
                            transaction.Amount = Convert.ToDecimal(x.SalesValue);
                            transaction.TRType = -1;
                            //transaction.TDate = DateTime.Now;
                            costAmount += (decimal)(x.clmCOGSRate * (decimal)(x.Qty + x.OfferQty));
                            //costAmount += (x.Qty + x.OfferQty) * coi.clmStandardCost;
                            //costAmount += (decimal)(x.clmCOGSRate) * (decimal)(x.Qty + x.OfferQty);
                            transaction.TDate = salesNewEntry.SalesDate;
                            transaction.StoreID = x.StoreID;
                            transaction.DocType = "Sales";
                            transaction.DocID = salesNewEntry.InvoiceNo;
                            salesNewEntry.DiscountAmt += x.DisAmount;
                            salesNewEntry.VatAmount += x.VatAmount;
                            context.InventoryTransaction.Add(transaction);
                        });
                        context.Notification.Add(notification);
                        context.SalesEntryNew.Add(salesNewEntry);
                        salesNewEntry.clmCOGSTotal = (decimal)costAmount;
                        var updateNotif = context.Notification.FirstOrDefault(x => x.TransactionNo == salesNewEntry.OrderNumber.ToString() && x.Type == "OrderEntry");
                        context.ApprovalLog.Where(x => x.NotificationId == updateNotif.Id).ToList().ForEach(x => { x.IsApproved = true; context.Entry<ApprovalLog>(x).State = EntityState.Modified; });
                        AccountModuleBridge.InsertFromSales(ref context, salesNewEntry);
                        CustomerCalculationBridge.InsertFromSales(ref context, salesNewEntry, salesLineItemNew);

                        UserLog.SaveUserLog(ref context, new UserLog(salesNewEntry.InvoiceNo.ToString(), salesNewEntry.SalesDate, "Sale", ScreenAction.Save));
                        context.SaveChanges();
                        return Json(new { InvoiceNo = salesNewEntry.InvoiceNo, Message = "1" }, JsonRequestBehavior.AllowGet);
                    }
                }

            }
            catch (Exception ex)
            {
                return Json(new { InvoiceNo = 0, Message = "0" }, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult GetSaleByInvoiceNo(int invoiceNo)
        {
            var saleMain = context.SalesEntryNew.FirstOrDefault(x => x.InvoiceNo == invoiceNo);
            if (saleMain == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var details = context.SalesEntryNewLineItem.Where(x => x.InvoiceNo == invoiceNo).ToList();
                List<SalesEntryNewLineItem> itemDetails = new List<SalesEntryNewLineItem>();
                SalesVModel model = new SalesVModel();
                model.OrderNumber = saleMain.OrderNumber;
                var Order = context.OrderBookings.FirstOrDefault(x => x.OrderNo == saleMain.OrderNumber);
                model.OrderDate = Order.OrderDate;

                var customer = context.Customers.FirstOrDefault(x => x.Id == saleMain.CustomerID);
                model.CustName = customer.CustomerName;
                model.ZoneId = context.TblZones.FirstOrDefault(x => x.ZoneId == customer.ZoneId).ZoneName;
                model.Area = customer.AreaName;
                model.CustMobileNo = customer.MobileNo;
                var sr = context.Employees.FirstOrDefault(x => x.Id == saleMain.SalesManID);
                model.SrName = sr.Name;
                model.CreditLimit = customer.Credit;
                model.CustomerID = customer.Id;
                model.SRId = sr.Id;
                model.DepotId = saleMain.BranchId;
                model.StoreId = details.FirstOrDefault().StoreID;
                model.SalesDate = saleMain.SalesDate;
                model.SrDesignation = context.Designation.FirstOrDefault(x => x.Id == sr.Designation).Name;
                model.CommissionAmt = saleMain.CommissionAmt;
                model.DiscountAmt = saleMain.DiscountAmt;
                model.NetAmount = saleMain.InvoiceAmount + saleMain.CommissionAmt;
                model.NetInvoice = saleMain.InvoiceAmount - saleMain.DiscountAmt + saleMain.CommissionAmt;
                model.Comission = Math.Round((100 * saleMain.CommissionAmt) / saleMain.InvoiceAmount, 2);
                model.Discount = Math.Round((((saleMain.DiscountAmt) * 100) / (saleMain.InvoiceAmount + saleMain.CommissionAmt)), 2);
                model.SaleType = saleMain.SaleType;
                model.Description = saleMain.Description;
                model.RefNo = saleMain.RefNo;
                model.Remarks = saleMain.Remarks;

                details.ForEach(x =>
                {
                    var chartOfInv = context.ChartOfInventory.FirstOrDefault(y => y.Id == x.ItemID);
                    x.ItemName =chartOfInv.Name;
                    x.GroupId = chartOfInv.parentId;
                    //x.PacSize = chartOfInv.PacSize;
                    itemDetails.Add(x);
                });
                return Json(new { item = model, itemDetails }, JsonRequestBehavior.AllowGet);

            }


        }

        public ActionResult GetOrderByOrderNo(int orderNo)  
        {
            var orderMain = context.OrderBookings.FirstOrDefault(x => x.OrderNo == orderNo);
            if (orderMain == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var details = context.OrderBookingLineItem.Where(x => x.OrderNo == orderNo).ToList();
                List<OrderBookingLineItem> itemDetails = new List<OrderBookingLineItem>();
                SalesVModel model = new SalesVModel();
                var customer = context.Customers.FirstOrDefault(x => x.Id == orderMain.CustomerID);
                model.CustName = customer.CustomerName;
                model.ZoneId = context.TblZones.FirstOrDefault(x => x.ZoneId == customer.ZoneId).ZoneName;
                model.Area = customer.AreaName;
                model.CustMobileNo = customer.MobileNo;
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
                model.SaleType = orderMain.SaleType;
                model.Description = orderMain.Description;
                model.RefNo = orderMain.RefNo;
                details.ForEach(x =>
                {
                    //x.ItemName = context.ChartOfInventory.FirstOrDefault(y => y.Id == x.ItemID).Name;
                    //itemDetails.Add(x);
                    var chartOfInv = context.ChartOfInventory.FirstOrDefault(y => y.Id == x.ItemID);
                    x.ItemName = chartOfInv.Name;
                    //x.PacSize = chartOfInv.PacSize;
                    x.GroupId = chartOfInv.parentId;
                    itemDetails.Add(x);
                });
                return Json(new { item = model, itemDetails }, JsonRequestBehavior.AllowGet);

            }


        }

        public ActionResult UpdateSale(SalesEntryNew salesNewEntry, List<SalesEntryNewLineItem> salesLineItemNew)
        {
            salesNewEntry.VatAmount =(decimal) 0.0;
            salesNewEntry.DisAmount =(decimal) 0.0;
            try
            {
                using (context)
                {
                    var customerId = context.SalesEntryNew.AsNoTracking().FirstOrDefault(x => x.InvoiceNo == salesNewEntry.InvoiceNo).CustomerID;
                    context.SalesEntryNewLineItem.Where(x => x.InvoiceNo == salesNewEntry.InvoiceNo).ToList().ForEach(x => context.SalesEntryNewLineItem.Remove(x));
                    //context.Entry<SalesEntryNew>(salesNewEntry).State = EntityState.Modified;
                    context.InventoryTransaction.Where(x => x.DocID == salesNewEntry.InvoiceNo && x.DocType == "Sales").ToList().ForEach(y => context.InventoryTransaction.Remove(y));
                    var custBal = context.CustomerBalanceCalculations.FirstOrDefault(x => x.DocumentType == "Sales" && x.CustomerID == customerId && x.DocNo == salesNewEntry.InvoiceNo);
                    salesLineItemNew.ForEach(x => {
                        salesNewEntry.DiscountAmt += (decimal)Math.Round((x.SalesValue * (decimal)x.DisPerc) / 100,2);
                        salesNewEntry.VatAmount += (decimal)Math.Round((x.SalesValue * (decimal)x.VatPerc) / 100,2);
                    });
                    double costAmount = 0.0;
                    
                    salesLineItemNew.ForEach(x =>
                    {
                        var coi = context.ChartOfInventory.FirstOrDefault(y => y.Id == x.ItemID);
                        x.InvoiceNo = salesNewEntry.InvoiceNo;
                        x.clmCOGSRate = LoadComboBox.GetItemUnitCost(x.ItemID, "Sales", Convert.ToDecimal(x.Qty), salesNewEntry.InvoiceNo, x.StoreID, true);
                        
                        x.clmCOGSValue = x.clmCOGSRate * (decimal)(x.Qty + x.OfferQty);

                        context.SalesEntryNewLineItem.Add(x);
                        var transaction = new InventoryTransaction();
                        transaction.CIID = x.ItemID;
                        transaction.Qty =Convert.ToDecimal( x.Qty+x.OfferQty);
                        transaction.Rate = Convert.ToDecimal(x.Price);
                        transaction.Amount =Convert.ToDecimal(x.SalesValue);
                        transaction.TRType = -1;
                        transaction.TDate = DateTime.Now;
                        transaction.StoreID = x.StoreID;
                        transaction.DocType = "Sales";
                        transaction.DocID = salesNewEntry.InvoiceNo;
                        context.InventoryTransaction.Add(transaction);
                        costAmount += (double)x.clmCOGSValue;
                        
                    });
                    custBal.CustomerID = customerId;
                    salesNewEntry.clmCOGSTotal = (decimal)costAmount;
                    context.Entry<SalesEntryNew>(salesNewEntry).State = EntityState.Modified;
                    context.Entry<CustomerBalanceCalculation>(custBal).State = EntityState.Modified;
                    AccountModuleBridge.InsertFromSales(ref context, salesNewEntry);
                    CustomerCalculationBridge.InsertFromSales(ref context, salesNewEntry, salesLineItemNew);
                    UserLog.SaveUserLog(ref context, new UserLog(salesNewEntry.InvoiceNo.ToString(), salesNewEntry.SalesDate, "Sale", ScreenAction.Update));
                    context.SaveChanges();
                    return Json(salesNewEntry.InvoiceNo, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateOrder(OrderBookings salesNewEntry, List<OrderBookingLineItem> salesLineItemNew)  
        {
            try
            {
                using (context)
                {
                    context.OrderBookingLineItem.Where(x => x.OrderNo == salesNewEntry.OrderNo).ToList().ForEach(x => context.OrderBookingLineItem.Remove(x));
                    context.Entry<OrderBookings>(salesNewEntry).State = EntityState.Modified;
                   // context.InventoryTransaction.Where(x => x.DocID == salesNewEntry.OrderNo && x.DocType == "Order").ToList().ForEach(y => context.InventoryTransaction.Remove(y));
                    //var custBal = context.CustomerBalanceCalculations.FirstOrDefault(x => x.DocumentType == "Order" && x.CustomerID == salesNewEntry.CustomerID && x.DocNo == salesNewEntry.OrderNo);


                    salesLineItemNew.ForEach(x =>
                    {
                        x.OrderNo = salesNewEntry.OrderNo;

                        context.OrderBookingLineItem.Add(x);
                        //var transaction = new InventoryTransaction();
                        //transaction.CIID = x.ItemID;
                        //transaction.Qty = x.Qty + x.OfferQty;
                        //transaction.Rate = Convert.ToDecimal(x.Price);
                        //transaction.Amount = Convert.ToDecimal(x.SalesValue);
                        //transaction.TRType = -1;
                        //transaction.TDate = DateTime.Now;
                        //transaction.StoreID = x.StoreID;
                        //transaction.DocType = "Order";
                        //transaction.DocID = salesNewEntry.OrderNo;
                        //context.InventoryTransaction.Add(transaction);
                    });
                   // context.Entry<CustomerBalanceCalculation>(custBal).State = EntityState.Modified;
                    UserLog.SaveUserLog(ref context, new UserLog(salesNewEntry.OrderNo.ToString(), salesNewEntry.OrderDate, "Order", ScreenAction.Update));
                    context.SaveChanges();
                    return Json(salesNewEntry.OrderNo, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteSalesByInvoice(int invoiceNo)
        {
            var invoiceExist = context.SalesEntryNew.FirstOrDefault(x => x.InvoiceNo == invoiceNo);
            if (invoiceExist != null)
            {

                var invPaymentExist = context.PayVouLineItem.Where(x => x.InvoiceNo == invoiceNo).ToList();
                var advanceAdjustExist = context.AdvanceAmountLine.Where(x => x.InvoiceNo == invoiceNo).ToList();
                var customerBalAdjustExist = context.CustomerBalanceAdjustmentLine.Where(x => x.InvoiceNo == invoiceNo).ToList();
                var salesReturnExist = context.SalesReturnEntrie.Where(x => x.InvoiceNo == invoiceNo).ToList();

                if (invPaymentExist.Count() == 0 && advanceAdjustExist.Count == 0 && customerBalAdjustExist.Count() == 0 && salesReturnExist.Count() == 0)
                {
                    UserNotification notification = new UserNotification();
                    notification.UserId = User.Identity.GetUserId();
                    notification.Type = "SalesDelete";
                    notification.TransactionNo = invoiceNo.ToString();
                    notification.NotificationHead = "Delete Invoice";
                    notification.NotificationDetails = "This sale is Deleted.";
                    notification.PostingDate = DateTime.Now;
                    notification.BranchId = invoiceExist.BranchId;
                    //notification.IsView = false;
                    context.Notification.Add(notification);

                    var inveTransection = context.InventoryTransaction.Where(x => x.DocType == "Sales" && x.DocID == invoiceNo).ToList();
                    inveTransection.ForEach(x =>
                    {
                        context.InventoryTransaction.Remove(x);
                    });

                    context.SalesEntryNew.Remove(invoiceExist);
                    var salesLineItem = context.SalesEntryNewLineItem.Where(x => x.InvoiceNo == invoiceNo).ToList();
                    salesLineItem.ForEach(x =>
                    {
                        context.SalesEntryNewLineItem.Remove(x);
                    });
                    var RemainingStock = context.RemainingStock.Where(x => x.DocNo == invoiceNo).ToList();
                    RemainingStock.ForEach(x =>
                    {
                        context.RemainingStock.Remove(x);
                    });
                    UserLog.SaveUserLog(ref context, new UserLog(invoiceNo.ToString(), DateTime.Now, "Sale", ScreenAction.Delete));
                    AccountModuleBridge.DeleteFromSales(ref context, invoiceExist, null);
                    CustomerCalculationBridge.DeleteFromSales(ref context, invoiceExist, new List<SalesEntryNewLineItem>());

                    context.SaveChanges();
                    return Json(invoiceNo, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json(new { Message = 0}, JsonRequestBehavior.AllowGet);
                }
                
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            
        }

        public ActionResult Invoice(int invoiceNo)
        {
            var items = context.SalesEntryNewLineItem.Where(x => x.InvoiceNo == invoiceNo).ToList();
            var sale = context.SalesEntryNew.FirstOrDefault(x => x.InvoiceNo == invoiceNo);
            if (sale != null)
            {
                items.ForEach(x =>
                {
                    var item = context.ChartOfInventory.FirstOrDefault(y => y.Id == x.ItemID);
                    x.ItemName = item.Name;
                    //x.PacSize = item.PacSize;
                    //x.CartonCapacity = item.clmCartonCapacity;
                    x.VatAmount = (decimal)Math.Round(((x.SalesValue * Convert.ToDecimal(x.VatPerc)) / 100), 2);
                    x.DisAmount = (decimal)Math.Round(((x.SalesValue * Convert.ToDecimal(x.DisPerc)) / 100), 2);
                });
                ViewBag.SalesDetails = context.SalesEntryNewLineItem.Where(x => x.InvoiceNo == invoiceNo);
                InvoiceVModel model = new InvoiceVModel();
                
                var customer = context.Customers.FirstOrDefault(x => x.Id == sale.CustomerID);
                var tso = context.Employees.FirstOrDefault(x => x.Id == customer.TSOId);
                model.Address = customer.ShipTo;
                model.CustomerId = customer.Id;
                model.CustomerName = customer.CustomerName;
                model.InvoiceNo = sale.InvoiceNo;
                model.OrderBookDate = context.OrderBookings.FirstOrDefault(x=>x.OrderNo==sale.OrderNumber).OrderDate.ToString("dd-MM-yyyy");
                model.InvoiceDate = sale.SalesDate.ToString("dd-MM-yyyy");
                model.DACode = "";
                model.DAssttName = "";
                model.TSOName = tso.Name;
                model.TSOCode = tso.Id.ToString();
                model.NetSale = sale.InvoiceAmount;
                model.TelePhoneNo = customer.TelephoneNo;
                
                ViewBag.SaleEntry = model;
                ViewBag.PostedBy = (from p in context.UserLog join 
                                   c in context.Employees on p.LogInName equals c.LogInUserName
                                   where p.ScreenName== "Order"&&p.TrnasId==sale.OrderNumber.ToString() select c).ToList().FirstOrDefault().Name;
                ViewBag.AuthorizedBy= (from p in context.UserLog
                                       join
                                       c in context.Employees on p.LogInName equals c.LogInUserName
                                       where p.ScreenName == "Sale" && p.TrnasId == sale.InvoiceNo.ToString()
                                       select c).ToList().FirstOrDefault().Name;
                ViewBag.DepotName = context.BranchInformation.FirstOrDefault(x => x.Slno == sale.BranchId).BrnachName;

                ViewBag.startDate = context.CompanySetup.FirstOrDefault(m=>m.ID==1).StartDate;
                ViewBag.ComName = context.CompanyInformation.FirstOrDefault(m=>m.Id==1).CompanyName;
                ViewBag.ComAddress = context.CompanyInformation.FirstOrDefault(m => m.Id == 1).CompanyAddress;
                ViewBag.ComContact = context.CompanyInformation.FirstOrDefault(m => m.Id == 1).ConNo;
                ViewBag.FactoryAddress = context.CompanyInformation.FirstOrDefault(m => m.Id == 1).FactoryAddress;
                ViewBag.FactoryContact = context.CompanyInformation.FirstOrDefault(m => m.Id == 1).FactoryContact;

                //return View();
                return new ViewAsPdf();
            }
            else
            {
                ViewBag.SalesDetails = new List<SalesEntryNewLineItem>();
                ViewBag.SaleEntry = new InvoiceVModel();
                return new ViewAsPdf();
            }


        }

        public ActionResult Order(int orderNo)
        {
            var items = context.OrderBookingLineItem.Where(x => x.OrderNo == orderNo).ToList();
            var sale = context.OrderBookings.FirstOrDefault(x => x.OrderNo == orderNo);
            if (sale != null)
            {
                items.ForEach(x =>
                {
                    var item = context.ChartOfInventory.FirstOrDefault(y => y.Id == x.ItemID);
                    x.ItemName = item.Name;
                    //x.PacSize = item.PacSize;
                    //x.CartonCapacity = item.clmCartonCapacity;
                    x.VatAmount = (double)Math.Round(((x.SalesValue * x.VatPerc) / 100), 2);
                    x.DisAmount = (decimal)Math.Round(((x.SalesValue *(x.DisPerc)) / 100), 2);
                });
                ViewBag.SalesDetails = context.OrderBookingLineItem.Where(x => x.OrderNo == orderNo).ToList();
                InvoiceVModel model = new InvoiceVModel();

                var customer = context.Customers.FirstOrDefault(x => x.Id == sale.CustomerID);
                var tso = context.Employees.FirstOrDefault(x => x.Id == customer.TSOId);
                model.Address = customer.ShipTo;
                model.CustomerId = customer.Id;
                model.CustomerName = customer.CustomerName;
                model.InvoiceNo = sale.OrderNo;
                model.OrderBookDate = context.OrderBookings.FirstOrDefault(x => x.OrderNo == sale.OrderNo).OrderDate.ToString("dd-MM-yyyy");
                model.OrderBookDate = sale.OrderDate.ToString("dd-MM-yyyy");
                model.DACode = "";
                model.DAssttName = "";
                model.TSOName = tso.Name;
                model.TSOCode = tso.Id.ToString();
                model.NetSale = sale.InvoiceAmount;
                model.TelePhoneNo = customer.TelephoneNo;

                ViewBag.SaleEntry = model;
                ViewBag.PostedBy = (from p in context.UserLog
                                    join
         c in context.Employees on p.LogInName equals c.LogInUserName
                                    where p.ScreenName == "Order" && p.TrnasId == sale.OrderNo.ToString()
                                    select c).ToList().FirstOrDefault().Name;
                var auth = (from p in context.UserLog
                            join
                            c in context.Employees on p.LogInName equals c.LogInUserName
                            where p.ScreenName == "Order" && p.TrnasId == sale.OrderNo.ToString()
                            select c).ToList().FirstOrDefault();
                if(auth==null)
                {
                    ViewBag.AuthorizedBy = auth.Name;
                }
                else
                {
                    ViewBag.AuthorizedBy = "";

                }
                

                ViewBag.DepotName =context.Database.SqlQuery<string>("select top(1)br.BrnachName from Employee e inner join Customer c on e.id=c.TSOId inner join BranchInformation br on e.BranchID = br.Slno where c.Id = '"+sale.CustomerID+"'").FirstOrDefault();
                ViewBag.startDate = context.CompanySetup.FirstOrDefault(m => m.ID == 1).StartDate;
                ViewBag.ComName = context.CompanyInformation.FirstOrDefault(m => m.Id == 1).CompanyName;
                ViewBag.ComAddress = context.CompanyInformation.FirstOrDefault(m => m.Id == 1).CompanyAddress;
                ViewBag.ComContact = context.CompanyInformation.FirstOrDefault(m => m.Id == 1).ConNo;
                ViewBag.FactoryAddress = context.CompanyInformation.FirstOrDefault(m => m.Id == 1).FactoryAddress;
                ViewBag.FactoryContact = context.CompanyInformation.FirstOrDefault(m => m.Id == 1).FactoryContact;
                //return View();
                return new ViewAsPdf();
            }
            else
            {
                ViewBag.SalesDetails = new List<SalesEntryNewLineItem>();
                ViewBag.SaleEntry = new InvoiceVModel();
                return new ViewAsPdf();
            }


        }

        public ActionResult GetOrderStatus(int orderNo) 
        {
            var isOrderApprove = context.SalesEntryNew.FirstOrDefault(x => x.OrderNumber == orderNo);

            if(isOrderApprove == null)
            {
                return Json(new { InvoiceNo = 0, Message = "0" }, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(new { invoiceNo = isOrderApprove.InvoiceNo, Message = "1" }, JsonRequestBehavior.AllowGet);
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }

            base.Dispose(disposing);
        }
        public  ActionResult GetCustomerValidOrNot(int customerId)
        {

            var isvalid = FindObjectById.IsCustomerInvoiceDayLimiteExceed(customerId);
            return Json(isvalid, JsonRequestBehavior.AllowGet);
        }


    }
}