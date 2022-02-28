using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.DataAdmin.SPSettings;

namespace BEEERP.Controllers.DataAdmin.Setting
{
    [ShowNotification]
    public class MachineTypeController : Controller
    {
        BEEContext context = new BEEContext();
        GenerateId generate = new GenerateId();
        // GET: MachineType
        public ActionResult MachineTypeView()
        {
            ViewBag.Machine = context.MachineType.ToList();
            ViewBag.TypeID = generate.GenerateMachTypeId(context);
            return View();
        }
        public ActionResult SaveMachineType(MachineType mt)
        {

            var nameCheck = context.MachineType.FirstOrDefault(x => x.MtName == mt.MtName);
            if (nameCheck == null)
            {
                context.MachineType.Add(mt);
                context.SaveChanges();
                return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
            }
            else
            {

                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetMachineTypeInfo(int id)
        {
            var mtInfo = context.MachineType.FirstOrDefault(x => x.MTID == id);
            if (mtInfo == null)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //var fDesignation = context.FunctionalDesignation.FirstOrDefault(x => x.ID == id);

                return Json(new { info = mtInfo }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateMachineType(MachineType mt)
        {

            var nameCheck = context.MachineType.FirstOrDefault(x => x.MtName == mt.MtName);
            if (nameCheck == null)
            {
                var msInfo = context.MachineType.FirstOrDefault(x => x.MTID == mt.MTID);

                context.MachineType.Remove(msInfo);

                context.MachineType.Add(mt);
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