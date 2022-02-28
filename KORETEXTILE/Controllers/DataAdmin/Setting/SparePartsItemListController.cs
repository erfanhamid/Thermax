using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.DataAdmin.SPSettings;
using BEEERP.Models.Database;

namespace BEEERP.Controllers.Data_Admin.Setting
{
    [ShowNotification]
    public class SparePartsItemListController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: SparePartsItemList
        public ActionResult SparePartsItemList()
        {
            ViewBag.DepartmentID = LoadComboBox.LoadAllSparePartsDepartment();
            ViewBag.SPTypeID = LoadComboBox.loadAllSPItemType(null);
            ViewBag.UoMID = LoadComboBox.LoadAllUOM();
            ViewBag.SpareId = context.SparePartsItemList.ToList();

            var spare = context.SparePartsItemList.ToList();
            var MaxId = 0;
            if (spare.Count > 0)
            {
                var sParts = context.SparePartsItemList.Max(x => x.SPItemID);
                MaxId = sParts + 1;
            }
            else
            {
                MaxId = 1;
            }
            ViewBag.Id = MaxId;


            var items = new List<SparePartsVModel>();
            context.SparePartsItemList.ToList().ForEach(x =>
            {
                items.Add(new SparePartsVModel
                {
                    SPItemID = x.SPItemID,
                    SPItemPartNo = x.SPItemPartNo,
                    SPItemType = GetSPItemType(x.SPTypeID),
                    UomName = GetUoMID(x.UoMID)
                });
            });

            ViewBag.SpareId = items;

            return View();
        }

        public string GetSPItemType(int id)
        {
            var item = context.SPItemType.FirstOrDefault(x => x.ItemTypeID == id);
            if (item == null)
            {
                return string.Empty;
            }
            else
            {
                return item.ItemType;
            }
        }
        public string GetUoMID(int id)
        {
            var item = context.UOM.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return string.Empty;
            }
            else
            {
                return item.Name;
            }
        }

        public ActionResult GetSPTypeByBatchID(int id)
        {
            return Json(LoadComboBox.loadAllSPItemType(id), JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetSparePartsById(int id)
        {
            var parts = context.SparePartsItemList.ToList().FirstOrDefault(x => x.SPItemID == id);
            parts.DepartmentID = context.SPItemType.Where(x => x.ItemTypeID == parts.SPTypeID).SingleOrDefault().DepartmentID;
            if (parts == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { item = parts }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveSpareParts(SparePartsItemList sp)
        {
            var partNoExist = context.SparePartsItemList.FirstOrDefault(x => x.SPItemPartNo == sp.SPItemPartNo && x.SPTypeID == sp.SPTypeID);
            if (partNoExist == null)
            {
                context.SparePartsItemList.Add(sp);
                context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            //sp.SPItemID = MaxId;
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult UpdateSpareParts(SparePartsItemList sp)
        {
            var parts = context.SparePartsItemList.FirstOrDefault(x => x.SPItemID == sp.SPItemID);

            if (parts != null)
            {
                context.SparePartsItemList.Remove(parts);
                context.SparePartsItemList.Add(sp);
                context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteSparePartsByid(int id)
        {
            var partsExit = context.SparePartsItemList.FirstOrDefault(x => x.SPItemID == id);

            if (partsExit != null)
            {
                context.SparePartsItemList.Remove(partsExit);
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

    }

    public class SparePartsVModel
    {
        public int SPItemID { get; set; }
        public string SPItemPartNo { get; set; }
        public string SPItemType { get; set; }
        //public string SPDepartmentName { get; set; }
        public string UomName { get; set; }
    }
}