using BEEERP.Models.CommonInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.DataAdmin.SPSettings;

namespace BEEERP.Controllers.DataAdmin.Setting
{
    [ShowNotification]
    public class SPAdditionalInfoController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: SPAdditionalInfo
        public ActionResult SPAdditionalInfoView()
        {
            ViewBag.SPD = LoadComboBox.LoadAllSparePartsDepartment();
            ViewBag.ItemID = LoadComboBox.LoadAllItemList();
            ViewBag.SPTypeID = LoadComboBox.loadAllSPItemType(null);
            ViewBag.Country = LoadComboBox.LoadAllCountry();
            ViewBag.SpareId = context.SPAdditionalInfo.ToList();

            var items = new List<AdditionalInfoVModel>();
            context.SPAdditionalInfo.ToList().ForEach(x =>
            {
                items.Add(new AdditionalInfoVModel
                {
                    ItemID = x.ItemID,
                    ItemPartNo = GetItemPartNo(x.ItemID),
                    StandardCost = x.StandardCost,
                    MinStockLevel = x.MinStockLevel,
                    MaxStockLevel = x.MaxStockLevel,
                    ReorderLevel = x.ReorderLevel,
                    EconomicOQ = x.EconomicOQ,
                    CountryID = x.CountryID,
                    CountryName = GetCountryName(x.CountryID)
                });
            });
            ViewBag.SpareId = items;
            return View();
        }

        public string GetItemPartNo(int id)
        {
            var item = context.SparePartsItemList.FirstOrDefault(x => x.SPItemID == id);
            if (item == null)
            {
                return string.Empty;
            }
            else
            {
                return item.SPItemPartNo;
            }
        }
        public string GetCountryName(int id)
        {
            var item = context.Countries.FirstOrDefault(x => x.ID == id);
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

        public ActionResult GetAdditionalInfoByid(int id)
        {
            var additional = context.SPAdditionalInfo.FirstOrDefault(x => x.ItemID == id);
            if (additional == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { item = additional }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveInventoryAdditionalInfo(SPAdditionalInfo iai)
        {
            context.SPAdditionalInfo.Add(iai);
            context.SaveChanges();
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateAdditionalInfo(SPAdditionalInfo iai)
        {
            var additional = context.SPAdditionalInfo.FirstOrDefault(x => x.ItemID == iai.ItemID);
            if (additional != null)
            {
                context.SPAdditionalInfo.Remove(additional);
                context.SPAdditionalInfo.Add(iai);
                context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteAdditionalInfoByid(int id)
        {
            var additional = context.SPAdditionalInfo.FirstOrDefault(x => x.ItemID == id);
            if (additional != null)
            {
                context.SPAdditionalInfo.Remove(additional);
                context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
    }
    public class AdditionalInfoVModel
    {
        public int ItemID { get; set; }
        public string ItemPartNo { get; set; }
        public decimal StandardCost { get; set; }
        public decimal MinStockLevel { get; set; }
        public decimal MaxStockLevel { get; set; }
        public decimal ReorderLevel { get; set; }
        public decimal EconomicOQ { get; set; }
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        //public string Remarks { get; set; }
    }
}