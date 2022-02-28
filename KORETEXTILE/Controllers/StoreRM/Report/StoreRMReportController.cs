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

namespace BEEERP.Controllers.StoreRM.Report
{
    [ShowNotification]
    public class StoreRMReportController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: StoreRMReport
        public ActionResult ShowStoreRMReport()
        {
            ViewBag.Store = LoadComboBox.LoadAllStore();
            ViewBag.GroupId = LoadComboBox.LoadItemGroupRM();
            ViewBag.Item = LoadComboBox.LoadItem(null);
            return View();
        }

        public ActionResult GetStockPositionRM(InventoryReportVModel model)
        {

            Session["ReportName"] = "StockPositionRMR";

            ReportParmPersister param = new ReportParmPersister();
            param.AsOnDate = model.AsOnDate;
            Session["StockPositionRMOnDate"] = param;


            string sql = string.Format("exec StoreWiseFGStatus '" + DateTimeFormat.ConvertToDDMMYYYY(model.AsOnDate) + "','" + model.StoreId + "','',RM");
            var items = context.Database.SqlQuery<FGStatusReport>(sql).ToList();
            if (items.Count == 0)
            {
                FGStatusReport report = new FGStatusReport();
                items.Add(report);
            }
            Session["StockPositionRMReportData"] = items;
            return Redirect("/CrystalReport/StockPositionRawMaterials.aspx");
        }

        public ActionResult GetItemLedgerReport(InventoryReportVModel model)
        {
            Session["ReportName"] = "ItemLedger";

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;
            Session["ItemLedgerReportByDate"] = param;

            string sql = string.Format("exec ItemLedger '" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "', '" + model.Item + "','" + "RM" + "','" + model.StoreId + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "' ");
            var items = context.Database.SqlQuery<ItemLedgerReport>(sql).ToList();
            if (items.Count == 0)
            {
                ItemLedgerReport report = new ItemLedgerReport();
                items.Add(report);
            }
            Session["ItemLedgerReportData"] = items;
            return Redirect("/CrystalReport/ItemLedgerRpt.aspx");
        }

        public ActionResult GetRMItemList(InventoryReportVModel model)
        {
            Session["ReportName"] = "RMItemListR";
            string sql = string.Format("exec ItemList  '" + "RM" + "', '" + model.Group + "'");
            var items = context.Database.SqlQuery<ItemListReport>(sql).ToList();
            if (items.Count == 0)
            {
                ItemListReport report = new ItemListReport();
                items.Add(report);
            }
            Session["ItemListReportData"] = items;
            return Redirect("/CrystalReport/RMItemListRpt.aspx");
        }

    }
}