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

namespace BEEERP.Controllers.CommercialModule.Report
{
    [ShowNotification]
    public class CommercialReportController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: CommercialReport
        public ActionResult CommercialReport()
        {
            ViewBag.SupplierGroup = LoadComboBox.LoadAllSupplierGroup();
            return View();
        }

        public ActionResult ShowImportPISummary(ReportVModel model)
        {
            Session["ReportName"] = "ImportPISummaryNewR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;

            Session["IPISParam"] = param;

            string sql = string.Format("exec spImportPISummary  '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<ImportPISummaryNewReport>(sql).ToList();
            if (items.Count == 0)
            {
                ImportPISummaryNewReport report = new ImportPISummaryNewReport();
                items.Add(report);
            }
            Session["IPISData"] = items;
            return Redirect("/CrystalReport/ImportPISummaryNewRpt.aspx");
        }
        public ActionResult ShowImportLCSummary(ReportVModel model)
        {
            Session["ReportName"] = "ImportLCSummaryNewR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;

            Session["ILCSParam"] = param;

            string sql = string.Format("exec spImportLCSummary  '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<ImportLCSummaryNewReport>(sql).ToList();
            if (items.Count == 0)
            {
                ImportLCSummaryNewReport report = new ImportLCSummaryNewReport();
                items.Add(report);
            }
            Session["ILCSData"] = items;
            return Redirect("/CrystalReport/ImportLCSummaryNewRpt.aspx");
        }
        public ActionResult GetSupplierWiseLCSummary(ReportVModel model)
        {

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["SupplierWiseLCSummaryParam"] = param;
            string sql = string.Format("exec spSupplierWiseImportLCSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<SupplierWiseLCSummary>(sql).ToList();
            if (items.Count == 0)
            {
                SupplierWiseLCSummary report = new SupplierWiseLCSummary();
                items.Add(report);
            }
            Session["SupplierWiseLCSummaryData"] = items;
            return Redirect("/CrystalReport/SupplierWiseLCSummaryRpt.aspx");
        }
        public ActionResult GetItemWiseLCSummary(ReportVModel model)
        {

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["ItemWiseLCSummaryParam"] = param;
            string sql = string.Format("exec spItemWiseLCSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<ItemWiseLCSummary>(sql).ToList();
            if (items.Count == 0)
            {
                ItemWiseLCSummary report = new ItemWiseLCSummary();
                items.Add(report);
            }
            Session["ItemWiseLCSummaryData"] = items;
            return Redirect("/CrystalReport/ItemWiseLCSummaryRpt.aspx");
        }
        public ActionResult GetOnlyItemWiseLCSummary(ReportVModel model)
        {

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["OnlyItemWiseLCSummaryParam"] = param;
            string sql = string.Format("exec spOnlyItemWiseLCSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<OnlyItemWiseLCSummary>(sql).ToList();
            if (items.Count == 0)
            {
                OnlyItemWiseLCSummary report = new OnlyItemWiseLCSummary();
                items.Add(report);
            }
            Session["OnlyItemWiseLCSummaryData"] = items;
            return Redirect("/CrystalReport/OnlyItemWiseLCSummaryRpt.aspx");
        }
    }
}