using BEEERP.CrystalReport.ReportFormat;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.ViewModel.Sales.Report;
using BEEERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Account.Report
{
    [ShowNotification]
    public class CostCenterReportController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: CostCenterReport
        public ActionResult CostCenterReport()
        {
            ViewBag.Depot = LoadComboBox.LoadBranchInfo();
            ViewBag.CostGroup = LoadComboBox.LoadAllSection();
            return View();
        }
        // to show Depot and Cost Group Wise Expense Detail
        public ActionResult DepotandCostGroupWiseDetail(ReportVModel model)
        {
            BEEContext context = new BEEContext();

            Session["ReportName"] = "DepotandCostGroupWiseSummaryR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            //param.DepotID = model.DepotId;
            //var data = context.Database.SqlQuery<BranchInformation>(string.Format("select BrnachName from BranchInformation where Slno ='" + model.Depot + "'")).ToList();
            //param.DepotName = data.FirstOrDefault().BrnachName;

            Session["DepotandCostGroupWiseSummaryParam"] = param;
            var items = context.Database.SqlQuery<DepotandCostGroupWiseSummaryReport>(string.Format("exec spCostGroupWiseExpSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'")).ToList();
            if (items.Count == 0)
            {
                DepotandCostGroupWiseSummaryReport report = new DepotandCostGroupWiseSummaryReport();
                items.Add(report);
            }
            Session["DepotandCostGroupWiseSummaryData"] = items;
            return Redirect("/CrystalReport/DepotandCostGroupWiseSummaryRpt.aspx");

        }
        // to show Depot and Cost Group Wise Expense Summary
        public ActionResult DepotandCostGroupWiseSummary(ReportVModel model)
        {
            BEEContext context = new BEEContext();

            Session["ReportName"] = "DepotandCostGroupWiseSummaryyyR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            //param.DepotID = model.DepotId;
            //var data = context.Database.SqlQuery<BranchInformation>(string.Format("select BrnachName from BranchInformation where Slno ='" + model.Depot + "'")).ToList();
            //param.DepotName = data.FirstOrDefault().BrnachName;

            Session["DepotandCostGroupWiseSummaryyyParam"] = param;
            var items = context.Database.SqlQuery<DepotWiseCostGroupSummaryReport>(string.Format("exec spDepotCostGroupSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'")).ToList();
            if (items.Count == 0)
            {
                DepotWiseCostGroupSummaryReport report = new DepotWiseCostGroupSummaryReport();
                items.Add(report);
            }
            Session["DepotandCostGroupWiseSummaryyyData"] = items;
            return Redirect("/CrystalReport/DepotandCostGroupWiseSummaryyyRpt.aspx");

        }
        // Single Depot Cost/Expense Summary
        public ActionResult SingleDepotCostSummary(ReportVModel model)
        {
            BEEContext context = new BEEContext();

            Session["ReportName"] = "SingleDepotCostSummaryR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            param.DepotID = model.DepotId;
            //var data = context.Database.SqlQuery<BranchInformation>(string.Format("select BrnachName from BranchInformation where Slno ='" + model.Depot + "'")).ToList();
            //param.DepotName = data.FirstOrDefault().BrnachName;

            Session["SingleDepotCostSummaryParam"] = param;
            var items = context.Database.SqlQuery<SingleDepotCostSummary>(string.Format("exec spSingleDepotCostSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "','" + model.Depot + "'")).ToList();
            if (items.Count == 0)
            {
                SingleDepotCostSummary report = new SingleDepotCostSummary();
                items.Add(report);
            }
            Session["SingleDepotCostSummaryData"] = items;
            return Redirect("/CrystalReport/SingleDepotCostSummaryRpt.aspx");

        }
        // Single Depot Account Group Summary
        public ActionResult SingleDepotAccountGroupSummary(ReportVModel model)
        {
            BEEContext context = new BEEContext();

            Session["ReportName"] = "SingleDepotAccountGroupSummaryR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            param.DepotID = model.DepotId;
            //var data = context.Database.SqlQuery<BranchInformation>(string.Format("select BrnachName from BranchInformation where Slno ='" + model.Depot + "'")).ToList();
            //param.DepotName = data.FirstOrDefault().BrnachName;

            Session["SDAGSParam"] = param;
            var items = context.Database.SqlQuery<SingleDepotAccGroupSummaryReport>(string.Format("exec spSingleDepotAccountGroupSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "','" + model.Depot + "'")).ToList();
            if (items.Count == 0)
            {
                SingleDepotAccGroupSummaryReport report = new SingleDepotAccGroupSummaryReport();
                items.Add(report);
            }
            Session["SDAGSData"] = items;
            return Redirect("/CrystalReport/SingleDepotAccGrpsummaryRpt.aspx");

        }
        // to Depot Wise Cost Summary
        public ActionResult Depotwisecostsummary(ReportVModel model)
        {
            BEEContext context = new BEEContext();

            Session["ReportName"] = "DepotwisecostsummaryR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            //param.DepotID = model.DepotId;
            //var data = context.Database.SqlQuery<BranchInformation>(string.Format("select BrnachName from BranchInformation where Slno ='" + model.Depot + "'")).ToList();
            //param.DepotName = data.FirstOrDefault().BrnachName;

            Session["DWCSParam"] = param;
            var items = context.Database.SqlQuery<Depotwisecostsummaryreport>(string.Format("exec spDepotwisecostsummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'")).ToList();
            if (items.Count == 0)
            {
                Depotwisecostsummaryreport report = new Depotwisecostsummaryreport();
                items.Add(report);
            }
            Session["DWCSData"] = items;
            return Redirect("/CrystalReport/DepotwisecostsummaryRpt.aspx");

        }
        public ActionResult DepotWiseSingleCostGroupDetail(ReportVModel model)
        {
            BEEContext context = new BEEContext();

            Session["ReportName"] = "DepotWiseSingleCostGroupDetailR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            //param.DepotID = model.DepotId;
            param.CGroup = model.CostGroup;

            param.CostGroup = context.EmployeeSection.FirstOrDefault(x => x.ID == model.CostGroup).CGroup;

            //var cgrpname = context.Database.SqlQuery<EmployeeSection>(string.Format("select CGroup from CostGroup where ID ='" + model.CostGroup + "'")).ToList();
            //param.CostGroup = cgrpname.FirstOrDefault().CGroup;

            Session["DWSCGDetailParam"] = param;
            var items = context.Database.SqlQuery<DepotWiseSingleCGDetailReport>(string.Format("exec spDepotWiseSingleCostGroupDetail '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "','" + model.CostGroup + "'")).ToList();
            if (items.Count == 0)
            {
                DepotWiseSingleCGDetailReport report = new DepotWiseSingleCGDetailReport();
                items.Add(report);
            }
            Session["DWSCGDetailData"] = items;
            return Redirect("/CrystalReport/DepotWiseSingleCostGroupDetailRpt.aspx");

        }
        // to Cost Group Wise Summary
        public ActionResult CostGroupWiseSummary(ReportVModel model)
        {
            BEEContext context = new BEEContext();

            Session["ReportName"] = "CostGroupWiseSummaryR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            //param.DepotID = model.DepotId;
            //var data = context.Database.SqlQuery<BranchInformation>(string.Format("select BrnachName from BranchInformation where Slno ='" + model.Depot + "'")).ToList();
            //param.DepotName = data.FirstOrDefault().BrnachName;

            Session["CGWSParam"] = param;
            var items = context.Database.SqlQuery<CostGroupWiseSummaryReport>(string.Format("exec spCostGroupWiseSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'")).ToList();
            if (items.Count == 0)
            {
                CostGroupWiseSummaryReport report = new CostGroupWiseSummaryReport();
                items.Add(report);
            }
            Session["CGWSData"] = items;
            return Redirect("/CrystalReport/CostGroupWiseSummaryRpt.aspx");

        }
        public ActionResult SingleDepotMonthWiseCost(ReportVModel model)
        {
            Session["ReportName"] = "SingleDepotMonthWiseCostR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            param.DepotID = model.DepotId;
            param.BranchName = context.BranchInformation.FirstOrDefault(x => x.Slno == model.Depot).BrnachName;
            Session["SDMWCParam"] = param;

            string sql = string.Format("exec SingleDepotMonthWiseCost" + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "'" + "," + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "','" + model.Depot + "'");
            var items = context.Database.SqlQuery<SingleDepotMonthWiseCostReport>(sql).ToList();
            if (items.Count == 0)
            {
                SingleDepotMonthWiseCostReport report = new SingleDepotMonthWiseCostReport();
                items.Add(report);
            }
            Session["SDMWCData"] = items;
            return Redirect("/CrystalReport/SingleDepotMonthWiseCostRpt.aspx");
        }
    }

}