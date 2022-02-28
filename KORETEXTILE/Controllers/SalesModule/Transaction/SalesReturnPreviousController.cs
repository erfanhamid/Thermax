using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.SalesModule;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using BEEERP.Models.Bridge;
using BEEERP.Models.StoreRM.GRN;

namespace BEEERP.Controllers.SalesModule.Transaction
{
    [ShowNotification]
    [SaleModule]
    [Permission]

    public class SalesReturnPreviousController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: SalesReturnPrevious
        public ActionResult SalesReturnPrevious()   
        {
            var depot = ScreenSessionData.GetEmployeeDepot();
            ViewBag.DepotId = depot;
            if (depot == null)
            {
                ViewBag.Disabled = "true";
                ViewBag.TSO = LoadComboBox.LoadEmployeeByDepot(null);
                ViewBag.Store = LoadComboBox.LoadAllFGStoreByDepot(null);
            }
            else
            {
                ViewBag.Disabled = "false";
                ViewBag.TSO = LoadComboBox.LoadEmployeeByDepot((int)depot);
                ViewBag.Store = LoadComboBox.LoadAllFGStoreByDepot(depot);
            }

            ViewBag.Depot = LoadComboBox.LoadBranchInfo();
            ViewBag.Item = LoadComboBox.LoadAllFGItem();
            ViewBag.Customer = LoadComboBox.LoadCustomerByTSO(null);
            return View();
        }

        public ActionResult GetCustomerByTsoId(int tsoId)
        {
            return Json(LoadComboBox.LoadCustomerByTSO(tsoId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveSalesReturn(SalesReturnEntry salesReturn, List<SalesReturnLineItem> addedItems)
        {
            using (context)
            {
                var SRNoCount = context.SalesReturnEntrie.Count();
                var lastSRNo = 0;
                if (SRNoCount > 0)
                {
                    lastSRNo = context.SalesReturnEntrie.Max(x => x.SRNo);
                    lastSRNo += 1;
                }
                else
                {
                    lastSRNo = 1;
                }

                salesReturn.SRNo = lastSRNo;
                salesReturn.IsPrevious = true;
                salesReturn.SRDate = salesReturn.SRDate.Add(DateTime.Now.TimeOfDay);
                if (salesReturn.InvoiceNo == 0)
                {
                    salesReturn.InvoiceNo = 0;
                }
                addedItems.ForEach(x =>
                {
                    x.SRNo = salesReturn.SRNo;
                    x.RtnOfferQty = 0;
                    RemainingStock RS = new RemainingStock();
                    RS.ItemId = x.ItemID;
                    RS.Qty = (decimal)x.ReturnQty;
                    RS.LCRNNo = salesReturn.SRNo;
                    RS.UnitCost = x.Price;
                    RS.DocType = "SalesReturnPrevious";
                    RS.DocNo = salesReturn.SRNo;
                    RS.StoreId = salesReturn.Store;
                    context.RemainingStock.Add(RS);
                    context.SalesReturnLineItem.Add(x);
                });

                context.SalesReturnEntrie.Add(salesReturn);

                UserLog.SaveUserLog(ref context, new UserLog(salesReturn.SRNo.ToString(), DateTime.Now, "SalesReturnPrevious", ScreenAction.Save));
                InventoryTransactionBridge.InsertFromSalesRetrunEntry(ref context, salesReturn, addedItems);
                CustomerCalculationBridge.InsertFromSalesReturnEntry(ref context, salesReturn);
                AccountModuleBridge.InsertFromSalesReturn(ref context,salesReturn);
                context.SaveChanges();
                return Json(new { srNo = salesReturn.SRNo }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetSalesReturnById(int id)
       {
            var srItem = context.SalesReturnEntrie.FirstOrDefault(x => x.SRNo == id);
            if (srItem != null)
            {
                srItem.TSO = context.Customers.FirstOrDefault(x => x.Id == srItem.CustomerId).TSOId;
                var srLineItem = context.SalesReturnLineItem.Where(x => x.SRNo == id).ToList();
                srLineItem.ForEach(x =>
                {
                    var item = context.SalesReturnLineItem.FirstOrDefault(y => y.SRNo == x.SRNo && y.ItemID == x.ItemID);
                    var chartOfInv = context.ChartOfInventory.ToList().FirstOrDefault(z => z.Id == x.ItemID);
                    x.ItemName = chartOfInv.Name;
                });
                return Json(new { srItem, srLineItem }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateSalesReturn(SalesReturnEntry salesReturn, List<SalesReturnLineItem> addedItems)
        {
            var srExist = context.SalesReturnEntrie.FirstOrDefault(x => x.SRNo == salesReturn.SRNo);
            if (srExist != null)
            {
                context.SalesReturnEntrie.Remove(srExist);

                context.SalesReturnLineItem.Where(x => x.SRNo == salesReturn.SRNo).ToList().ForEach(x => {
                    context.SalesReturnLineItem.Remove(x);
                });

                
                salesReturn.SRDate = salesReturn.SRDate.Add(DateTime.Now.TimeOfDay);
                salesReturn.IsPrevious = true;

                addedItems.ForEach(x =>
                {
                    x.SRNo = salesReturn.SRNo;
                    var item = context.RemainingStock.Where(y => y.ItemId == x.ItemID && y.LCRNNo == x.SRNo && y.DocNo == salesReturn.InvoiceNo).ToList();
                    if (item.Count > 0)
                    {
                        item.ForEach(a => {
                            context.RemainingStock.Remove(a);
                        });
                        context.SaveChanges();
                    }
                    RemainingStock RS = new RemainingStock();
                    RS.ItemId = x.ItemID;
                    RS.Qty = (decimal)x.ReturnQty;
                    RS.LCRNNo = salesReturn.SRNo;
                    RS.UnitCost = x.Price;
                    RS.DocType = "SalesReturnPrevious";
                    RS.DocNo = salesReturn.SRNo;
                    RS.StoreId = salesReturn.Store;
                    context.RemainingStock.Add(RS);

                    context.SalesReturnLineItem.Add(x);
                });

                context.SalesReturnEntrie.Add(salesReturn);
                UserLog.SaveUserLog(ref context, new UserLog(salesReturn.SRNo.ToString(), DateTime.Now, "SalesReturnPrevious", ScreenAction.Update));
                InventoryTransactionBridge.InsertFromSalesRetrunEntry(ref context, salesReturn, addedItems);
                CustomerCalculationBridge.InsertFromSalesReturnEntry(ref context, salesReturn);
                AccountModuleBridge.InsertFromSalesReturn(ref context, salesReturn);
                context.SaveChanges();
                return Json(new { srNo = salesReturn.SRNo }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteSalesReturnByid(int id)
        {

            var srExist = context.SalesReturnEntrie.FirstOrDefault(x => x.SRNo == id);
            if (srExist != null)
            {
                context.SalesReturnEntrie.Remove(srExist);
                var srLineItem = context.SalesReturnLineItem.Where(x => x.SRNo == id).ToList();
                context.SalesReturnLineItem.Where(x => x.SRNo == id).ToList().ForEach(x =>
                {
                    var isExist = context.RemainingStock.Where(s => s.LCRNNo == id && s.DocType == "SalesReturnPrevious" && s.ItemId == x.ItemID).ToList();
                    isExist.ForEach(i => {
                        context.RemainingStock.Remove(i);
                    });
                    context.SaveChanges();
                    context.SalesReturnLineItem.Remove(x);
                });
                
                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "SalesReturn", ScreenAction.Delete));
                InventoryTransactionBridge.InsertFromSalesRetrunEntry(ref context, srExist, new List<SalesReturnLineItem>());
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);

        }
    }
}