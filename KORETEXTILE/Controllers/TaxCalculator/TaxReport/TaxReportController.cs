using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.ViewModel.TaxCalculator;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.Models.Database;

namespace BEEERP.Controllers.TaxCalculator.TaxReport
{
    [ShowNotification]
    public class TaxReportController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: TaxReport
        public ActionResult TaxReport() 
        {
            ViewBag.DuringYear = LoadComboBox.LoadDuringYear();
            ViewBag.Cororation = LoadComboBox.LoadCorporation();
            ViewBag.LoadLocation = LoadComboBox.LoadLocation();
            return View();
        }

        public ActionResult GetEmployeeTaxBalanceLedgerReport(TaxReportViewModel model)
        {
            Session["ReportName"] = "EmployeeTaxBalanceLedger";

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;
            param.EmployeeID = model.EmployeeID;
            //param.EmployeeName = FindObjectById.GetERMTransection()
            Session["EmployeeTaxBalanceLedgerParam"] = param;

            string sql = string.Format("exec EmployeeTaxBalanceLedger '" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "', '" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "', '" + model.EmployeeID + "' ");
            var items = context.Database.SqlQuery<EmployeeTaxBalanceLedgerReport>(sql).ToList();
            if (items.Count() == 0)
            {
                EmployeeTaxBalanceLedgerReport report = new EmployeeTaxBalanceLedgerReport();
                items.Add(report);
            }
            Session["EmployeeTaxBalanceLedgerData"] = items;
            return Redirect("/CrystalReport/EmployeeTaxBalanceLedgerRpt.aspx");
        }

        public ActionResult GetEmployeeAiTBalanceLedgerReport(TaxReportViewModel model) 
        {
            Session["ReportName"] = "EmployeeAiTBalanceLedger";

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;
            param.EmployeeID = model.EmployeeID;
            //param.EmployeeName = FindObjectById.GetERMTransection()
            Session["EmployeeAiTBalanceLedgerParam"] = param;

            string sql = string.Format("exec EmployeeAITBalanceLedger '" + model.EmployeeID + "', '" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "', '" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "' ");
            var items = context.Database.SqlQuery<EmployeeAITBalanceLedgerReport>(sql).ToList();
            if (items.Count() == 0)
            {
                EmployeeAITBalanceLedgerReport report = new EmployeeAITBalanceLedgerReport();
                items.Add(report);
            }
            Session["EmployeeAiTBalanceLedgerData"] = items;
            return Redirect("/CrystalReport/EmployeeAiTBalanceLedgerRpt.aspx");
        }
    }
}