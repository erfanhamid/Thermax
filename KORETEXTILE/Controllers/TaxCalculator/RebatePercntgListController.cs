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
    public class RebatePercntgListController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: RebatePercntgList
        public ActionResult RebatePercntgList() 
        {
            ViewBag.RebatePercntg = context.RebatePercntgList.ToList();  
            ViewBag.RebatePercntgId = GenerateRebatePercntgId();    
            return View();
        }

        public ActionResult SaveRebate(RebatePercntgList rebate)    
        {
            var rebateExits = context.RebatePercntgList.FirstOrDefault(x => x.Item.ToLower() == rebate.Item.ToLower().Trim());

            if (rebateExits == null)
            {
                context.RebatePercntgList.Add(rebate);
                context.SaveChanges();
                return Json(rebate, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ViewBag.RebatePercntg = context.RebatePercntgList.ToList();
                ViewBag.RebatePercntgId = GenerateRebatePercntgId();
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetRebateById(int id)   
        {
            var rebate = context.RebatePercntgList.FirstOrDefault(x => x.Id == id);  
            if (rebate == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(rebate, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateRebate(RebatePercntgList rebate)  
        {
            var rebateE = context.RebatePercntgList.FirstOrDefault(x => x.Id == rebate.Id);    

            if (rebateE != null)
            {
                context.RebatePercntgList.Remove(rebateE);
                context.RebatePercntgList.Add(rebate);
                context.SaveChanges();
                return Json(rebate, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteRebateByid(int id)    
        {
            var rebate = context.RebatePercntgList.FirstOrDefault(x => x.Id == id);  

            if (rebate != null)
            {
                context.RebatePercntgList.Remove(rebate);
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }

        public int GenerateRebatePercntgId()    
        {
            var rebate = context.RebatePercntgList.ToList().OrderBy(x => x.Id).LastOrDefault();
            if (rebate != null)
            {
                return rebate.Id + 1;
            }
            else
            {
                return 1;
            }
        }
    }
}