using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Payroll
{
    [ShowNotification]
    //[Authorize(Roles = "HraAdmin,HraOperator,HraViewer,HraApprover,SysAdmin,SysViewer,SysOperator,SysApprover,DataAdmin,DataOperator,DataViewer,DataApprover")]
    public class WorkPlaceController : Controller
    {
        BEEContext context = new BEEContext();
        GenerateId generate = new GenerateId();
        // GET: WorkPlace
        public ActionResult WorkPlace()
        {
            ViewBag.WPID = generate.GetWPID(context);
            ViewBag.WP = context.WorkPlace.ToList();
            return View();
        }
        public ActionResult GetWPById(int id)
        {
            var wop = context.WorkPlace.ToList().FirstOrDefault(x => x.WorkPlaceNo == id);

            if (wop == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { item = wop }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveWP(WorkPlace WP)
        {
            var wop = context.WorkPlace.ToList().FirstOrDefault(x => x.WorksPlace.ToLower() == WP.WorksPlace.ToLower().Trim());

            if (wop == null)
            {
                context.WorkPlace.Add(WP);
                context.SaveChanges();
                return Json(WP.WorkPlaceNo, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateWP(WorkPlace WP)
        {
            var wop = context.WorkPlace.ToList().Find(x => x.WorkPlaceNo == WP.WorkPlaceNo);
            context.WorkPlace.Remove(wop);

            context.WorkPlace.Add(WP);
            context.SaveChanges();
            return Json(WP.WorkPlaceNo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteWPByid(int id)
        {
            var wop = context.WorkPlace.ToList().Find(x => x.WorkPlaceNo == id);

            context.WorkPlace.Remove(wop);
            context.SaveChanges();
            return Json(id, JsonRequestBehavior.AllowGet);
        }
    }
}