using BEEERP.CrystalReport.ReportFormat;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.ViewModel.Sales.Report;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Payroll.Report
{
    [ShowNotification]
    public class AttandanceLogReportController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: AttandanceLogReport
        public ActionResult AttandanceLog()
        {
            ViewBag.Emp = LoadComboBox.LoadAllEmployee();
            return View();
        }
        public ActionResult ShowAttandancelogDetails(ReportVModel model)
        {

            Session["ReportName"] = "AttandanceLog";

            ReportParmPersister param = new ReportParmPersister();
            string sql = string.Format("exec EmployeeAttendanceLogDetails '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "', '" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "','" + model.EmployeeID + "' ");
            var items = context.Database.SqlQuery<AttandanceLog>(sql).ToList();
            param.FromDate = model.From;
            param.ToDate = model.To;


            if (items.Count == 0)
            {
                AttandanceLog report = new AttandanceLog();
                items.Add(report);
            }
            Session["AttandanceLogData"] = items;
            Session["AttandanceLogParam"] = param;
            return Redirect("/CrystalReport/AttandanceLogRpt.aspx");
        }

        public ActionResult ShowEmpAttandancelogDetails(ReportVModel model, string starthour = "09:00:59", string endhour = "17:00:59")
        {

            Session["ReportName"] = "EmpAttandanceLog";

            ReportParmPersister param = new ReportParmPersister();
            string sql = string.Format("exec AttendanceReport '" + starthour + "','" + endhour + "', '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "', '" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "','" + model.EmployeeID + "'");
            var items = context.Database.SqlQuery<EmployeeAttandanceLog>(sql).ToList();
            param.FromDate = model.From;
            param.ToDate = model.To;


            if (items.Count == 0)
            {
                EmployeeAttandanceLog report = new EmployeeAttandanceLog();
                items.Add(report);
            }
            Session["EmpAttandanceLogData"] = items;
            Session["EmpAttandanceLogParam"] = param;
            return Redirect("/CrystalReport/EmployeeAttandanceLogRpt.aspx");
        }
        [HttpGet]
        public ActionResult MonthWiseAttendanceSummary()
        {
            ViewBag.Months = LoadComboBox.LoadMonths();
            ViewBag.Employees = LoadComboBox.LoadAllEmployeeByCompanyId(null);
            ViewBag.Company = LoadComboBox.LoadCompanyInformation();
            return View();
        }
        [HttpPost]
        public ActionResult MonthWiseAttendanceSummary(ReportVModel reportmodel)
        {
            if (ModelState.IsValid)
            {
                if (reportmodel.EmployeeID < 0)
                {
                    reportmodel.EmployeeID = 0;
                }
                Session["ReportName"] = "Month Wise Attendance Summary";
                string sql = string.Format("exec MonthWiseAttSum " + reportmodel.Month + "," + reportmodel.Year + "," + reportmodel.EmployeeID + "," + reportmodel.CompanyId + "");
                ReportParmPersister param = new ReportParmPersister();
                string fullMonthName = new DateTime(reportmodel.Year, reportmodel.Month, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("en"));
                param.Month = fullMonthName + "-" + reportmodel.Year.ToString();
                param.CompanyName = context.CompanyInfo.FirstOrDefault(x => x.Id == reportmodel.CompanyId).CompanyName;
                Session["MonthWiseAttendanceParam"] = param;
                var items = context.Database.SqlQuery<MonthWiseAttendanceSummary>(sql).ToList();
                Session["MonthWiseAttendanceSummary"] = items;
                return Redirect("/CrystalReport/MonthWiseAttendanceSummaryRpt.aspx");
            }
            else
            {
                ViewBag.Months = LoadComboBox.LoadMonths();
                ViewBag.Employees = LoadComboBox.LoadAllEmployeeByCompanyId(null);
                ViewBag.Company = LoadComboBox.LoadCompanyInformation();
                return View(reportmodel);
            }

        }
        public JsonResult GetEmployeesByCompanyId(int companyid)
        {
            return Json(LoadComboBox.LoadAllEmployeeByCompanyId(companyid), JsonRequestBehavior.AllowGet);
        }
    }
}