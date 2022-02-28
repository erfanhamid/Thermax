using BEEERP.Models.Bridge;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using BEEERP.Models.ProductionModule;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Production
{
   
    [ShowNotification]
    [Permission]
    //[Authorize(Roles = "ProAdmin,ProOperator,ProViewer,ProApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]

    public class RawMaterialConsumptionController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: RawMaterialConsumption
        public ActionResult RMProduction()
        {
            ViewBag.Item = LoadComboBox.LoadItem(null);
            ViewBag.GroupId = LoadComboBox.LoadItemGroupByBatchNo(null);
            ViewBag.LoadBatch = LoadComboBox.LoadBatch();
            ViewBag.Store = LoadComboBox.LoadProductionStoreForRM();
            return View();
        }
        public ActionResult UOMOnItemChange(int id)
        {
            //var item = context.ChartOfInventory.FirstOrDefault(x => x.Id == id);
            var item = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                var uom = context.UOM.FirstOrDefault(x => x.Id == item.UoMID);
                return Json(new { Messsage = 1, Uom = uom.Name, UnitCost = item.clmStandardCost }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetGroupByBatch(string batchNo)
        {
            var items = LoadComboBox.LoadItemGroupByBatchNo(batchNo);
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetItemByBatchID(string batchName)
        {
            return Json(LoadComboBox.LoadAllRMItem(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveRMC(RMCEntry rmcEntry ,List<RMCLineItem> rmcLineItems)
        {
            if (rmcEntry != null)
            {
                string RMCNo = "";
                string yearLastTwoPart = rmcEntry.RMCDate.Year.ToString().Substring(2, 2).ToString();
                var noOfInvoice = context.RMCEntry.Count();
                if (noOfInvoice > 0)
                {
                    var lastInvoice = context.RMCEntry.ToList().LastOrDefault(x => x.RMCNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                    if (lastInvoice == null)
                    {
                        RMCNo = yearLastTwoPart + "00001";
                    }
                    else
                    {
                        RMCNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.RMCNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                    }
                }
                else
                {
                    RMCNo = yearLastTwoPart + "00001";
                }
                rmcEntry.RMCNo = Convert.ToInt32(RMCNo);
                foreach (var item in rmcLineItems)
                {
                    rmcEntry.RMCTotalQty += item.ItemQty;
                }
                var existsRMCNo = context.RMCEntry.FirstOrDefault(m => m.RMCNo == rmcEntry.RMCNo);
                if (existsRMCNo == null)
                {
                    try
                    {
                        rmcEntry.RMCDate = rmcEntry.RMCDate.Add(DateTime.Now.TimeOfDay);
                        context.RMCEntry.Add(rmcEntry);
                        rmcLineItems.ForEach(x =>
                        {
                            var item = LoadComboBox.GetItemInfo().FirstOrDefault(y => y.Id == x.ItemID);
                            x.UnitStanCost = Convert.ToDecimal(item.clmStandardCost);
                            x.TotalStanCost = Math.Round(x.ItemQty * x.UnitStanCost, 2);
                        });
                        rmcLineItems.ForEach(x => { x.RMCNo = rmcEntry.RMCNo; context.RMCLineItem.Add(x); });
                        InventoryTransactionBridge.InsertFromRMCEntry(ref context, rmcLineItems, rmcEntry);
                        UserLog.SaveUserLog(ref context, new UserLog(rmcEntry.RMCNo.ToString(), DateTime.Now, "Raw Material Consumption", ScreenAction.Update));
                        context.SaveChanges();
                        return Json(new { Message = 1, JsonRequestBehavior.AllowGet });
                    }
                    catch (Exception ex)
                    {
                        return Json(new { Message = 0, JsonRequestBehavior.AllowGet });
                    }
                }
                else
                {
                    return Json(new { Message = 2, JsonRequestBehavior.AllowGet });
                }
            }
            else
            {
                return Json(new { Message = 0, JsonRequestBehavior.AllowGet });
            }
            
        }
        public ActionResult GetRMPByRMCNo(int rmcNo)
        {
            if (rmcNo == 0)
            {
                return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var rmc = context.RMCEntry.FirstOrDefault(x => x.RMCNo == rmcNo);
                if (rmc == null)
                {
                    return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    var rmcLineItem = context.RMCLineItem.Where(x => x.RMCNo == rmcNo).ToList();
                    rmcLineItem.ForEach(x =>
                    {
                        //var item = context.ChartOfInventory.FirstOrDefault(y => y.Id == x.ItemID);
                        var item = LoadComboBox.GetItemInfo().FirstOrDefault(y => y.Id == x.ItemID);
                        x.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(m => m.Id == x.ItemID).Name;
                    });
                   
                    return Json(new {Message = 1, rmcEntry = rmc, rmcLineItem = rmcLineItem }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult GetItemRemainQty(int itemId, int storeId)
        {
            var item = FindObjectById.GetChartOfInventoryById(itemId);
            return Json(new { RemQty = SaleCommonInfo.GetRemainingQtyForRMC(itemId), Item = item }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateRMCProduction(RMCEntry rmcConsumption, List<RMCLineItem> addedItems)  
        {

            try
            {
                foreach (var item in addedItems)
                {
                    rmcConsumption.RMCTotalQty += item.ItemQty;
                }
                var isExist = context.RMCEntry.AsNoTracking().FirstOrDefault(x => x.RMCNo == rmcConsumption.RMCNo);
                if (isExist == null)
                {
                    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    
                    var prevItem = context.RMCLineItem.Where(x => x.RMCNo == rmcConsumption.RMCNo).ToList();
                    prevItem.ForEach(x =>
                    {
                        context.RMCLineItem.Remove(x);
                    });
                    rmcConsumption.RMCDate = isExist.RMCDate;
                    context.Entry<RMCEntry>(rmcConsumption).State = EntityState.Modified;
                    addedItems.ForEach(x => {
                        var item = LoadComboBox.GetItemInfo().FirstOrDefault(y => y.Id == x.ItemID);
                        x.UnitStanCost = Convert.ToDecimal(item.clmStandardCost);
                        x.TotalStanCost = Math.Round(x.ItemQty * x.UnitStanCost, 2);
                    });
                    addedItems.ForEach(x => { x.RMCNo = rmcConsumption.RMCNo; context.RMCLineItem.Add(x); });
                    InventoryTransactionBridge.InsertFromRMCEntry(ref context, addedItems, rmcConsumption);
                    UserLog.SaveUserLog(ref context, new UserLog(rmcConsumption.RMCNo.ToString(), DateTime.Now, "Raw Material Consumption", ScreenAction.Update));
                    context.SaveChanges();
                }
                return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }
            
        }
        public ActionResult DeleteRMC(int id)
        {

            
            var isExist = context.RMCEntry.AsNoTracking().FirstOrDefault(x => x.RMCNo == id);
            if (isExist == null)
            {
                return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
            }
            else
            {

                var prevItem = context.RMCLineItem.Where(x => x.RMCNo == id).ToList();
                prevItem.ForEach(x =>
                {
                    context.RMCLineItem.Remove(x);
                });
                context.RMCEntry.Remove(isExist);
                InventoryTransactionBridge.DeleteFromRMCEntry(ref context,id);
                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "Raw Material Consumption", ScreenAction.Delete));
                context.SaveChanges();
            }
            return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
        }

    }
}