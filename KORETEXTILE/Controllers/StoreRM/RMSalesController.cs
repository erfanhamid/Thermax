using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Bridge;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BEEERP.Models.StoreRM;
using BEEERP.Models.StoreRM.RMSalesEntry;
using BEEERP.Controllers.StoreDepot;

namespace BEEERP.Controllers.StoreRM
{
    [ShowNotification]
    public class RMSalesController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: RMSales
        public ActionResult RMSales()
        {
            ViewBag.Customer = LoadComboBox.LoadRMCustomer();
            ViewBag.Item = LoadComboBox.LoadAllItem();
            ViewBag.StoreId = LoadComboBox.LoadAllRMStore();
            ViewBag.Group = LoadItemGroup();
            ViewBag.Item = LoadItem(null);
            return View();
        }
        public static SelectList LoadItemGroup()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Group ---");
            string sql = "select id as ItemID, Name as ItemName from chartofInv where type = 'g' and rootAccountType = 'RM' ";
            List<ItemInfo> Data = context.Database.SqlQuery<ItemInfo>(sql).ToList();
            Data.ForEach(x => { items.Add(x.ItemID.ToString(), x.ItemID.ToString() + " - " + x.ItemName); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadItem(int? group)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            if (group == null)
            {
                items.Add("", "--- Select Item ---");
                return new SelectList(items, "Key", "Value");
            }
            else
            {
                items.Add("", "--- Select Item ---");
                string sql = "select id as ItemID, Name as ItemName from chartofInv where type = 'l' and parentId = " + group + "";
                List<ItemInfo> Data = context.Database.SqlQuery<ItemInfo>(sql).ToList();
                Data.ForEach(x => { items.Add(x.ItemID.ToString(), x.ItemID.ToString() + " - " + x.ItemName); });
                return new SelectList(items, "Key", "Value");
            }

        }
        public ActionResult GetItemRemainQty(int itemId, int storeId,string date)
        {
            var item = FindObjectById.GetChartOfInventoryById(itemId);
            var rate= LoadComboBox.GetRMCRate(itemId,Convert.ToDateTime( date).ToString("yyyy-MM-dd"));
            return Json(new { avgrate = rate /*SaleCommonInfo.GetAvarageRate(itemId, storeId, null)*/, RemQty = SaleCommonInfo.GetRemainItemQty(itemId, storeId, null) }, JsonRequestBehavior.AllowGet);
            
        }
        public ActionResult GetItemByGroupId(int GroupId)
        {
            return Json(LoadItem(GroupId), JsonRequestBehavior.AllowGet);

        }
        public ActionResult SaveRMSales(RMSales rms, List<TblRMSalesLineItem> addedItems)
        {
            string rmsNo = "";
            string yearLastTwoPart = rms.clmDate.Year.ToString().Substring(2, 2).ToString();
            var noOfrms = context.RMSales.Count();


            if (noOfrms > 0)
            {
                var lastrms = context.RMSales.ToList().LastOrDefault(x => x.clmRMSalesNo.ToString().Substring(0, 2) == yearLastTwoPart);
                if (lastrms == null)
                {
                    rmsNo = yearLastTwoPart + "00001";
                }
                else
                {
                    rmsNo = yearLastTwoPart + (Convert.ToInt32(lastrms.clmRMSalesNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                }
            }
            else
            {
                rmsNo = yearLastTwoPart + "00001";
            }
            rms.clmRMSalesNo = Convert.ToInt32(rmsNo);
            addedItems.ForEach(x =>
            {
                x.clmRMSalesNo = rms.clmRMSalesNo;
                context.TblRMSalesLineItem.Add(x);
            });

            context.RMSales.Add(rms);
            AccountModuleBridge.InsertUpdateFromRMSales(ref context, rms,addedItems);
            RMCustomerCalculationBridge.InsertUpdateFromRMSales(ref context, rms);
            InventoryTransactionBridge.InsertFromRMSalesEntry(ref context, rms, addedItems);

            UserLog.SaveUserLog(ref context, new UserLog(rms.clmRMSalesNo.ToString(), DateTime.Now, "RMSales", ScreenAction.Save));
            context.SaveChanges();
            return Json(new { clmRMSalesNo = rms.clmRMSalesNo }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetRMSalesById(int id)
        {
            var rms = context.RMSales.FirstOrDefault(x => x.clmRMSalesNo == id);

            if (rms != null)
            {
                var rmsLineItem = context.TblRMSalesLineItem.Where(x => x.clmRMSalesNo == id).ToList().Select(x =>
                {
                    var item = context.TblRMSalesLineItem.FirstOrDefault(y => y.clmRMSalesNo == x.clmRMSalesNo && y.clmItemID == x.clmItemID);
                    x.clmItemValue = item.clmItemValue;
                    x.clmItemRate = item.clmItemRate;
                    x.clmCOGSRate = item.clmCOGSRate;
                    x.clmCOGSTotal = item.clmCOGSTotal;
                    x.clmItemQty = item.clmItemQty;
                    x.itemName = FindObjectById.GetChartOfInventoryById(x.clmItemID).Name;
                    x.GroupId = FindObjectById.GetChartOfInventoryById(x.clmItemID).ParentId;
                    var gid= FindObjectById.GetChartOfInventoryById(x.clmItemID).ParentId;
                    x.GroupName= FindObjectById.GetChartOfInventoryById(gid).Name;
                    return x;
                }).ToList();
                return Json(new { rms, rmsLineItem }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
        //UpdateRMSales

        public ActionResult UpdateRMSales(RMSales rms, List<TblRMSalesLineItem> addedItems)
        {
            using (context)
            {
                var rmsExist = context.RMSales.FirstOrDefault(x => x.clmRMSalesNo == rms.clmRMSalesNo);
                if (rmsExist != null)
                {
                    context.RMSales.Remove(rmsExist);

                    context.TblRMSalesLineItem.Where(x => x.clmRMSalesNo == rms.clmRMSalesNo).ToList().ForEach(x =>
                    {
                        context.TblRMSalesLineItem.Remove(x);
                    });

                    addedItems.ForEach(x =>
                    {
                        x.clmRMSalesNo = rms.clmRMSalesNo;
                        context.TblRMSalesLineItem.Add(x);
                    });

                    rms.clmDate = rms.clmDate.Add(DateTime.Now.TimeOfDay);
                    context.RMSales.Add(rms);
                    UserLog.SaveUserLog(ref context, new UserLog(rms.clmRMSalesNo.ToString(), DateTime.Now, "RMSales", ScreenAction.Update));
                    AccountModuleBridge.InsertUpdateFromRMSales(ref context, rms, addedItems);
                    RMCustomerCalculationBridge.InsertUpdateFromRMSales(ref context, rms);
                    InventoryTransactionBridge.InsertFromRMSalesEntry(ref context, rms, addedItems);
                    context.SaveChanges();
                    return Json(new { clmRMSalesNo = rms.clmRMSalesNo }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult DeleteRMSales(int id)
        {
            var rmsExist = context.RMSales.FirstOrDefault(x => x.clmRMSalesNo == id);
            if (rmsExist != null)
            {
                context.RMSales.Remove(rmsExist);
                context.TblRMSalesLineItem.Where(x => x.clmRMSalesNo == id).ToList().ForEach(x =>
                {
                    context.TblRMSalesLineItem.Remove(x);
                });
                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "RMSales", ScreenAction.Delete));
                AccountModuleBridge.DeleteFromRMSales(ref context, id);
                RMCustomerCalculationBridge.DeleteFromRMSales(ref context, id);
                InventoryTransactionBridge.DeleteRMSalesEntry(ref context, id);
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }
    }
}