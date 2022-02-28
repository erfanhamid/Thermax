using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Payroll;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;


namespace BEEERP.Controllers.Payroll
{
    [ShowNotification]
    [AccountingModule]
    public class UploadAttendanceFromExcelController : Controller
    {
        // GET: UploadAttendanceFromExcel
        BEEContext context = new BEEContext();
        public ActionResult UploadAttendance()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveUploadData(List<CheckInAttandanceLog> attendance)
        {
            if (attendance.Count > 0)
            {
                for (int i = 0; i < attendance.Count; i++)
                {
                    CheckInAttandanceLog attendance1 = new CheckInAttandanceLog();
                    attendance1.EmployeeID = attendance[i].EmployeeID;
                    attendance1.CheckIn = Convert.ToDateTime(attendance[i].Date.ToString("dd-MM-yyyy") + " " + attendance[i].CheckIn.ToString("hh: mmtt"));
                    attendance1.IsCalculate = false;
                    context.CheckInAttandanceLogs.Add(attendance1);
                    CheckInAttandanceLog attendance2 = new CheckInAttandanceLog();
                    attendance2.EmployeeID = attendance[i].EmployeeID;
                    attendance2.CheckIn = Convert.ToDateTime(attendance[i].Date.ToString("dd-MM-yyyy") + " " + attendance[i].CheckOut.ToString("hh: mmtt"));
                    attendance2.IsCalculate = false;
                    context.CheckInAttandanceLogs.Add(attendance2);
                }
                int x = context.SaveChanges();
                if (x > 0)
                {
                return Json(new { id = attendance.Count });
                }
            }
            return Json(new { id = 0 });
        }
    }
}