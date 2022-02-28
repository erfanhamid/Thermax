using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.SpareParts;
using BEEERP.Models.DataAdmin.SPSettings;

namespace BEEERP.Controllers.DataAdmin.Setting
{
    [ShowNotification]
    public class SparePartsDepartmentController : Controller
    {
        BEEContext context = new BEEContext();
        GenerateId generate = new GenerateId();
        // GET: SparePartsDepartment
        public ActionResult SparePartsDepartmentView()
        {
            ViewBag.Department = context.SPDepartment.ToList();
            ViewBag.DepartmentId = generate.GenerateSPDepartmentId(context);
            return View();
        }
        public ActionResult SaveSPDepartment(SPDepartment spd)
        {

            var nameCheck = context.SPDepartment.FirstOrDefault(x => x.SPDName == spd.SPDName);
            if (nameCheck == null)
            {
                context.SPDepartment.Add(spd);
                context.SaveChanges();
                return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetSPDepartment(int id)
        {
            var spdInfo = context.SPDepartment.FirstOrDefault(x => x.SPDID == id);
            if (spdInfo == null)
            {
                return Json(new { Message = 0}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //var fDesignation = context.FunctionalDesignation.FirstOrDefault(x => x.ID == id);

                return Json(new { info = spdInfo }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateSPDepartment(SPDepartment spd)
        {

            var nameCheck = context.SPDepartment.FirstOrDefault(x => x.SPDID == spd.SPDID);
            if (nameCheck.SPDName != null)
            {
                var spdInfo = context.SPDepartment.FirstOrDefault(x => x.SPDID == spd.SPDID);

                context.SPDepartment.Remove(spdInfo);

                context.SPDepartment.Add(spd);
                context.SaveChanges();
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }



               
        }
    }
}