using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.TaxCalculator;

namespace BEEERP.Controllers.TaxCalculator
{
    [ShowNotification]
    public class SLabListController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: SLabList
        public ActionResult SLabList()  
        {
            ViewBag.Slab = context.SLabList.ToList();
            ViewBag.SlabId  = GenerateExemtionId();
            return View();
        }

        public ActionResult SaveSlab(SLabList slab)
        {
            var slabExits = context.SLabList.FirstOrDefault(x => x.Slab.ToLower() == slab.Slab.ToLower().Trim());   

            if (slabExits == null)
            {
                context.SLabList.Add(slab);
                context.SaveChanges();
                return Json(slab, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ViewBag.Slab = context.SLabList.ToList();
                ViewBag.SlabId = GenerateExemtionId();
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetSlabById(int id) 
        {
            var slab = context.SLabList.FirstOrDefault(x => x.Id == id);    
            if (slab == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(slab, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateSlab(SLabList sLab)   
        {
            var slabE = context.SLabList.FirstOrDefault(x => x.Id == sLab.Id);  

            if (slabE != null)  
            {
                context.SLabList.Remove(slabE);
                context.SLabList.Add(sLab);
                context.SaveChanges();
                return Json(sLab, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteSlabByid(int id)  
        {
            var slabE = context.SLabList.FirstOrDefault(x => x.Id == id);

            if (slabE != null)
            {
                context.SLabList.Remove(slabE);
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
            var exemtion = context.SLabList.ToList().OrderBy(x => x.Id).LastOrDefault();
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