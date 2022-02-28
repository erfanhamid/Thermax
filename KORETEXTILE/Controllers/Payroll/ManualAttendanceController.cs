using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Payroll;
using BEEERP.Models.Database;
using BEEERP.Models.Log;

namespace BEEERP.Controllers.Payroll
{
    [ShowNotification]
    public class ManualAttendanceController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: ManualAttendance
        public ActionResult ManualAttendance()
        {
            ViewBag.Employee = LoadComboBox.LoadAllEmployee();
            return View();
        }

        public ActionResult SaveAttendence(List<ManualAttendance> item)
        {
            if(item.Count() > 0)
            {
                item.ForEach(x =>
                {
                    DateTime CheckIn = Convert.ToDateTime(x.CheckIn);
                    DateTime s = new DateTime(CheckIn.Year, CheckIn.Month, CheckIn.Day, 0, 0, 0);
                    DateTime e = CheckIn.AddDays(1);
                    var attendanceLog = context.CheckInAttandanceLogs.Where(y => y.EmployeeID == x.EmployeeID.ToString() && y.CheckIn >= s && y.CheckIn <= e).ToList();
                    if (attendanceLog != null)
                    {
                        CheckInAttandanceLog atLog = new CheckInAttandanceLog();
                        atLog.CheckIn = CheckIn;
                        atLog.EmployeeID = x.EmployeeID.ToString();
                        atLog.IsCalculate = false;
                        context.CheckInAttandanceLogs.Add(atLog);

                        attendanceLog.ForEach(a =>
                        {
                            a.IsCalculate = false;
                            context.Entry<CheckInAttandanceLog>(a).State = System.Data.Entity.EntityState.Modified;
                        });

                        var atLogsExist = context.AttandanceLogs.FirstOrDefault(b => b.EmployeeId.ToString() == x.EmployeeID.ToString() && b.CheckIn >= s && b.CheckIn <= e);
                        if (atLogsExist != null)
                        {
                            context.AttandanceLogs.Remove(atLogsExist);
                        }

                        UserLog.SaveUserLog(ref context, new UserLog(MaximumCheckInAttendanceLogId().ToString(), DateTime.Now, "ManualAttendance", ScreenAction.Save));
                    }



                });
                context.SaveChanges();
                return Json(MaximumCheckInAttendanceLogId().ToString(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }

            //DateTime s = new DateTime(employee.CheckIn.Year, employee.CheckIn.Month, employee.CheckIn.Day, 0, 0, 0);
            //DateTime e = employee.CheckIn.AddDays(1);
            //e = new DateTime(e.Year, e.Month, e.Day, 0, 0, 0);
            //var attendanceLog = context.CheckInAttandanceLogs.Where(x => x.EmployeeID == employee.EmployeeID && x.CheckIn >= s && x.CheckIn <= e).ToList();

            //if (attendanceLog != null)
            //{
            //    CheckInAttandanceLog atLog = new CheckInAttandanceLog();
            //    atLog.CheckIn = employee.CheckIn;
            //    atLog.EmployeeID = employee.EmployeeID;
            //    atLog.IsCalculate = false;
            //    context.CheckInAttandanceLogs.Add(atLog);

            //    attendanceLog.ForEach(x =>
            //    {
            //        x.IsCalculate = false;
            //        context.Entry<CheckInAttandanceLog>(x).State = System.Data.Entity.EntityState.Modified;
            //    });

            //    var atLogsExist = context.AttandanceLogs.FirstOrDefault(x => x.EmployeeId.ToString() == employee.EmployeeID && x.CheckIn >= s && x.CheckIn <= e);
            //    if(atLogsExist != null)
            //    {
            //        context.AttandanceLogs.Remove(atLogsExist);
            //    }

            //    UserLog.SaveUserLog(ref context, new UserLog(MaximumCheckInAttendanceLogId().ToString(), DateTime.Now, "ManualAttendance", ScreenAction.Save));
            //    context.SaveChanges();

            //    return Json(item, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{

       

    public int MaximumCheckInAttendanceLogId()
    {
        int atLog = context.CheckInAttandanceLogs.Select(x => x.ID).Max();
        if (atLog == 0)
        {
            return 1;
        }
        else
        {
            return atLog + 1;
        }
    }


    }

    public class ManualAttendance
    {
        public int EmployeeID { get; set; }
        public string CheckIn { get; set; }
    }
}