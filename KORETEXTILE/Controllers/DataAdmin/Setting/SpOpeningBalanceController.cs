using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.DataAdmin.SPSettings;
using BEEERP.Models.Bridge;

namespace BEEERP.Controllers.Data_Admin.Setting
{
    [ShowNotification]
    public class SpOpeningBalanceController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: SpOpeningBalance
        public ActionResult SpOpeningBalance()
        {
            ViewBag.GroupId = LoadComboBox.LoadAllSPGroup();
            ViewBag.ItemId = LoadComboBox.LoadAllItemByID(null);
            ViewBag.TypeId = LoadComboBox.LoadAllTypeId();
            ViewBag.StoreId = LoadComboBox.LoadAllStore();
            ViewBag.BoxID = LoadComboBox.LoadAllSPBox(null);
            ViewBag.CompanyID = LoadComboBox.LoadAllCompanyID();
            ViewBag.Date = FindObjectById.GetCompanySetup().ToString("dd-MM-yyyy");

            ViewBag.SpareId = context.SpOpeningBalance.ToList();


            var spob = context.SpOpeningBalance.ToList();
            var maxId = 0;
            if (spob.Count > 0)
            {
                var ob = context.SpOpeningBalance.Max(x => x.ID);
                maxId = ob + 1;
            }
            else
            {
                maxId = 1;
            }
            ViewBag.Id = maxId;


            var items = new List<SpOpeningBalanceVModel>();
            context.SpOpeningBalance.ToList().ForEach(x =>
            {
                items.Add(new SpOpeningBalanceVModel
                {
                    ID = x.ID,
                    GroupName = GetGroupName(x.GroupId),
                    ItemId = x.ItemId,
                    ItemName = GetItemName(x.ItemId),
                    SpDate = x.SpDate,
                    Quantity = x.Quantity,
                    Rate = x.Rate,
                    Value = x.Value,
                    PageNo = x.PageNo,
                    TypeName = GetTypeName(x.TypeId),
                    StoreName = GetStoreName(x.StoreId),
                    BoxID = x.BoxID,
                    BoxNo = GetBoxNo(x.BoxID),
                    CompanyName = GetCompanyName(x.CompanyID)
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

        public string GetTypeName(int id)
        {
            var item = context.SPType.FirstOrDefault(x => x.SPTypeId == id);
            if (item == null)
            {
                return string.Empty;
            }
            else
            {
                return item.SPTypeName;
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
        public string GetBoxNo(int id)
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


        public ActionResult GetItemByBatchID(int id)
        {
            return Json(LoadComboBox.LoadAllItemByID(id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBoxByBatchID(int id)
        {
            return Json(LoadComboBox.LoadAllSPBox(id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOpeningBalanceByid(int id)
        {
            var ob = context.SpOpeningBalance.FirstOrDefault(x => x.ID == id);
            //ob.SPTypeID = context.SparePartsItemList.Where(x => x.SPItemID == ob.ItemId).SingleOrDefault().SPTypeID;
            //ob.DepartmentID = context.SPItemType.Where(x => x.ItemTypeID == ob.SPTypeID).SingleOrDefault().DepartmentID;
            if (ob == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { item = ob }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveSpOpeningBalance(SpOpeningBalance spob)
        {

            var obExists = context.SpOpeningBalance.FirstOrDefault(x => x.ItemId == spob.ItemId && x.TypeId == spob.TypeId && x.CompanyID == spob.CompanyID && x.BoxID == spob.BoxID);

            if (obExists == null)
            {
                context.SpOpeningBalance.Add(spob);

                SPInventoryTransactionBridge.InsertFromSPOpeningBalance(ref context, spob);


                context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult UpdateSpOpeningBalance(SpOpeningBalance spob)
        {
            var balance = context.SpOpeningBalance.FirstOrDefault(x => x.ID == spob.ID);

            if (balance != null)
            {
                context.SpOpeningBalance.Remove(balance);
                context.SpOpeningBalance.Add(spob);


                SPInventoryTransactionBridge.UpdateFromSPOpeningBalance(ref context, spob);

                context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteSpOpeningBalanceByid(int id)
        {
            var openingExit = context.SpOpeningBalance.FirstOrDefault(x => x.ID == id);

      
                context.SpOpeningBalance.Remove(openingExit);

                SPInventoryTransactionBridge.DeleteFromSPOpeningBalance(ref context, id);

                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
        }
           
    }



    public class SpOpeningBalanceVModel
    {
        public int ID { get; set; }
        public string GroupName { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public DateTime SpDate { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Value { get; set; }
        public string TypeName { get; set; }
        public string StoreName { get; set; }
        public int BoxID { get; set; }
        public string BoxNo { get; set; }
        public string CompanyName { get; set; }
        public string PageNo { get; set; }
    }
}