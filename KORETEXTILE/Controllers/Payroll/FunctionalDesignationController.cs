using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.Payroll;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.HRModule;

namespace BEEERP.Controllers.Payroll
{
    [ShowNotification]
    //[Authorize(Roles = "HraAdmin,HraOperator,HraViewer,HraApprover,SysAdmin,SysViewer,SysOperator,SysApprover,DataAdmin,DataOperator,DataViewer,DataApprover")]
    public class FunctionalDesignationController : Controller
    {

        BEEContext context = new BEEContext();
        GenerateId generate = new GenerateId();
        // GET: FunctionalDesignation
        public ActionResult FunctionalDesignation() 
        {
            ViewBag.FuncDesignation = context.FunctionalDesignation.ToList();
            ViewBag.ID = generate.GenerateFunctionalDesignationId(context);
            return View();
        }
        public ActionResult SaveUpdateFunctionalDesignation(FunctionalDesignation deg)
        {
            if (deg.ID == 0 || deg == null)
            {
                return RedirectToAction("FunctionalDesignation");
            }
            else
            {
                var prevZone = context.FunctionalDesignation.AsNoTracking().FirstOrDefault(m => m.ID == deg.ID);
                if (prevZone == null)
                {
                    context.FunctionalDesignation.Add(deg);
                    context.SaveChanges();
                    return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    context.Entry<FunctionalDesignation>(deg).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult GetFunctionalDesignation(int id)
        {
            var store = context.FunctionalDesignation.FirstOrDefault(x => x.ID == id);
            if (store == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var fDesignation = context.FunctionalDesignation.FirstOrDefault(x => x.ID == id);

                return Json(new { fDesignation = fDesignation }, JsonRequestBehavior.AllowGet);
            }
        }

        //public ActionResult UpdateDesignation(BEEERP.Models.SalesModule.Store store)
        //{
        //    var str = context.Store.ToList().Find(x => x.Id == store.Id);

        //    context.Store.Remove(str);

        //    context.Store.Add(store);
        //    context.SaveChanges();
        //    return Json(store.Id, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult DeleteFDesignationByid(int id)
        {
            var str = context.FunctionalDesignation.ToList().Find(x => x.ID == id);

            context.FunctionalDesignation.Remove(str);
            context.SaveChanges();
            return Json(id, JsonRequestBehavior.AllowGet);
        }

    }
}