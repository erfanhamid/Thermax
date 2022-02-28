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
    public class WorkerSectionController : Controller
    {
        BEEContext context = new BEEContext();
        GenerateId generate = new GenerateId();
        // GET: WorkerSection
        public ActionResult WorkerSection()
        {
            ViewBag.ProID = generate.GenerateWorkSectionId(context);
            ViewBag.info = context.WorkSection.ToList();
            return View();
        }
        public ActionResult GetWorkerSectionByid(int id)
        {
            var WorkSec = context.WorkSection.ToList().FirstOrDefault(x => x.ProdSectID == id);

            if (WorkSec == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { item = WorkSec }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveWorkSec(WorkerSection WS)
        {
            var dpt = context.WorkSection.ToList().FirstOrDefault(x => x.ProdSectID == WS.ProdSectID);

            if (dpt == null)
            {
                context.WorkSection.Add(WS);
                context.SaveChanges();
                return Json(WS.ProdSectID, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateDepot(WorkerSection WS)
        {
            var dpt = context.WorkSection.ToList().Find(x => x.ProdSectID == WS.ProdSectID);
            context.WorkSection.Remove(dpt);

            context.WorkSection.Add(WS);
            context.SaveChanges();
            return Json(WS.ProdSectID, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteWSByid(int id)
        {
            var dpt = context.WorkSection.ToList().Find(x => x.ProdSectID == id);

            context.WorkSection.Remove(dpt);
            context.SaveChanges();
            return Json(id, JsonRequestBehavior.AllowGet);
        }
        }
}