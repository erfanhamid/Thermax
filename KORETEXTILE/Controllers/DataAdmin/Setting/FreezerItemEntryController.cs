using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Procurement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Data_Admin.Setting
{
    [ShowNotification]
    public class FreezerItemEntryController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: FreezerItemEntry
        public ActionResult FreezerItemEntry()
        {
            ViewBag.messege = "Freezer New Item Entry";
            ViewBag.TypeList = LoadComboBox.LoadAllFWOTypeList();
            ViewBag.BrandList = LoadComboBox.LoadAllFWOBrandList();
            ViewBag.ModelList = LoadComboBox.LoadAllFWOModelList();
            ViewBag.CapacityList = LoadComboBox.LoadAllFWOCapacityList();
            var FWOItem = LoadComboBox.LoadFreezerModelDescription(null);
            ViewBag.FWOItem = FWOItem.Where(c => c.Text != "--- Select Item ---").ToList();
            return View();
        }
        public ActionResult SaveUpdateType(FWOType type)
        {
            if (type.ID!=0)
            {
                var item = context.FWOTypes.ToList().FirstOrDefault(x => x.ID == type.ID);
                context.FWOTypes.Remove(item);
                context.FWOTypes.Add(type);
                context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var Lastitem = context.FWOTypes.ToList().LastOrDefault();
                var id = Lastitem.ID + 1;
                type.ID = id;
                context.FWOTypes.Add(type);
                context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            
        }

        public ActionResult SaveUpdateBrand(FWOBrand brand)
        {
            if (brand.ID != 0)
            {
                var item = context.FWOBrands.ToList().FirstOrDefault(x => x.ID == brand.ID);
                context.FWOBrands.Remove(item);
                context.FWOBrands.Add(brand);
                context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var Lastitem = context.FWOBrands.ToList().LastOrDefault();
                var id = Lastitem.ID + 1;
                brand.ID = id;
                context.FWOBrands.Add(brand);
                context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveUpdateModel(FWOModel model)
        {
            if (model.ID != 0)
            {
                var item = context.FWOModels.ToList().FirstOrDefault(x => x.ID == model.ID);
                context.FWOModels.Remove(item);
                context.FWOModels.Add(model);
                context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var Lastitem = context.FWOModels.ToList().LastOrDefault();
                var id = Lastitem.ID + 1;
                model.ID = id;
                context.FWOModels.Add(model);
                context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveUpdateCapacity(FWOCapacity capacity)
        {
            if (capacity.ID != 0)
            {
                var item = context.FWOCapacities.ToList().FirstOrDefault(x => x.ID == capacity.ID);
                context.FWOCapacities.Remove(item);
                context.FWOCapacities.Add(capacity);
                context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var Lastitem = context.FWOCapacities.ToList().LastOrDefault();
                var id = Lastitem.ID + 1;
                capacity.ID = id;
                context.FWOCapacities.Add(capacity);
                context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveItem(FreezerModelDescription item)
        {
            if (item.ID!=0)
            {
                var Lastitem = context.FreezerModelDescription.ToList().FirstOrDefault(x => x.ID == item.ID);
                context.FreezerModelDescription.Remove(Lastitem);
                context.FreezerModelDescription.Add(item);
                context.SaveChanges();
                return Json(2, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var Lastitem = context.FreezerModelDescription.ToList().LastOrDefault();
                var id = Lastitem.ID + 1;
                item.ID = id;
                context.FreezerModelDescription.Add(item);
                context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            
        }
        public ActionResult DeleteItem(int ID)
        {
            var item = context.FreezerModelDescription.ToList().FirstOrDefault(x => x.ID == ID);
            context.FreezerModelDescription.Remove(item);
            context.SaveChanges();
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetItem(int ID)
        {
            var item = context.FreezerModelDescription.ToList().FirstOrDefault(x => x.ID == ID);
            return Json(item, JsonRequestBehavior.AllowGet);
        }
    }
}