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
    public class WorkerTypeController : Controller
    {
        BEEContext context = new BEEContext();
        GenerateId generate = new GenerateId();
        // GET: WorkerType
        public ActionResult WorkerType()
        {
            ViewBag.WTID = generate.GetWTID(context);
            ViewBag.WT = context.WorkerType.ToList();
            return View();
        }

        public ActionResult GetWTById(int id)
        {
            var WT = context.WorkerType.ToList().FirstOrDefault(x => x.WTNo == id);

            if (WT == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { item = WT }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveWT(WorkerType WT)
        {
            var dpt = context.WorkerType.ToList().FirstOrDefault(x => x.WorksType.ToLower() == WT.WorksType.ToLower().Trim());

            if (dpt == null)
            {
                context.WorkerType.Add(WT);
                context.SaveChanges();
                return Json(WT.WTNo, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateWT(WorkerType WT)
        {
            var dpt = context.WorkerType.ToList().Find(x => x.WTNo == WT.WTNo);
            context.WorkerType.Remove(dpt);

            context.WorkerType.Add(WT);
            context.SaveChanges();
            return Json(WT.WTNo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteWTByid(int id)
        {
            var dpt = context.WorkerType.ToList().Find(x => x.WTNo == id);

            context.WorkerType.Remove(dpt);
            context.SaveChanges();
            return Json(id, JsonRequestBehavior.AllowGet);
        }
    }
}