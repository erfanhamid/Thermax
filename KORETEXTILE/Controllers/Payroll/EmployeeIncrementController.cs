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
    public class EmployeeIncrementController : Controller
    {
        // GET: EmployeeIncrement
        BEEContext context = new BEEContext();
        public ActionResult EmployeeIncrement()
        {
            ViewBag.Employee = LoadComboBox.LoadAllEmployee();
            return View();
        }
        public ActionResult GetEmployeeDetails(int empId)
        {
            var employee = context.Employees.FirstOrDefault(x => x.Id == empId);

            if (employee != null)
            {
                var desigation = context.Designation.FirstOrDefault(x => x.Id == employee.Designation).Name;
                var isExist = context.EmployeeIncrement.FirstOrDefault(x => x.EmployeeID == empId);
                if (isExist != null)
                {
                    return Json(new { message = 1, isExist, desigation }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    return Json(new { message = 0, desigation}, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { message = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveEmployeeIncrement(EmployeeIncrement Increment)
        {
            using (context)
            {
                string incrementID = "";
                string yearLastTwoPart = Increment.EffectiveDate.Year.ToString().Substring(2, 2).ToString();
                var noOfIncrement = context.EmployeeIncrement.Count();

                if (noOfIncrement > 0)
                {
                    EmployeeIncrement lastIncrement = context.EmployeeIncrement.ToList().LastOrDefault(x => x.IncrementID.ToString().Substring(0, 2) == yearLastTwoPart);
                    if (lastIncrement == null)
                    {
                        incrementID = yearLastTwoPart + "00001";
                    }
                    else
                    {
                        incrementID = yearLastTwoPart + (Convert.ToInt32(lastIncrement.IncrementID.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                    }
                }
                else
                {
                    incrementID = yearLastTwoPart + "00001";
                }
                Increment.IncrementID = Convert.ToInt32(incrementID);
                var isexists = context.EmployeeIncrement.FirstOrDefault(x => x.EmployeeID == Increment.EmployeeID);
                if (Increment != null)
                {
                    context.EmployeeIncrement.Add(Increment);
                    context.SaveChanges();
                    return Json(new { id = Increment.EmployeeID, tid = Increment.IncrementID }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    context.EmployeeIncrement.Add(Increment);
                    context.SaveChanges();
                    return Json(new { id = Increment.EmployeeID, tid = Increment.IncrementID }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult UpdateEmployeeIncrement(EmployeeIncrement Increment)
        {
            var isExist = context.EmployeeIncrement.FirstOrDefault(x => x.EmployeeID == Increment.EmployeeID);
            if (isExist != null)
            {
                context.EmployeeIncrement.Remove(isExist);
                //Transfer.Tdate = itob.Tdate.Add(DateTime.Now.TimeOfDay);
                context.EmployeeIncrement.Add(Increment);
                context.SaveChanges();
                return Json(Increment, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
    }
}