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
    public class RebateConditionController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: RebateCondition
        public ActionResult RebateCondition()   
        {
            ViewBag.RebateCondition = context.RebateCondition.ToList();
            ViewBag.ConditionId = GenerateConditionId();    
            return View();
        }

        public ActionResult SaveCondition(RebateCondition condition) 
        {
            var conditionExits = context.RebateCondition.FirstOrDefault(x => x.Name.ToLower() == condition.Name.ToLower().Trim());

            if (conditionExits == null)
            {
                context.RebateCondition.Add(condition);
                context.SaveChanges();
                return Json(condition, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ViewBag.RebateCondition = context.RebateCondition.ToList();
                ViewBag.ConditionId = GenerateConditionId();
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetConditionById(int id)    
        {
            var condition = context.RebateCondition.FirstOrDefault(x => x.ID == id);
            if (condition == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(condition, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateCondition(RebateCondition condition)  
        {
            var conExist = context.RebateCondition.FirstOrDefault(x => x.ID == condition.ID);

            if (conExist != null)
            {
                context.RebateCondition.Remove(conExist);
                context.RebateCondition.Add(condition);
                context.SaveChanges();
                return Json(condition, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteConditionByid(int id) 
        {
            var conExist = context.RebateCondition.FirstOrDefault(x => x.ID == id);

            if (conExist != null)
            {
                context.RebateCondition.Remove(conExist);
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }

        public int GenerateConditionId()
        {
            var exemtion = context.RebateCondition.Where(x => x.ID != 99).ToList().OrderBy(x => x.ID).LastOrDefault();
            if (exemtion != null)
            {
                return exemtion.ID + 1;
            }
            else
            {
                return 1;
            }
        }
    }
}