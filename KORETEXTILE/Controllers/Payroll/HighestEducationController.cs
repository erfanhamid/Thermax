using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Payroll;

namespace BEEERP.Controllers.Payroll
{
    [ShowNotification]
    //[Authorize(Roles = "HraAdmin,HraOperator,HraViewer,HraApprover,SysAdmin,SysViewer,SysOperator,SysApprover,DataAdmin,DataOperator,DataViewer,DataApprover")]
    public class HighestEducationController : Controller
    {
        BEEContext context = new BEEContext();
        GenerateId generate = new GenerateId();
        // GET: HighestEducation
        public ActionResult HighestEducation()  
        {
            ViewBag.HighEdu = context.HighestEducation.ToList();
            ViewBag.Id = generate.GeneratedHightEduId(context);
            return View();
        }

        [HttpPost]
        public ActionResult SaveUpdateHighestEdu(HighestEducation highedu)
        {
            

            if (highedu.ID == 0 || highedu == null)
            {
                return RedirectToAction("FunctionalDesignation");
            }
            else
            {
                var prevZone = context.HighestEducation.AsNoTracking().FirstOrDefault(m => m.ID == highedu.ID);
                if (prevZone == null)
                {
                    context.HighestEducation.Add(highedu);
                    context.SaveChanges();
                    return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    context.Entry<HighestEducation>(highedu).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult GetEduById(int id)
        {
            var highestEdu = context.HighestEducation.FirstOrDefault(x => x.ID == id);
            if (highestEdu == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var highestEduForShow = context.HighestEducation.FirstOrDefault(x => x.ID == id);

                return Json(new { highestEduForShow = highestEduForShow }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteEduByid(int id)
        {
            var str = context.HighestEducation.ToList().Find(x => x.ID == id);

            context.HighestEducation.Remove(str);
            context.SaveChanges();
            return Json(id, JsonRequestBehavior.AllowGet);
        }

    }
}