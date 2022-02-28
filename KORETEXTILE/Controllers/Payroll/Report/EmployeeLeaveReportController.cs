using BEEERP.CrystalReport.ReportFormat;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.ViewModel.Sales.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Payroll.Report
{
    [ShowNotification]
    public class EmployeeLeaveReportController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: EmployeeLeaveReport
        public ActionResult LeaveReport()
        {
            ViewBag.Emp = LoadComboBox.LoadAllEmployee();
            return View();
        }
        public ActionResult ShowEmpoyeeLeaveReport(ReportVModel model)
        {

            Session["ReportName"] = "EmployeeLeaveReport";

            ReportParmPersister param = new ReportParmPersister();
            string sql = string.Format("exec EmployeeLeaveRecordDetails '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "', '" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "','" + model.EmployeeID + "' ");
            var items = context.Database.SqlQuery<EmployeeLeaveReport>(sql).ToList();
            param.FromDate = model.From;
            param.ToDate = model.To;
            if (items.Count() == 0)
            {
                EmployeeLeaveReport report = new EmployeeLeaveReport();
                items.Add(report);
            }
            Session["empLeaveReportData"] = items;
            Session["empLeaveParam"] = param;
            return Redirect("/CrystalReport/EmployeeLeaveReportRpt.aspx");
        }
    }
}