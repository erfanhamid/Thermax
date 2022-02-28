using BEEERP.CrystalReport.ReportFormat;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.SalesModule;
using BEEERP.Models.ViewModel.Sales.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Account.Report
{
    [ShowNotification]
    public class ManagementReportController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: ManagementReport
        public ActionResult ManagementReport()
        {
            ViewBag.Depot = LoadComboBox.LoadBranchInfo();
            ViewBag.VehcType = LoadComboBox.LoadVehicleType();
            ViewBag.SubLedger = LoadComboBox.LoadAllSubLedgerBytype(null);
            //ViewBag.Store = LoadAllStore();
            return View();
        }
        public ActionResult GetSubledgerByType(string vtype)
        {
            return Json(LoadComboBox.LoadAllSubLedgerBytype(vtype), JsonRequestBehavior.AllowGet);
        }
        public static SelectList LoadAllStore()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Store ---");
            var sql = context.Database.SqlQuery<LoadDepot>(string.Format("Select slno, BrnachName from BranchInformation")).ToList();
            sql.ForEach(x => { items.Add(x.slno.ToString(), x.slno.ToString() + " - " + x.BrnachName); });
            return new SelectList(items, "Key", "Value");
        }

        public ActionResult MonthlyExpenseReport(ReportVModel model)
        {
            Session["ReportName"] = "MonthlyExpenseReport";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["MonthlyExpenseReportParam"] = param;

            string sql = string.Format("exec MonthlyExpenseReport" + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "'" + "," + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<MonthlyExpenseReport>(sql).ToList();
            if (items.Count == 0)
            {
                MonthlyExpenseReport report = new MonthlyExpenseReport();
                items.Add(report);
            }
            Session["MonthlyExpenseReportData"] = items;
            return Redirect("/CrystalReport/MonthlyExpenseReportRpt.aspx");
        }
        public ActionResult VehicleExpenseDetails(ReportVModel model)
        {
            BEEContext context = new BEEContext();

            Session["ReportName"] = "VehicleExpenseDetails";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            //var data = context.Database.SqlQuery<LoadStore>(string.Format("Select Name from Store where id ='" + model.StoreId + "'")).ToList();
            //param.StoreName = data.FirstOrDefault().Name;

            Session["VehicleExpenseDetailsParam"] = param;
            var items = context.Database.SqlQuery<VehicleExpenseDetailsReport>(string.Format("exec VehicleExpenseDetails '" + model.SubLedgerID + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'")).ToList();
            if (items.Count == 0)
            {
                VehicleExpenseDetailsReport report = new VehicleExpenseDetailsReport();
                items.Add(report);
            }
            Session["VehicleExpenseDetailsData"] = items;
            return Redirect("/CrystalReport/VehicleExpenseDetailsRpt.aspx");

        }
        public sealed class LoadDepot
        {
            public int slno { get; set; }
            public string BrnachName { get; set; }
        }

    }
}
        