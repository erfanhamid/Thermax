using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.Database;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.SalesModule;

namespace BEEERP.Controllers.Data_Admin.Setting
{
    [ShowNotification]
    public class CreateUOMController : Controller
    {
        BEEContext context = new BEEContext();
        GenerateId generate = new GenerateId();
        // GET: CreateUOM
        public ActionResult CreateUOM()
        {
            ViewBag.UomId = generate.GenerateUoMId(context);
            ViewBag.Uom = context.UOM.ToList();
            return View();
        }
        public ActionResult GetUoMByid(int id)
        {
            var uom = context.UOM.ToList().FirstOrDefault(x => x.Id == id);

            if (uom == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { item = uom }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveUoM(UOM uom)
        {
            var um = context.UOM.ToList().FirstOrDefault(x => x.Name.ToLower() == uom.Name.ToLower().Trim());

            if (um == null)
            {
                context.UOM.Add(uom);
                context.SaveChanges();
                return Json(uom.Id, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateUoM(UOM uom)
        {
            var um = context.UOM.ToList().Find(x => x.Id == uom.Id);
            context.UOM.Remove(um);

            context.UOM.Add(uom);
            context.SaveChanges();
            return Json(uom.Id, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteUoMByid(int id)
        {
            var um = context.UOM.ToList().Find(x => x.Id == id);

            context.UOM.Remove(um);
            context.SaveChanges();
            return Json(id, JsonRequestBehavior.AllowGet);
        }
    }
}