using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.DataAdmin.SPSettings;

namespace BEEERP.Controllers.Data_Admin.Setting
{
    [ShowNotification]
    public class SPItemTypeController : Controller
    {
        BEEContext Context = new BEEContext();
        // GET: SPItemType
        public ActionResult SPItemType()
        {
            ViewBag.DepartmentID = LoadComboBox.LoadAllSparePartsDepartment();
            ViewBag.ItemType = Context.SPItemType.ToList();

            var itemType = Context.SPItemType.ToList();
            var maxId = 0;
            if (itemType.Count > 0)
            {
                var mid = Context.SPItemType.Max(x => x.ItemTypeID);
                maxId = mid + 1;
            }
            else
            {
                maxId = 1;
            }
            ViewBag.Id = maxId;

            var items = new List<ItemTypeVModel>();
            Context.SPItemType.ToList().ForEach(x =>
            {
                items.Add(new ItemTypeVModel
                {
                    ItemTypeID = x.ItemTypeID,
                    ItemType = x.ItemType,
                    DepartmentName = GetDepartmentName(x.DepartmentID)

                });

            });

            ViewBag.ItemType = items;

            return View();
        }

        public string GetDepartmentName(int id)
        {
            var item = Context.SparePartsDepartment.FirstOrDefault(x => x.SPDId == id);
            if (item == null)
            {
                return string.Empty;
            }
            else
            {
                return item.SPDName;
            }
        }





        public ActionResult SaveItemTypeInfo(SPItemType itemtype)
        {

            var nameCheck = Context.SPItemType.FirstOrDefault(x => x.ItemType == itemtype.ItemType);

            if (nameCheck == null)
            {
                Context.SPItemType.Add(itemtype);
                Context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

            
        }

        public ActionResult GetItemTypeById(int id)
        {

            var itemDetails = Context.SPItemType.ToList().FirstOrDefault(x => x.ItemTypeID == id);

            if (itemDetails == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(new { item = itemDetails }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult UpdateItemType(SPItemType itemtype)
        {
            var itemExit = Context.SPItemType.FirstOrDefault(x => x.ItemTypeID == itemtype.ItemTypeID);

            if (itemExit != null)
            {
                Context.SPItemType.Remove(itemExit);
                Context.SPItemType.Add(itemtype);
                Context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteItemTypeByid(int id)
        {
            var itemExit = Context.SPItemType.FirstOrDefault(x => x.ItemTypeID == id);

            if (itemExit != null)
            {
                Context.SPItemType.Remove(itemExit);
                Context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }


    }

    public class ItemTypeVModel
    {
        public int ItemTypeID { get; set; }
        public string ItemType { get; set; }
        public string DepartmentName { get; set; }
    }
}