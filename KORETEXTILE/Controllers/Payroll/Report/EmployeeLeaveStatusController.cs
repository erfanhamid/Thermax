using BEEERP.CrystalReport.ReportFormat;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using BEEERP.Models.ViewModel.Sales.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Payroll.Report
{
    public class EmployeeLeaveStatusController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: EmployeeLeaveStatus
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowEmpoyeeLeaveStatusReport(ReportVModel model)
        {

            Session["ReportName"] = "EmployeeLeaveStatus";

            ReportParmPersister param = new ReportParmPersister();
            string sql = string.Format("exec LeaveStatus");
            var items = context.Database.SqlQuery<LeaveStatus>(sql).ToList();
            if (items.Count() == 0)
            {
                LeaveStatus report = new LeaveStatus();
                items.Add(report);
            }
            Session["LeaveStatusData"] = items;
            Session["LeaveStatusParam"] = param;
            return Redirect("/CrystalReport/LeaveStatusRpt.aspx");
        }
    }
}