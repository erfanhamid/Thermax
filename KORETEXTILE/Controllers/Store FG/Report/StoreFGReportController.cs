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

namespace BEEERP.Controllers.Store_FG.Report
{
    [ShowNotification]
    public class StoreFGReportController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: StoreFGReport
        public ActionResult ShowStoreFGReport()
        {
            //var depot = ScreenSessionData.GetEmployeeDepot();
            //ViewBag.DepotId = depot;
            //if (depot == null)
            //{
            //    ViewBag.Store = LoadComboBox.LoadAllFGStoreByDepot(null);
            //}
            //else
            //{
            //    ViewBag.Store = LoadComboBox.LoadAllFGStoreByDepot(depot);
               
            //}
            ViewBag.Store = LoadComboBox.LoadAllFGStores();
            ViewBag.GroupId = LoadComboBox.LoadItemGroup();
            ViewBag.Item = LoadComboBox.LoadItem(null);
            return View();
        }

        
        public ActionResult GetStoreWiseItemQty(InventoryReportVModel model)
        {
            //Session["ReportName"] = "Finish Goods Production Summary";
            ReportParmPersister param = new ReportParmPersister();
            param.AsOnDate = model.AsOnDate;
            //param.ToDate = model.ToDate;
            param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;
            param.RootAccountType = "FG";

            Session["storeParam"] = param;
            string sql = string.Format("exec StoreWiseItemQty '" + DateTimeFormat.ConvertToDDMMYYYY(model.AsOnDate) + "'" + "," + model.StoreId + "," + "'FG'");
            var items = context.Database.SqlQuery<StoreWiseItemQtyReport>(sql).ToList();
            if (items.Count == 0)
            {
                StoreWiseItemQtyReport report = new StoreWiseItemQtyReport();
                items.Add(report);
            }
            Session["storeData"] = items;
            return Redirect("/CrystalReport/StoreWiseItemQtyRpt.aspx");
        }
        public ActionResult GetItemWiseInventorySummary(InventoryReportVModel model)
        {
            //Session["ReportName"] = "DealerLedger";
            ReportParmPersister param = new ReportParmPersister();
            //var store = context.Store.FirstOrDefault(x => x.Id == model.StoreId);
            param.AsOnDate = model.AsOnDate;
            Session["ItemParam"] = param;
            string sql = string.Format("exec ItemWiseInventorySummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.AsOnDate) + "'");
            var items = context.Database.SqlQuery<ItemWiseInventorySummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                ItemWiseInventorySummaryReport report = new ItemWiseInventorySummaryReport();
                items.Add(report);
            }
            Session["ItemReportData"] = items;
            return Redirect("/CrystalReport/ItemWiseInventorySummaryRpt.aspx");
        }

        public ActionResult GetInventorySummaryByAllStore(InventoryReportVModel model)
        {
            //Session["ReportName"] = "DealerLedger";
            ReportParmPersister param = new ReportParmPersister();
            //var store = context.Store.FirstOrDefault(x => x.Id == model.StoreId);
            param.AsOnDate = model.AsOnDate;
            Session["InvParam"] = param;
            string sql = string.Format("exec StoreWiseAllItemSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.AsOnDate) + "'");
            var items = context.Database.SqlQuery<InventorySummaryByAllStoreReport>(sql).ToList();
            if (items.Count == 0)
            {
                InventorySummaryByAllStoreReport report = new InventorySummaryByAllStoreReport();
                items.Add(report);
            }
            Session["InvReportData"] = items;
            return Redirect("/CrystalReport/InventorySummaryByAllStoreRpt.aspx");
        }
        public ActionResult GetStoreWiseInventoryValueSummary(InventoryReportVModel model)
        {
            //Session["ReportName"] = "DealerLedger";
            ReportParmPersister param = new ReportParmPersister();
            //var store = context.Store.FirstOrDefault(x => x.Id == model.StoreId);
            param.AsOnDate = model.AsOnDate;
            Session["StParam"] = param;
            string sql = string.Format("exec StoreWiseInventoryValueSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.AsOnDate) + "'");
            var items = context.Database.SqlQuery<StoreWiseInventoryValueSummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                StoreWiseInventoryValueSummaryReport report = new StoreWiseInventoryValueSummaryReport();
                items.Add(report);
            }
            Session["StReportData"] = items;
            return Redirect("/CrystalReport/StoreWiseInventoryValueSummaryRpt.aspx");
        }


        public ActionResult GetItemLedgerReport(InventoryReportVModel model)
        {
            Session["ReportName"] = "ItemLedger";

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;
            param.ItemID = model.ItemID;
            param.ItemGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
            param.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
            param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;

            Session["ItemLedgerReportByDate"] = param;

            string sql = string.Format("exec ItemLedger '" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "', '" + model.ItemID + "','" + "FG" + "','" + model.StoreId + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "' ");
            var items = context.Database.SqlQuery<ItemLedgerReport>(sql).ToList();
            if (items.Count == 0)
            {
                ItemLedgerReport report = new ItemLedgerReport();
                items.Add(report);
            }
            Session["ItemLedgerReportData"] = items;
            return Redirect("/CrystalReport/ItemLedgerRpt.aspx");
        }

        //public ActionResult GetFGItemList(InventoryReportVModel model)
        //{
        //    Session["ReportName"] = "ItemListR";
        //    string sql = string.Format("exec ItemList  '"+"FG"+"', '"+model.Group+"'");
        //    var items = context.Database.SqlQuery<ItemListReport>(sql).ToList();
        //    if (items.Count == 0)
        //    {
        //        ItemListReport report = new ItemListReport();
        //        items.Add(report);
        //    }
        //    Session["ItemListReportData"] = items;
        //    return Redirect("/CrystalReport/ItemListRpt.aspx");
        //}      

        public ActionResult GetStockSumamry(InventoryReportVModel model)
        {
            Session["ReportName"] = "StockSummaryR";

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;
            param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;
            Session["StockSummaryReportByDate"] = param;

            string sql = string.Format("exec StockSummary '" + model.StoreId + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "' ");
            var items = context.Database.SqlQuery<StockSummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                StockSummaryReport report = new StockSummaryReport();
                items.Add(report);
            }
            Session["StockSummaryReportData"] = items;
            return Redirect("/CrystalReport/StockSummaryRpt.aspx");
        }

        //public ActionResult GetItemWiseTransactionSummary(InventoryReportVModel model)
        //{
        //    Session["ReportName"] = "ItemWiseTransactionSummaryR";

        //    ReportParmPersister param = new ReportParmPersister();
        //    param.FromDate = model.FromDate;
        //    param.ToDate = model.ToDate;

        //    Session["ItemWiseTransactionSummaryParam"] = param;
        //    string sql = string.Format("exec ItemWiseTransactionSummary '" + model.StoreId + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "' ");
        //    var items = context.Database.SqlQuery<ItemWiseTransactionSummaryReport>(sql).ToList();
        //    if (items.Count == 0)
        //    {
        //        ItemWiseTransactionSummaryReport report = new ItemWiseTransactionSummaryReport();
        //        items.Add(report);
        //    }
        //    Session["ItemWiseTransactionSummaryData"] = items;
        //    return Redirect("/CrystalReport/ItemWiseTransactionSummaryRpt.aspx");
        //}        

        //public ActionResult GetIssueReceiveSumamry(InventoryReportVModel model)
        //{
        //    Session["ReportName"] = "IssueReceiveSumamryR";

        //    ReportParmPersister param = new ReportParmPersister();
        //    param.FromDate = model.FromDate;
        //    param.ToDate = model.ToDate;
        //    param.StoreName = context.Store.FirstOrDefault(m => m.Id == model.StoreFor).Name;
        //    Session["IssueReceiveSumamryParam"] = param;

        //    string sql = string.Format("exec ItemWiseIssueReceiveSumamry '" + model.StoreId + "','"+model.StoreFor + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "' ");
        //    var items = context.Database.SqlQuery<IssueReceiveSumamryReport>(sql).ToList();
        //    if (items.Count == 0)
        //    {
        //        IssueReceiveSumamryReport report = new IssueReceiveSumamryReport();
        //        items.Add(report);
        //    }
        //    Session["IssueReceiveSumamryData"] = items;
        //    return Redirect("/CrystalReport/IssueReceiveSumamryRpt.aspx");
        //}

        //public ActionResult GetIssueReceiveDetails(InventoryReportVModel model)
        //{
        //    Session["ReportName"] = "IssueReceiveDetailsR";

        //    ReportParmPersister param = new ReportParmPersister();
        //    param.FromDate = model.FromDate;
        //    param.ToDate = model.ToDate;
        //    param.StoreName = context.Store.FirstOrDefault(m => m.Id == model.StoreFor).Name;

        //    Session["IssueReceiveDetailsParam"] = param; 
        //    string sql = string.Format("exec ItemWiseIssueReceiveDetails '" + model.StoreId + "','" + model.StoreFor + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "' ");
        //    var items = context.Database.SqlQuery<IssueReceiveDetailsReport>(sql).ToList();
        //    if (items.Count == 0)
        //    {
        //        IssueReceiveDetailsReport report = new IssueReceiveDetailsReport();
        //        items.Add(report);
        //    }
        //    Session["IssueReceiveDetailsData"] = items;
        //    return Redirect("/CrystalReport/IssueReceiveDetailsRpt.aspx");
        //}

        //public ActionResult GetInventoryBalancheSummary(InventoryReportVModel model)
        //{
        //    Session["ReportName"] = "InventoryBalancheSummaryR";

        //    string sql = string.Format("exec InventoryBalancheSummary '" + model.StoreId + "'");
        //    var items = context.Database.SqlQuery<InventoryBalancheSummaryReport>(sql).ToList();
        //    if (items.Count == 0)
        //    {
        //        InventoryBalancheSummaryReport report = new InventoryBalancheSummaryReport();
        //        items.Add(report);
        //    }
        //    Session["InventoryBalancheSummaryData"] = items;
        //    return Redirect("/CrystalReport/InventoryBalancheSummaryRpt.aspx");
        //}
    }
}