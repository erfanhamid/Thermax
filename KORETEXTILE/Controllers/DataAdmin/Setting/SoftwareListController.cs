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
    public class SoftwareListController : Controller
    {
        BEEContext context = new BEEContext();
        GenerateId generate = new GenerateId();
        // GET: SoftwareList
        public ActionResult SoftwareListView()
        {


            ViewBag.Software = context.SoftwareList.ToList();
            ViewBag.SoftwareID = generate.GeneratSoftwareId(context);


            return View();
        }
        public ActionResult SaveSoftwareList(SoftwareList sw)
        {

            var nameCheck = context.SoftwareList.FirstOrDefault(x => x.SoftwareName == sw.SoftwareName);
            if (nameCheck == null)
            {
                context.SoftwareList.Add(sw);
                context.SaveChanges();
                return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
            }
            else
            {

                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetSoftwareINfo(int id)
        {
            var swInfo = context.SoftwareList.FirstOrDefault(x => x.SoftwareID == id);
            if (swInfo == null)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //var fDesignation = context.FunctionalDesignation.FirstOrDefault(x => x.ID == id);

                return Json(new { info = swInfo }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateSoftwareList(SoftwareList sw)
        {

            var nameCheck = context.SoftwareList.FirstOrDefault(x => x.SoftwareName == sw.SoftwareName);
            if (nameCheck == null)
            {
                var msInfo = context.SoftwareList.FirstOrDefault(x => x.SoftwareID == sw.SoftwareID);

                context.SoftwareList.Remove(msInfo);

                context.SoftwareList.Add(sw);
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