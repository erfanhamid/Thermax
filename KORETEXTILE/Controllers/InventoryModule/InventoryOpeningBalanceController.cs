using BEEERP.Models.Bridge;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Data_Admin;
using BEEERP.Models.Database;
using BEEERP.Models.InventoryModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.InventoryModule
{
    [ShowNotification]
    public class InventoryOpeningBalanceController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: InventoryOpeningBalance
        public ActionResult InventoryOpeningBalance()
        {

            ViewBag.GroupId = LoadComboBox.LoadGroupGS();
            ViewBag.ItemId = LoadComboBox.LoadAllItemByID(null);
            ViewBag.Item = LoadComboBox.LoadInventoryItem();
            ViewBag.Store = LoadComboBox.LoadAllStore();
            ViewBag.BoxID = LoadComboBox.LoadAllSPBox(null);
            ViewBag.Date = FindObjectById.GetCompanySetup().ToString("dd-MM-yyyy");
            ViewBag.Company = LoadComboBox.LoadAllCompanyID();
            ViewBag.DepartmentID = LoadComboBox.LoadAllSparePartsDepartment();
            ViewBag.SpareId = context.InventoryOpeningBalance.ToList();


            var items = new List<InventoryOpeingBalanceVModel>();
            context.InventoryOpeningBalance.ToList().ForEach(x =>
            {
                items.Add(new InventoryOpeingBalanceVModel
                {
                    GroupName = GetGroupName(x.GroupId),
                    ItemId = x.ItemId,
                    ItemName = GetItemName(x.ItemId),
                    IOBDate = x.IOBDate,
                    Quantity = x.ItemQty,
                    Rate = x.ItemRate,
                    Value = x.ItemValue,
                    PageNo = x.PageNo,
                    StoreName = GetStoreName(x.StoreId),
                    BoxName = GetBoxName(x.BoxID),
                    CompanyName = GetCompanyName(x.CompanyID),
                });
            });

            ViewBag.SpareId = items;
            return View();
        }

        public string GetGroupName(int id)
        {
            var item = context.ChartOfInventory.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return string.Empty;
            }
            else
            {
                return item.Name;
            }
        }

        public string GetItemName(int id)
        {
            var item = context.ChartOfInventory.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return string.Empty;
            }
            else
            {
                return item.Name;
            }
        }




        public string GetStoreName(int id)
        {
            var item = context.Store.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return string.Empty;
            }
            else
            {
                return item.Name;
            }
        }
        public string GetBoxName(int id)
        {
            var item = context.SPBox.FirstOrDefault(x => x.BoxID == id);
            if (item == null)
            {
                return string.Empty;
            }
            else
            {
                return item.BoxNo;
            }
        }

        public string GetCompanyName(int id)
        {
            var item = context.CompanyList.FirstOrDefault(x => x.CompanyID == id);
            if (item == null)
            {
                return string.Empty;
            }
            else
            {
                return item.CompanyName;
            }
        }


        public ActionResult SaveInventoryOpeningBalance(InventoryOpeningBalance ob)
        {

            var data = context.InventoryOpeningBalance.FirstOrDefault(x => x.ItemId == ob.ItemId && x.StoreId == ob.StoreId && x.CompanyID == ob.CompanyID);
            if (data == null)
            {
                context.InventoryOpeningBalance.Add(ob);


                InventoryTransactionBridge.InsertFromIOB(ref context, ob);

                context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult GetOpeningBalanceByid(int id)
        {
            var iob = context.InventoryOpeningBalance.FirstOrDefault(x => x.ItemId == id);
            if (iob == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { item = iob }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateInventoryOpeningBalance(InventoryOpeningBalance ob)
        {
            var balance = context.InventoryOpeningBalance.FirstOrDefault(x => x.ItemId == ob.ItemId);

            if (balance != null)
            {
                context.InventoryOpeningBalance.Remove(balance);
                context.InventoryOpeningBalance.Add(ob);


                InventoryTransactionBridge.UpdateFromIOB(ref context, ob);

                context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteInventoryOpeningBalanceByid(int id)
        {

            var obinfo = context.InventoryOpeningBalance.FirstOrDefault(x => x.ItemId == id);
            if (obinfo != null)
            {
                context.InventoryOpeningBalance.Remove(obinfo);

                InventoryTransactionBridge.DeleteFromIOB(ref context, id);

                context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }


        }

    }

    public class InventoryOpeingBalanceVModel
    {
        public string GroupName { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public DateTime IOBDate { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Value { get; set; }
        public string StoreName { get; set; }
        public string BoxName { get; set; }
        public string CompanyName { get; set; }
        public string PageNo { get; set; }
    }

}