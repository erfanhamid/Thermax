using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.InventoryModule;

namespace BEEERP.Controllers.InventoryModule
{
    [ShowNotification]
    public class InventoryAdditionalInfoController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: InventoryAdditionalInfo
        public ActionResult InventoryAdditionalInfo()
        {
            ViewBag.InvType = LoadComboBox.LoadInventoryType();
            ViewBag.Group = LoadComboBox.LoadItemGroupRM();
            ViewBag.Item = LoadComboBox.LoadInventoryItem();
            ViewBag.Country = LoadComboBox.LoadAllCountry();
            return View();
        }

        public ActionResult SaveInventoryAdditionalInfo(InventoryAdditionalInfo iai)
        {
            var data = context.InventoryAdditionalInfo.Where(x => x.ItemID == iai.ItemID).Count();
            if (data > 0)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                context.InventoryAdditionalInfo.Add(iai);
                context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
        }
    }
}