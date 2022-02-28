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
    public class WorkerDesignationController : Controller
    {
        BEEContext context = new BEEContext();
        GenerateId generate = new GenerateId();
        // GET: WorkerDesignation
        public ActionResult WorkerDesignation()
        {
            ViewBag.WDID = generate.GenerateWDID(context);
            ViewBag.WD = context.WorkerDesignation.ToList();
            return View();
        }
        public ActionResult GetWDByid(int id)
        {
            var WD = context.WorkerDesignation.ToList().FirstOrDefault(x => x.WDesignationNo == id);

            if (WD == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { item = WD }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveWD(WorkerDesignation WD)
        {
            var dpt = context.WorkerDesignation.ToList().FirstOrDefault(x => x.WorkersDesignation.ToLower() == WD.WorkersDesignation.ToLower().Trim());

            if (dpt == null)
            {
                context.WorkerDesignation.Add(WD);
                context.SaveChanges();
                return Json(WD.WDesignationNo, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateWD(WorkerDesignation WD)
        {
            var dpt = context.WorkerDesignation.ToList().Find(x => x.WDesignationNo == WD.WDesignationNo);
            context.WorkerDesignation.Remove(dpt);

            context.WorkerDesignation.Add(WD);
            context.SaveChanges();
            return Json(WD.WDesignationNo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteWDByid(int id)
        {
            var wd = context.WorkerDesignation.ToList().Find(x => x.WDesignationNo == id);

            context.WorkerDesignation.Remove(wd);
            context.SaveChanges();
            return Json(id, JsonRequestBehavior.AllowGet);
        }
    }
}