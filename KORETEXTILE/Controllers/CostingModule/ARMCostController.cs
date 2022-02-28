using BEEERP.Models.Bridge;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CostingModule;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.CostingModule
{
    [ShowNotification]
    public class ARMCostController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: ARMCost
        public ActionResult ARMCost()
        {
            ViewBag.Item = LoadComboBox.LoadItem(null);
            ViewBag.GroupId = LoadComboBox.LoadItemGroupByBatchNo(null);
            ViewBag.LoadBatch = LoadComboBox.LoadBatch();
            ViewBag.Store = LoadComboBox.LoadProductionStoreForRM();
            ViewBag.Group = LoadComboBox.LoadItemGroupRM();
            return View();
        }

        public ActionResult GetAllItemByBatchID(int batcId)
        {
            var items = (from l in context.RMCLineItem
                         join m in context.RMCEntry on l.RMCNo equals m.RMCNo
                         where m.BatchID == batcId
                         select new { l,m.StoreId,m.RMCDate,m.RMCNo}).ToList();
            var batchDate = context.Batch.FirstOrDefault(x => x.ID == batcId).BatchDate;
            var firstDate = batchDate.Year.ToString()+"-" + "01" + "-" + "01";
            var lastDate= LastDayOfMonth(batchDate);
            items.ForEach(x =>
            {
                x.l.ItemName = context.ChartOfInventory.FirstOrDefault(p => p.Id == x.l.ItemID).Name;
                x.l.RmcRate =Convert.ToDecimal( LoadComboBox.GetNewRMCRateById(x.l.ItemID, lastDate.ToString("yyyy-MM-dd")));
            });
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public static DateTime LastDayOfMonth(DateTime dt)
        {
            DateTime ss = new DateTime(dt.Year, dt.Month, 1);
            return ss.AddMonths(1).AddDays(-1);
        }

        public ActionResult GetMovingAvgCost(int id, int storeid)
        {
            var cost = LoadComboBox.GetMovingAvg(id, storeid);
            if (cost != null)
            {
                if (cost.clmStandardCost > 0)
                {
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveRMCApply(RMCApplyEntry rmcApplyEntry, List<RMCApplyLineItem> rmcAlpplyLineItems)
        {
            var ScreenID = context.Screens.FirstOrDefault(x => x.Name == "RM Consumption").ScreenID;
            var IsExpired = context.ScreenEntryLocks.FirstOrDefault(x => x.ScreenID == ScreenID);

            if (rmcApplyEntry.RMCDate > IsExpired.SELDate)
            {
                string RMCNo = "";
                string yearLastTwoPart = rmcApplyEntry.RMCDate.Year.ToString().Substring(2, 2).ToString();
                if (context.RMCApplyEntry.Count() > 0)
                {
                    var lastRMC = context.RMCApplyEntry.ToList().LastOrDefault(x => x.RMCNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                    if (lastRMC == null)
                    {
                        RMCNo = yearLastTwoPart + "00001";
                    }
                    else
                    {
                        RMCNo = yearLastTwoPart + (Convert.ToInt32(lastRMC.RMCNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                    }
                }
                else
                {
                    RMCNo = yearLastTwoPart + "00001";
                }
                rmcApplyEntry.RMCNo = Convert.ToInt32(RMCNo);
                foreach (var item in rmcAlpplyLineItems)
                {
                    rmcApplyEntry.RMCTotalQty += item.ItemQty;
                    rmcApplyEntry.RMCTotalValue += item.TotalStanCost;
                }
                var existsRMCNo = context.RMCApplyEntry.FirstOrDefault(m => m.RMCNo == rmcApplyEntry.RMCNo);
                if (existsRMCNo == null)
                {
                    //try
                    //{
                        //rmcEntry.RMCDate = rmcEntry.RMCDate.Add(DateTime.Now.TimeOfDay);
                        context.RMCApplyEntry.Add(rmcApplyEntry);
                        rmcAlpplyLineItems.ForEach(x =>
                        {
                            var item = LoadComboBox.GetItemInfo().FirstOrDefault(y => y.Id == x.ItemID);
                            //var cost = LoadComboBox.GetRMCRate(x.ItemID, rmcApplyEntry.RMCDate.ToString("yyyy-MM-dd"));
                            //if (cost != null)
                            //{
                            //    x.UnitStanCost = Convert.ToDecimal(cost);
                            //    x.RmcRate = x.UnitStanCost;
                            //    x.RmcValue = x.UnitStanCost * x.ItemQty;
                            //}
                            //else
                            //{
                            //    x.UnitStanCost = 0;
                            //    x.RmcRate = x.UnitStanCost;
                            //    x.RmcValue = x.UnitStanCost * x.ItemQty;
                            //}

                            x.TotalStanCost = Math.Round(x.ItemQty * x.UnitStanCost, 2);
                            //CommonInfo.AllocateItem(ref context, x.ItemID, "RMCBatch", x.ItemQty, rmcEntry.RMCNo, rmcEntry.StoreId, false);
                        });
                        rmcAlpplyLineItems.ForEach(x => { x.RMCNo = rmcApplyEntry.RMCNo; context.RMCApplyLineItem.Add(x); });
                        InventoryTransactionBridge.InsertFromARMCEntry(ref context, rmcAlpplyLineItems, rmcApplyEntry);
                        UserLog.SaveUserLog(ref context, new UserLog(rmcApplyEntry.RMCNo.ToString(), rmcApplyEntry.RMCDate, "ARMC", ScreenAction.Save));
                        AccountModuleBridge.InsertUpdateFromARMCBatch(ref context, rmcApplyEntry, rmcAlpplyLineItems);
                        BatchBalanceCalculationBridge.InsertUpdateFromARMCBatch(ref context, rmcApplyEntry, rmcAlpplyLineItems);
                        context.SaveChanges();
                        return Json(rmcApplyEntry.RMCNo, JsonRequestBehavior.AllowGet);
                    //}
                    //catch (Exception ex)
                    //{
                    //    return Json(new { Message = 0, JsonRequestBehavior.AllowGet });
                    //}
                }
                else
                {
                    return Json(new { Message = 2, JsonRequestBehavior.AllowGet });
                }
            }
            else
            {
                return Json(3, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetRMCApplyByRMCNo(int rmcNo)
        {
            if (rmcNo == 0)
            {
                return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var rmc = context.RMCApplyEntry.FirstOrDefault(x => x.RMCNo == rmcNo);
                if (rmc == null)
                {
                    return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    var rmcLineItem = context.RMCApplyLineItem.Where(x => x.RMCNo == rmcNo).ToList();
                    rmcLineItem.ForEach(x =>
                    {
                        var item = context.ChartOfInventory.FirstOrDefault(y => y.Id == x.ItemID);
                        x.ItemName = context.ChartOfInventory.FirstOrDefault(m => m.Id == x.ItemID).Name;
                        //x.PacSize = item.PacSize;
                    });

                    return Json(new { Message = 1, rmcEntry = rmc, rmcLineItem = rmcLineItem }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult UpdateRMCApply(RMCApplyEntry rmcApplyEntry, List<RMCApplyLineItem> rmcAlpplyLineItems)
        {
            var ScreenID = context.Screens.FirstOrDefault(x => x.Name == "RM Consumption").ScreenID;
            var IsExpired = context.ScreenEntryLocks.FirstOrDefault(x => x.ScreenID == ScreenID);

            if (rmcApplyEntry.RMCDate > IsExpired.SELDate)
            {
                var existsRMCNo = context.RMCApplyEntry.FirstOrDefault(m => m.RMCNo == rmcApplyEntry.RMCNo);
                var existsRMCLine = context.RMCApplyLineItem.Where(x => x.RMCNo == rmcApplyEntry.RMCNo).ToList();
                if (existsRMCNo!=null)
                {
                    context.RMCApplyEntry.Remove(existsRMCNo);
                    existsRMCLine.ForEach(x =>
                    {
                        context.RMCApplyLineItem.Remove(x);
                    });

                }
                rmcApplyEntry.RMCNo = Convert.ToInt32(existsRMCNo.RMCNo);
                foreach (var item in rmcAlpplyLineItems)
                {
                    rmcApplyEntry.RMCTotalQty += item.ItemQty;
                    rmcApplyEntry.RMCTotalValue += item.TotalStanCost;
                }
                //try
                //{
                context.RMCApplyEntry.Add(rmcApplyEntry);
                rmcAlpplyLineItems.ForEach(x =>
                {
                    var item = LoadComboBox.GetItemInfo().FirstOrDefault(y => y.Id == x.ItemID);
                    x.TotalStanCost = Math.Round(x.ItemQty * x.UnitStanCost, 2);
                });
                rmcAlpplyLineItems.ForEach(x => { x.RMCNo = rmcApplyEntry.RMCNo; context.RMCApplyLineItem.Add(x); });
                InventoryTransactionBridge.InsertFromARMCEntry(ref context, rmcAlpplyLineItems, rmcApplyEntry);
                UserLog.SaveUserLog(ref context, new UserLog(rmcApplyEntry.RMCNo.ToString(), rmcApplyEntry.RMCDate, "ARMC", ScreenAction.Save));
                AccountModuleBridge.InsertUpdateFromARMCBatch(ref context, rmcApplyEntry, rmcAlpplyLineItems);
                BatchBalanceCalculationBridge.InsertUpdateFromARMCBatch(ref context, rmcApplyEntry, rmcAlpplyLineItems);
                context.SaveChanges();
                return Json(rmcApplyEntry.RMCNo, JsonRequestBehavior.AllowGet);
                //}
                //catch (Exception ex)
                //{
                //    return Json(new { Message = 0, JsonRequestBehavior.AllowGet });
                //}
                
            }
            else
            {
                return Json(3, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult DeleteRMCApply(RMCApplyEntry rmcConsumption, List<RMCApplyLineItem> addedItems)
        {


            var isExist = context.RMCApplyEntry.AsNoTracking().FirstOrDefault(x => x.RMCNo == rmcConsumption.RMCNo);
            if (isExist == null)
            {
                return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
            }
            else
            {

                var prevItem = context.RMCApplyLineItem.Where(x => x.RMCNo == rmcConsumption.RMCNo).ToList();
                prevItem.ForEach(x =>
                {
                    context.RMCApplyLineItem.Remove(x);
                });
                context.Entry<RMCApplyEntry>(isExist).State = EntityState.Deleted;
                //context.RMCEntry.Remove(isExist);
                InventoryTransactionBridge.DeleteFromARMCEntry(ref context, rmcConsumption.RMCNo);
                BatchBalanceCalculationBridge.DeleteFromARMCBatch(ref context, rmcConsumption);
                AccountModuleBridge.DeleteFromARMCBatch(ref context, rmcConsumption.RMCNo);
                UserLog.SaveUserLog(ref context, new UserLog(rmcConsumption.RMCNo.ToString(), DateTime.Now, "ARMC", ScreenAction.Delete));
                context.SaveChanges();
            }
            return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
        }

    }
}