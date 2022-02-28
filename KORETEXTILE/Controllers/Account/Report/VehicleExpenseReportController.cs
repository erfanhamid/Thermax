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
    public class VehicleExpenseReportController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: VehicleExpenseReport
        public ActionResult VehicleExpense()
        {
            ViewBag.Depot = LoadComboBox.LoadBranchInfo();
            ViewBag.Store = LoadAllStore();
            return View();
        }

        public ActionResult ItemWiseVehicleExpense(ReportVModel model)
        {
            BEEContext context = new BEEContext();

            Session["ReportName"] = "VehicleExpenseSummary";
            ReportParmPersister param = new ReportParmPersister();
                param.FromDate = model.From;
                param.ToDate = model.To;
                Session["VehicleExpenseParam"] = param;
                var items = context.Database.SqlQuery<ItemWiseVehicleExpense>(string.Format("exec ItemWiseVehicleExpenseSummary '"  + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'")).ToList();
                if (items.Count == 0)
                {
                ItemWiseVehicleExpense report = new ItemWiseVehicleExpense();
                    items.Add(report);
                }
                Session["VehicleExpenseData"] = items;
                return Redirect("/CrystalReport/ItemWiseVehicleExpenseRpt.aspx");
            
        }
        public ActionResult LedgerItemWiseVehicleExpense(ReportVModel model)
        {
            BEEContext context = new BEEContext();

            Session["ReportName"] = "LedgerVehicleExpenseSummary";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["LedgerVehicleExpenseParam"] = param;
            var items = context.Database.SqlQuery<LedgerItemWiseVehicleExpense>(string.Format("exec LedgerItemWiseVehicleExpenseSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'")).ToList();
            if (items.Count == 0)
            {
                LedgerItemWiseVehicleExpense report = new LedgerItemWiseVehicleExpense();
                items.Add(report);
            }
            Session["LedgerVehicleExpenseData"] = items;
            return Redirect("/CrystalReport/LedgerItemWiseVehicleExpenseRpt.aspx");

        }
        public ActionResult VehicleExpenseSummary(ReportVModel model)
        {
            BEEContext context = new BEEContext();

            Session["ReportName"] = "VehicleExpenseSummary";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            param.BranchName = context.BranchInformation.FirstOrDefault(x => x.Slno == model.Depot).BrnachName;
            
            Session["VehicleExpenseParam"] = param;
            var items = context.Database.SqlQuery<VehicleExpenseReport>(string.Format("exec VehicleExpenseSummary '" + model.Depot +"','" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'")).ToList();
            if (items.Count == 0)
            {
                VehicleExpenseReport report = new VehicleExpenseReport();
                items.Add(report);
            }
            Session["VehicleExpenseData"] = items;
            return Redirect("/CrystalReport/VehicleExpenseRpt.aspx");

        }


        public ActionResult DepotWiseVehicleExpenseDetails(ReportVModel model)
        {
            BEEContext context = new BEEContext();

            Session["ReportName"] = "DepotWiseVehicleExpenseDetails";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            param.BranchName = context.BranchInformation.FirstOrDefault(x => x.Slno == model.Depot).BrnachName;

            Session["DepotVehicleExpenseParam"] = param;
            var items = context.Database.SqlQuery<DepotWiseVehicleExpense>(string.Format("exec DepotVehicleExpenseDetails '" + model.Depot + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'")).ToList();
            if (items.Count == 0)
            {
                DepotWiseVehicleExpense report = new DepotWiseVehicleExpense();
                items.Add(report);
            }
            Session["DepotVehicleExpenseData"] = items;
            return Redirect("/CrystalReport/DepotWiseVehicleExpenseRpt.aspx");

        }

        public static SelectList LoadAllStore()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Vehicle ---");
            var sql = context.Database.SqlQuery<LoadStore>(string.Format("Select Id, Name from Store")).ToList();
            sql.ForEach(x => { items.Add(x.Id.ToString(), x.Id.ToString()+" - "+ x.Name); });
            return new SelectList(items, "Key", "Value");
        }
        public sealed class LoadStore
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

    }
}

