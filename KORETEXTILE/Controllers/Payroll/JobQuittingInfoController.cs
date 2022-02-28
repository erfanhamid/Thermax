using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.HRModule;
using BEEERP.Models.Log;
using BEEERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Payroll
{
    [ShowNotification]
    public class JobQuittingInfoController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: JobQuittingInfo
        public ActionResult JobQuittingInfo()
        {
            ViewBag.ReasonOfLeaving = LoadComboBox.ReasonOfLeaving();
            return View();
        }

        public ActionResult GetEmployeeById(int empId)
        {
            var employee = FindObjectById.GetEmployeeById(empId);
            if (employee != null)
            {
                return Json(new {Name=employee.Name, Designation=FindObjectById.SearchDesignation(employee.Designation).Name }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveJQI(JobQuittingInformation jqi)
        {
            string id = "";
            string yearLastTwoPart = jqi.Date.Year.ToString().Substring(2, 2).ToString();
            var noOfJQI = context.JobQuittingInformations.Count();
            if (noOfJQI > 0)
            {
                var lastJQI = context.JobQuittingInformations.ToList().LastOrDefault(x => x.JQINo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
               
                if (lastJQI == null)
                {
                    id = yearLastTwoPart + "00001";
                }
                else
                {
                    id = yearLastTwoPart + (Convert.ToInt32(lastJQI.JQINo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                }
            }
            else
            {
                id = yearLastTwoPart + "00001";
            }
            jqi.JQINo = Convert.ToInt32(id);
            jqi.Date = jqi.Date.Add(DateTime.Now.TimeOfDay);
            var leftemployee = context.Employees.FirstOrDefault(x => x.Id == jqi.EmployeeId);
            //try
            //{
                context.Employees.Remove(leftemployee);
                context.SaveChanges();
                leftemployee.Status = 0;
                context.Employees.Add(leftemployee);
                context.JobQuittingInformations.Add(jqi);
                UserLog.SaveUserLog(ref context, new UserLog(jqi.JQINo.ToString(), DateTime.Now, "JobQuittingInfo", ScreenAction.Save));
                context.SaveChanges();
                return Json(jqi, JsonRequestBehavior.AllowGet);
            //}
            //catch (Exception ex)
            //{
            //    return Json(0, JsonRequestBehavior.AllowGet);
            //}
        }
        // to search the JQI
        public ActionResult GetJQIById(int jqino)
        {
            var jqi = context.JobQuittingInformations.FirstOrDefault(x=>x.JQINo==jqino);
            if (jqi != null)
            {
                jqi.EmployeeName = context.Employees.FirstOrDefault(x => x.Id == jqi.EmployeeId).Name;
                jqi.Designation = (from l in context.Employees
                                   join de in context.Designation on l.Designation equals de.Id
                                   select new { name = de.Name }).FirstOrDefault().name;
                return Json(jqi, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateJQI(JobQuittingInformation jqi)
        {
            var jqiExist = context.JobQuittingInformations.AsNoTracking().FirstOrDefault(x => x.JQINo == jqi.JQINo);
            if (jqiExist != null)
            {
                context.Entry<JobQuittingInformation>(jqi).State = EntityState.Modified;
                UserLog.SaveUserLog(ref context, new UserLog(jqi.JQINo.ToString(), DateTime.Now, "JobQuittingInfo", ScreenAction.Update));
                context.SaveChanges();
                return Json(jqi, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteJQIById(int jqino)
        {
            var jqiExist = context.JobQuittingInformations.FirstOrDefault(x => x.JQINo == jqino);
            if (jqiExist != null)
            {
                context.JobQuittingInformations.Remove(jqiExist);
                UserLog.SaveUserLog(ref context, new UserLog(jqino.ToString(), DateTime.Now, "JobQuittingInfo", ScreenAction.Delete));
                context.SaveChanges();
                return Json(jqino, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult RevertJQIById(int jqino)
        {
            var jqiExist = context.JobQuittingInformations.FirstOrDefault(x => x.JQINo == jqino);
            if (jqiExist != null)
            {
                var eMployee = context.Employees.FirstOrDefault(x => x.Id == jqiExist.EmployeeId);
                context.Employees.Remove(eMployee);
                context.SaveChanges();
                eMployee.Status = 1;
                context.Employees.Add(eMployee);
                context.JobQuittingInformations.Remove(jqiExist);
                UserLog.SaveUserLog(ref context, new UserLog(jqino.ToString(), DateTime.Now, "JobQuittingInfo", ScreenAction.Update));
                context.SaveChanges();
                return Json(jqino, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
    }
}