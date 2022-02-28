using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.TaxCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;

namespace BEEERP.Controllers.TaxCalculator
{
    [ShowNotification]
    public class RebateConditionLineController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: RebateConditionLine
        public ActionResult RebateConditionLine()
        {
            var conLine = context.RebateConditionLine.ToList();
            conLine.ForEach(x => {
                 var condition = context.RebateCondition.FirstOrDefault(y => y.ID == x.ConditionID);
                x.ConditionName = condition.Name;
             });
            ViewBag.RebateConditionLine = conLine;
            ViewBag.SlabId = GenerateSlabId(0);
            ViewBag.Condition = LoadComboBox.LoadRebateCondition();
            return View();
        }

        public ActionResult GetSlabIdByCondition(int id)
        {
            return Json(GenerateSlabId(id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveConditionLine(RebateConditionLine conditionLine)
        {
            //var conditionLineExits = context.RebateConditionLine.FirstOrDefault(x => x.Slab.ToLower() == conditionLine.Slab.ToLower().Trim() && x.ConditionID == conditionLine.ConditionID);

            if (conditionLine != null)
            {
                context.RebateConditionLine.Add(conditionLine);
                context.SaveChanges();
                return Json(conditionLine, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ViewBag.RebateConditionLine = context.RebateConditionLine.ToList();
                ViewBag.SlabId = GenerateSlabId(null);
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetConditionLineById(string id)    
        {
            int conId = Convert.ToInt32(id.Substring(0, 1));
            int slabId = Convert.ToInt32(id.Substring(1, 1));
            var conditionLine = context.RebateConditionLine.FirstOrDefault(x => x.ConditionID == conId && x.SlabID == slabId);
            if (conditionLine == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(conditionLine, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateConditionLine(RebateConditionLine conditionLine)  
        {
            var conExist = context.RebateConditionLine.FirstOrDefault(x => x.ConditionID == conditionLine.ConditionID && x.SlabID == conditionLine.SlabID);

            if (conExist != null)
            {
                context.RebateConditionLine.Remove(conExist);
                context.RebateConditionLine.Add(conditionLine);
                context.SaveChanges();
                return Json(conditionLine, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteConditionLineByid(int conId, int slabId) 
        {
            var conExist = context.RebateConditionLine.FirstOrDefault(x => x.ConditionID == conId && x.SlabID == slabId);

            if (conExist != null)
            {
                context.RebateConditionLine.Remove(conExist);
                context.SaveChanges();
                return Json(conId, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }

        public int GenerateSlabId(int? condition) 
        {
            if(condition != 0)
            {
                var slabId = context.RebateConditionLine.Where(x => x.ConditionID == condition).ToList().OrderBy(x => x.SlabID).LastOrDefault();
                if (slabId != null)
                {
                    return slabId.SlabID + 1;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 0;
            }
        }
    }
}