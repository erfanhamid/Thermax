using BEEERP.Models.Bridge;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using Microsoft.AspNet.Identity;
using BEEERP.Models.Log;
using BEEERP.Models.Notification;
using BEEERP.Models.SpareParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.SpareParts
{
    [ShowNotification]
    public class StorePurchaseRequitionController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: StorePurchaseRequition
        public ActionResult StorePurchaseRequisitionView(string type = "", int sprno = 0)
        {

            //ViewBag.Depaertment = LoadComboBox.LoadSPDepartments();
            ViewBag.Company = LoadComboBox.LoadAllCompanyID();
            ViewBag.Store = LoadComboBox.LoadAllStore();
            ViewBag.Group = LoadComboBox.LoadGSItemGroup();
            ViewBag.Item = LoadComboBox.LoadAllItem();
            ViewBag.Item = LoadItem(null);


            if (sprno != 0)
            {
                var notificationE = context.Notification.FirstOrDefault(x => x.Type == type && x.TransactionNo == sprno.ToString() && x.NotificationDetails == "This Store Purchase Requisition is pending for approved.");
                ViewBag.MRVNo = sprno;
                ViewBag.IsNotify = 1;
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
                ViewBag.IsNotify = 0;
            }



            return View();
        }
        public static SelectList LoadItemGroup()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Group ---");
            string sql = "select id as ItemID, Name as ItemName from chartofInv where type = 'g' and rootAccountType = 'SP' ORDER BY Name";
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
        public ActionResult GetItemByGroupId(int GroupId)
        {
            return Json(LoadItem(GroupId), JsonRequestBehavior.AllowGet);

        }
        public class ItemInfo
        {
            public int ItemID { get; set; }
            public string ItemName { get; set; }
            public int GroupId { get; set; }
            public decimal UnitCost { get; set; }
            public int UOM { get; set; }
            public decimal clmStandardCost { get; set; }
        }
        public ActionResult GetItemRemainQty(int itemId, int storeId, string date)
        {
            var item = FindObjectById.GetChartOfInventoryById(itemId);
            var rate = LoadComboBox.GetRMCRate(itemId, Convert.ToDateTime(date).ToString("yyyy-MM-dd"));

            var uOMID = context.ChartOfInventory.FirstOrDefault(x => x.Id == itemId).UoMID;

            string uomName = context.UOM.FirstOrDefault(x => x.Id == uOMID).Name; 




            return Json(new { uom = uomName, avgrate = rate /*SaleCommonInfo.GetAvarageRate(itemId, storeId, null)*/, RemQty = SaleCommonInfo.GetRemainItemQty(itemId, storeId, null) }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult SaveSPR(StorePurchaseRequistion spr, List<StorePurchaseRequisitionLineItem> addedItems)
        {
            string sprNo = "";
            string yearLastTwoPart = spr.SPRDate.Year.ToString().Substring(2, 2).ToString();
            var noOfrms = context.StorePurchaseRequistion.Count();


            if (noOfrms > 0)
            {
                var lastrms = context.StorePurchaseRequistion.ToList().LastOrDefault(x => x.SPRNo.ToString().Substring(0, 2) == yearLastTwoPart);
                if (lastrms == null)
                {
                    sprNo = yearLastTwoPart + "00001";
                }
                else
                {
                    sprNo = yearLastTwoPart + (Convert.ToInt32(lastrms.SPRNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                }
            }
            else
            {
                sprNo = yearLastTwoPart + "00001";
            }
            spr.SPRNo = Convert.ToInt32(sprNo);
            spr.SPRDate = spr.SPRDate.Add(DateTime.Now.TimeOfDay);
            addedItems.ForEach(x =>
            {
                x.SPRNo = spr.SPRNo;
                context.StorePurchaseRequisitionLineItem.Add(x);
            });

            context.StorePurchaseRequistion.Add(spr);

            //spr.SPRNo = Convert.ToInt32(id);

            UserNotification notification = new UserNotification();
            notification.UserId = User.Identity.GetUserId();
            notification.Type = "SPR";
            notification.TransactionNo = spr.SPRNo.ToString();
            notification.NotificationHead = "New Store Purchase Requisition";
            notification.NotificationDetails = "This Store Purchase Requisition is pending for approved.";
            notification.PostingDate = DateTime.Now;
            notification.BranchId = 0;

            context.Notification.Add(notification);


            //AccountModuleBridge.InsertUpdateFromRMSales(ref context, spr, addedItems);
            //RMCustomerCalculationBridge.InsertUpdateFromRMSales(ref context, spr);
            //InventoryTransactionBridge.InsertFromRMSalesEntry(ref context, spr, addedItems);

            //UserLog.SaveUserLog(ref context, new UserLog(rms.clmRMSalesNo.ToString(), DateTime.Now, "RMSales", ScreenAction.Save));
            context.SaveChanges();
            return Json(new { sprno = spr.SPRNo }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSPRById(int id)
        {
            var spr = context.StorePurchaseRequistion.FirstOrDefault(x => x.SPRNo == id);

            if (spr != null)
            {
                var sprLineItem = context.StorePurchaseRequisitionLineItem.Where(x => x.SPRNo == id).ToList().Select(x =>
                {
                    var item = context.StorePurchaseRequisitionLineItem.FirstOrDefault(y => y.SPRNo == x.SPRNo && y.ItemID == x.ItemID);
                    x.ItemValue = item.ItemValue;
                    x.ItemRate = item.ItemRate;
                    //x.clmCOGSRate = item.COGSRate;
                    //x.clmCOGSTotal = item.COGSTotal;
                    x.ItemQty = item.ItemQty;
                    x.ItemName = FindObjectById.GetChartOfInventoryById(x.ItemID).Name;
                    x.GroupId = FindObjectById.GetChartOfInventoryById(x.ItemID).ParentId;
                    var gid = FindObjectById.GetChartOfInventoryById(x.ItemID).ParentId;
                    x.GroupName = FindObjectById.GetChartOfInventoryById(gid).Name;
                    return x;
                }).ToList();
                return Json(new { spr, sprLineItem }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
        //UpdateRMSales

        public ActionResult UpdateRMSales(StorePurchaseRequistion spr, List<StorePurchaseRequisitionLineItem> addedItems)
        {
            using (context)
            {
                var rmsExist = context.StorePurchaseRequistion.FirstOrDefault(x => x.SPRNo == spr.SPRNo);
                if (rmsExist != null)
                {
                    context.StorePurchaseRequistion.Remove(rmsExist);

                    context.StorePurchaseRequisitionLineItem.Where(x => x.SPRNo == spr.SPRNo).ToList().ForEach(x =>
                    {
                        context.StorePurchaseRequisitionLineItem.Remove(x);
                    });

                    addedItems.ForEach(x =>
                    {
                        x.SPRNo = spr.SPRNo;
                        context.StorePurchaseRequisitionLineItem.Add(x);
                    });

                    spr.SPRDate = spr.SPRDate.Add(DateTime.Now.TimeOfDay);
                    context.StorePurchaseRequistion.Add(spr);
                    //UserLog.SaveUserLog(ref context, new UserLog(rms.clmRMSalesNo.ToString(), DateTime.Now, "RMSales", ScreenAction.Update));
                    //AccountModuleBridge.InsertUpdateFromRMSales(ref context, rms, addedItems);
                    //RMCustomerCalculationBridge.InsertUpdateFromRMSales(ref context, rms);
                    //InventoryTransactionBridge.InsertFromRMSalesEntry(ref context, rms, addedItems);
                    context.SaveChanges();
                    return Json(new { sprno = spr.SPRNo }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult DeleteRMSales(int id)
        {
            var rmsExist = context.StorePurchaseRequistion.FirstOrDefault(x => x.SPRNo == id);
            if (rmsExist != null)
            {
                context.StorePurchaseRequistion.Remove(rmsExist);
                context.StorePurchaseRequisitionLineItem.Where(x => x.SPRNo == id).ToList().ForEach(x =>
                {
                    context.StorePurchaseRequisitionLineItem.Remove(x);
                });
                //UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "RMSales", ScreenAction.Delete));
                //AccountModuleBridge.DeleteFromRMSales(ref context, id);
                //RMCustomerCalculationBridge.DeleteFromRMSales(ref context, id);
                //InventoryTransactionBridge.DeleteRMSalesEntry(ref context, id);
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }
    }
}