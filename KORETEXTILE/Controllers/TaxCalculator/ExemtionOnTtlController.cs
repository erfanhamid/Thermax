using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.Database;
using BEEERP.Models.TaxCalculator;

namespace BEEERP.Controllers.TaxCalculator
{
    [ShowNotification]
    public class ExemtionOnTtlController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: ExemtionOnTtl
        public ActionResult ExemtionOnTtl() 
        {
            ViewBag.Gender = LoadGender();
            ViewBag.Exemtion = context.ExemtionOnTtl.ToList();
            ViewBag.ExemtionId = GenerateExemtionId();
            return View();
        }

        public ActionResult SaveExemtion(ExemtionOnTtl exemtion)
        {
            var exemtionExits = context.ExemtionOnTtl.FirstOrDefault(x => x.Gender == exemtion.Gender);

            if (exemtionExits == null)
            {
                context.ExemtionOnTtl.Add(exemtion);
                context.SaveChanges();
                return Json(exemtion, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ViewBag.Exemtion = context.ExemtionOnTtl.ToList();
                ViewBag.ExemtionId = GenerateExemtionId();
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetExemtionById(int id)
        {
            var exemtion = context.ExemtionOnTtl.FirstOrDefault(x => x.Id == id);
            if (exemtion == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(exemtion, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateExemtion(ExemtionOnTtl exemtion)
        {
            var exemt = context.ExemtionOnTtl.FirstOrDefault(x => x.Id == exemtion.Id);

            if (exemt != null)
            {
                context.ExemtionOnTtl.Remove(exemt);
                context.ExemtionOnTtl.Add(exemtion);
                context.SaveChanges();
                return Json(exemtion, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteExemtionByid(int id)
        {
            var exemt = context.ExemtionOnTtl.FirstOrDefault(x => x.Id == id);

            if (exemt != null)
            {
                context.ExemtionOnTtl.Remove(exemt);
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }

        public int GenerateExemtionId()
        {
            var exemtion = context.ExemtionOnTtl.ToList().OrderBy(x => x.Id).LastOrDefault();
            if (exemtion != null)
            {
                return exemtion.Id + 1;
            }
            else
            {
                return 1;
            }
        }

        public SelectList LoadGender()   
        {
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Gender ---");
            items.Add("Male", "Male");
            items.Add("Female", "Female");
            items.Add("Disabled", "Disabled");
            return new SelectList(items, "Key", "Value");
        }
    }
}