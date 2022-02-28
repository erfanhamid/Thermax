using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZkFingerPrint;

namespace BEEERP.Controllers.Payroll
{
    [ShowNotification]
    public class EmployeeAttendanceController : Controller
    {
        // GET: EmployeeAttendance
        public ActionResult Index()
        {
            try
            {
                if (AttandanceInfo.IsDeviceConnected == true)
                {
                    ViewBag.Status = true;
                }
                else
                {
                    ViewBag.Status = false;
                }
                return View();
            }
            catch
            {
                ViewBag.Status = false;
                return View();
            }
            
           
        }
        [HttpPost]
        public ActionResult Index(string isConnect)
        {
            try
            {
                bool connect = bool.Parse(isConnect);

                if (connect == true)
                {
                    if (AttandanceInfo.IsDeviceConnected == true)
                    {

                    }
                    else
                    {
                        AttandanceInfo.ConnectToDevice();
                    }
                }
                else
                {
                    AttandanceInfo.DisconnectFromDevice();

                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
           
        }

        public ActionResult ImportTodayAttLog()
        {
            var data = AttandanceInfo.GetAttandanceLog(1);
            return Json("1",JsonRequestBehavior.AllowGet);
        }
    }
}