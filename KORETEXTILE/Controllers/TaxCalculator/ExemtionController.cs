using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.TaxCalculator;
using BEEERP.Models.Database;

namespace BEEERP.Controllers.TaxCalculator
{
    [ShowNotification]
    public class ExemtionController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: Exemtion
        public ActionResult Exemtion()  
        {
            ViewBag.Exemtion = context.ExemtionList.ToList();
            ViewBag.ExemtionId = GenerateExemtionId();
            return View();
        }

        public ActionResult SaveExemtion(ExemtionList exemtion)
        {
            var exemtionExits = context.ExemtionList.FirstOrDefault(x => x.Item.ToLower() == exemtion.Item.ToLower().Trim());

            if (exemtionExits == null)
            {
                context.ExemtionList.Add(exemtion);
                context.SaveChanges();
                return Json(exemtion, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ViewBag.Exemtion = context.ExemtionList.ToList();
                ViewBag.ExemtionId = GenerateExemtionId();
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetExemtionById(int id)
        {
            var exemtion = context.ExemtionList.FirstOrDefault(x => x.Id == id);   
            if (exemtion == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(exemtion, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateExemtion(ExemtionList exemtion)
        {
            var exemt = context.ExemtionList.FirstOrDefault(x => x.Id == exemtion.Id);

            if (exemt != null)
            {
                context.ExemtionList.Remove(exemt);
                context.ExemtionList.Add(exemtion);
                context.SaveChanges();
                return Json(exemtion, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0 , JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteExemtionByid(int id)
        {
            var exemt = context.ExemtionList.FirstOrDefault(x => x.Id == id);

            if (exemt != null)
            {
                context.ExemtionList.Remove(exemt);
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
            var exemtion = context.ExemtionList.ToList().OrderBy(x => x.Id).LastOrDefault();
            if (exemtion != null)
            {
                return exemtion.Id + 1;
            }
            else
            {
                return 1;
            }
        }


    }
}