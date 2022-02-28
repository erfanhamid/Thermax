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
    public class MachineSectionController : Controller
    {
        BEEContext context = new BEEContext();
        GenerateId generate = new GenerateId();
        // GET: MachineSection
        public ActionResult MachineSectionView()
        {



            ViewBag.Section = context.MachineSection.ToList();
            ViewBag.MSSectionID = generate.GenerateMachineSectionId(context);
            return View();
        }
        public ActionResult SaveMachineSection(MachineSection ms)
        {

            var nameCheck = context.MachineSection.FirstOrDefault(x => x.MSName == ms.MSName);
            if (nameCheck == null)
            {
                context.MachineSection.Add(ms);
                context.SaveChanges();
                return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
            }
            else
            {

                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetMachineSection(int id)
        {
            var msInfo = context.MachineSection.FirstOrDefault(x => x.MSID == id);
            if (msInfo == null)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //var fDesignation = context.FunctionalDesignation.FirstOrDefault(x => x.ID == id);

                return Json(new { info = msInfo }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateMachineSection(MachineSection ms)
        {

            var nameCheck = context.MachineSection.FirstOrDefault(x => x.MSName == ms.MSName);
            if (nameCheck == null)
            {
                var msInfo = context.MachineSection.FirstOrDefault(x => x.MSID == ms.MSID);

                context.MachineSection.Remove(msInfo);

                context.MachineSection.Add(ms);
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