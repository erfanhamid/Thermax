using BEEERP.CrystalReport.ReportFormat;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.ViewModel.Sales.Report;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Payroll.Report
{
    [ShowNotification]
    public class SalaryReportController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: SalaryReport
        public ActionResult SalaryReport()
        {
            ViewBag.Months = LoadComboBox.LoadAllMonths();
            ViewBag.AllEmployees = LoadComboBox.LoadAllEmployee();
            return View();
        }
        public ActionResult ShowRemunerationStatementforSeniorManagement(ReportVModel model)
        {
            Session["ReportName"] = "RemunerationStatementforSeniorManagementR";
            ReportParmPersister param = new ReportParmPersister();
            param.CompanyName = "Asian Petroleum Limited";
            //param.ReportName = "Remuneration Statement for Senior Management";
            param.Months = 0;
            param.Year = 0;
            param.Month = "From " + DateTimeFormat.ConvertToDDMMYYYY(model.From) + " To " + DateTimeFormat.ConvertToDDMMYYYY(model.To);
            Session["RemunerationStatementforSeniorManagementParam"] = param;

            string sql = string.Format("exec RemunerationStatementforSeniorManagement '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "', '" + model.EmployeeID + "' ");
            var items = context.Database.SqlQuery<RemunerationStatementforSeniorManagementReport>(sql).ToList();
            if (items.Count == 0)
            {
                RemunerationStatementforSeniorManagementReport report = new RemunerationStatementforSeniorManagementReport();
                items.Add(report);
            }
            Session["RemunerationStatementforSeniorManagementData"] = items;
            return Redirect("/CrystalReport/RemunerationStatementforSeniorManagementRpt.aspx");

        }
        public ActionResult ShowSalarySheetSummary(ReportVModel model)
        {
            Session["ReportName"] = "SalarySheetSummaryR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;

            Session["SalarySheetSummaryParam"] = param;

            string sql = string.Format("exec SalarySheetSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "' ");
            var items = context.Database.SqlQuery<SalarySheetSummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                SalarySheetSummaryReport report = new SalarySheetSummaryReport();
                items.Add(report);
            }
            Session["SalarySheetSummaryReportData"] = items;
            return Redirect("/CrystalReport/SalarySheetSummaryReportRpt.aspx");

        }
        public ActionResult ShowCompanyWiseSalarySummary(ReportVModel model)
        {
            ReportParmPersister param = new ReportParmPersister();
            param.Month = "Summary From " + model.From.ToString("dd-MM-yyyy") + " To " + model.To.ToString("dd-MM-yyyy");
            Session["ReportName"] = "CompanyWiseSalarySummaryR";
            param.CompanyName = "Asian Petroleum Limited";
            param.Year = model.Year;
            Session["CompanyWiseSalarySummaryParam"] = param;

            string sql = string.Format("exec CompanyWiseSalarySummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<CompanyWiseSalarySummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                CompanyWiseSalarySummaryReport report = new CompanyWiseSalarySummaryReport();
                items.Add(report);
            }
            Session["CompanyWiseSalarySummaryData"] = items;
            return Redirect("/CrystalReport/CompanyWiseSalarySummaryReportRpt.aspx");
        }
        // Salary increment Report
        public ActionResult ShowSalaryIncrementReport(ReportVModel model)
        {
            ReportParmPersister param = new ReportParmPersister();
            string sql = string.Format("exec EmployeeSalaryIncrement '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'," + model.EmployeeID + "");
            var items = context.Database.SqlQuery<EmployeeSalaryIncrementReport>(sql).ToList();
            if (items.Count == 0)
            {
                EmployeeSalaryIncrementReport report = new EmployeeSalaryIncrementReport();
                items.Add(report);
            }
            if (items.Count > 0)
            {
                param.CompanyName = (from cmp in context.CompanyInfo
                                     join emp in context.Employees on cmp.Id equals emp.CompanyId
                                     select new { CompanyName = cmp.CompanyName, EmpId = emp.Id }).FirstOrDefault(x => x.EmpId == model.EmployeeID).CompanyName.ToString();
                param.EmployeeID = model.EmployeeID;
                param.Description = context.Employees.FirstOrDefault(x => x.Id == model.EmployeeID).Name;
            }
            param.Month = "From " + model.From.Date.ToString("dd-MM-yyyy") + " to " + model.To.Date.ToString("dd-MM-yyyy");
            Session["SalaryIncrementReportParam"] = param;
            Session["SalaryIncrementReportData"] = items;
            return Redirect("/CrystalReport/SalaryIncrementRpt.aspx");
        }
    }
}