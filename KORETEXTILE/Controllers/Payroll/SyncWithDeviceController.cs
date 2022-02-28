using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZkFingerPrint;

namespace BEEERP.Controllers.Payroll
{
    [ShowNotification]
    public class SyncWithDeviceController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: SyncWithDevice
       
        public ActionResult Sync()
        {
            try
            {
                var attandance = AttandanceInfo.GetAttandanceLog(1);
                attandance.ToList().ForEach(x => {
                    var date = Convert.ToDateTime(x.DateTimeRecord).ToString("dd-MM-yyyy HH:mm");
                    var isExist = context.CheckInAttandanceLogs.ToList().FirstOrDefault(y => y.EmployeeID == x.IndRegID.ToString() && y.CheckIn.ToString("dd-MM-yyyy HH:mm") == date);
                    if (isExist == null)
                    {
                        context.CheckInAttandanceLogs.Add(new CheckInAttandanceLog() { EmployeeID = x.IndRegID.ToString(), CheckIn = Convert.ToDateTime(x.DateTimeRecord) });
                    }
                });
                context.SaveChanges();
                ViewBag.Message = "Successfully sync";
            }
            catch
            {
                ViewBag.Message = "There is no device found for Sync";
            }
           
            return View();
        }
        public ActionResult CalculateOvertime()
        {
            try
            {
                context.Database.SqlQuery<string>("exec InsertIntoAttandanceLog").FirstOrDefault();
            }
            catch(Exception ex)
            {

            }
           
            return View();
        }
    }
}