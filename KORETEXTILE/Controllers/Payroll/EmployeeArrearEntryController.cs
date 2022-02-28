using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.Database;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Payroll;
using BEEERP.Models.ViewModel.Payroll;

namespace BEEERP.Controllers.Payroll
{
    [ShowNotification]
    public class EmployeeArrearEntryController : Controller
    {
        BEEContext context = new BEEContext();
        public ActionResult EmployeeArrearEntry()
        {
            string sql = string.Format("exec GetEmployeeDetailsforArrearEntry");
            ViewBag.list = context.Database.SqlQuery<EmployeeArrearEntryVModel>(sql).ToList();
            return View();
        }
        public ActionResult GetEmployeeArrearEntry(int EmployeeID)
        {
            if (EmployeeID == 0)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                EmployeeArrearEntry employeeArrear = new EmployeeArrearEntry();
                employeeArrear = context.EmployeeArrearEntry.FirstOrDefault(m => m.EmployeeID == EmployeeID);
                if (employeeArrear != null)
                {
                    employeeArrear.Name = context.Employees.FirstOrDefault(x => x.Id == EmployeeID).Name;
                    employeeArrear.ARDate = Convert.ToDateTime(employeeArrear.ARDate);
                    return Json(new { employeeArrears = employeeArrear,message=1 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    EmployeeArrearEntry ArrearEntry = new EmployeeArrearEntry();
                    ArrearEntry.EmployeeID = EmployeeID;
                    ArrearEntry.Name = context.Employees.FirstOrDefault(x => x.Id == EmployeeID).Name;
                    ArrearEntry.ARAmount = 0;
                    ArrearEntry.ARRefNO = null;
                    ArrearEntry.ARDescription = null;
                    return Json(new { employeeArrears = ArrearEntry,message=0 }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult SaveEmployeeArrearEntry(EmployeeArrearEntry ArrearEntry)
        {
            var isexists = context.EmployeeArrearEntry.FirstOrDefault(x => x.EmployeeID == ArrearEntry.EmployeeID);
            if (isexists != null)
            {
                context.EmployeeArrearEntry.Remove(isexists);
                context.EmployeeArrearEntry.Add(ArrearEntry);
                context.SaveChanges();
                return Json(new { id = ArrearEntry.EmployeeID }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                context.EmployeeArrearEntry.Add(ArrearEntry);
                context.SaveChanges();
                return Json(new { id = ArrearEntry.EmployeeID }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}