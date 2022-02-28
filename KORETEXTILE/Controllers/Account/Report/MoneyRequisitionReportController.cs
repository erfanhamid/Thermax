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

namespace BEEERP.Controllers.Account.Report
{
    [ShowNotification]
    public class MoneyRequisitionReportController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: MoneyRequisitionReport
        public ActionResult MoneyRequisitionReport()
        {
            return View();
        }
        public ActionResult EmployeeMRBalanceSummary(ReportVModel model)
        {
            BEEContext context = new BEEContext();

            Session["ReportName"] = "EmployeeMRBalanceSummaryR";
            ReportParmPersister param = new ReportParmPersister();
            //param.FromDate = model.From;
            //param.ToDate = model.To;
            param.AsOnDate = model.AsOnDate;
            //param.DepotID = model.DepotId;
            //var data = context.Database.SqlQuery<BranchInformation>(string.Format("select BrnachName from BranchInformation where Slno ='" + model.Depot + "'")).ToList();
            //param.DepotName = data.FirstOrDefault().BrnachName;

            Session["MRBalanceSummaryParam"] = param;
            var items = context.Database.SqlQuery<MRBalanceSummaryReport>(string.Format("exec spMRBalanceSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.AsOnDate) + "'")).ToList();
            if (items.Count == 0)
            {
                MRBalanceSummaryReport report = new MRBalanceSummaryReport();
                items.Add(report);
            }
            Session["MRBalanceSummaryData"] = items;
            return Redirect("/CrystalReport/MRBalanceSummaryRpt.aspx");

        }
    }
}