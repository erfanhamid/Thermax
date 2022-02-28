using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.TaxCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.TaxCalculator
{
    [ShowNotification]
    public class ChallanPercentageListController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: ChallanPercentageList
        public ActionResult ChallanPercentageList() 
        {
            ViewBag.ChallanPercntg = context.ChallanPercentageList.ToList();
            ViewBag.ChallanPercntgId = GenerateChallanPercntgId();
            return View();
        }

        public ActionResult SaveChallan(ChallanPercentageList challan)       
        {
            var challanExits = context.ChallanPercentageList.FirstOrDefault(x => x.Percentage == challan.Percentage);

            if (challanExits == null)
            {
                context.ChallanPercentageList.Add(challan);
                context.SaveChanges();
                return Json(challan, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ViewBag.ChallanPercntg = context.ChallanPercentageList.ToList();
                ViewBag.ChallanPercntgId = GenerateChallanPercntgId();
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetChallanById(int id)  
        {
            var challan = context.ChallanPercentageList.FirstOrDefault(x => x.ID == id);
            if (challan == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(challan, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateChallan(ChallanPercentageList challan)    
        {
            var challanE = context.ChallanPercentageList.FirstOrDefault(x => x.ID == challan.ID);

            if (challanE != null)
            {
                context.ChallanPercentageList.Remove(challanE);
                context.ChallanPercentageList.Add(challan);
                context.SaveChanges();
                return Json(challan, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteChallanByid(int id)
        {
            var challan = context.ChallanPercentageList.FirstOrDefault(x => x.ID == id);

            if (challan != null)
            {
                context.ChallanPercentageList.Remove(challan);
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }

        public int GenerateChallanPercntgId()
        {
            var challan = context.ChallanPercentageList.ToList().OrderBy(x => x.ID).LastOrDefault();
            if (challan != null)
            {
                return challan.ID + 1;
            }
            else
            {
                return 1;
            }
        }
    }
}