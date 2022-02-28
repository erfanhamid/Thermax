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
    public class HolidayController : Controller
    {
        // GET: Holiday
        BEEContext context = new BEEContext();
        GenerateId generate = new GenerateId();
        // GET: WorkerDesignation
        public ActionResult Holiday()
        {
            ViewBag.HID = generate.GenerateHolidayID(context);
            var holiday = context.Holidays.OrderByDescending(x => x.ID).ToList();

            holiday.ForEach(x =>
            {
                x.Date = x.HolidayDate.ToString("dd-MM-yyyy");
              
            });
            ViewBag.HD = holiday;
            return View();
        }
        public ActionResult GetHDByid(int id)
        {
            var date = DateTime.Today;
            var HD = context.Holidays.ToList().FirstOrDefault(x => x.ID == id  && x.HolidayDate > date);

            if (HD == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { item = HD }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveHD(Holiday HD)
        {
            var hd = context.Holidays.ToList().FirstOrDefault(x => x.HolidayName.ToLower() == HD.HolidayName.ToLower().Trim());

            if (hd == null)
            {
                context.Holidays.Add(HD);
                context.SaveChanges();
                return Json(HD.ID, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateHD(Holiday HD)
        {
            var hds = context.Holidays.ToList().Find(x => x.ID == HD.ID);
            context.Holidays.Remove(hds);

            context.Holidays.Add(HD);
            context.SaveChanges();
            return Json(HD.ID, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteHDByid(int id)
        {
            var hd = context.Holidays.ToList().Find(x => x.ID == id);

            context.Holidays.Remove(hd);
            context.SaveChanges();
            return Json(id, JsonRequestBehavior.AllowGet);
        }
    }
}