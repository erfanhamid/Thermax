using BEEERP.CrystalReport;
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

namespace BEEERP.Controllers.Store
{
    [ShowNotification]
    public class StoreReportController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: StoreReport
        public ActionResult StoreReport()
        {
            ViewBag.SGroup = LoadComboBox.LoadAllSupplierGroup();
            ViewBag.Supplier = LoadComboBox.LoadSupplier(null);
            ViewBag.Group = LoadComboBox.LoadItemGroupRM();
            ViewBag.Item = LoadComboBox.LoadRmItem(null);
            ViewBag.Store = LoadComboBox.LoadAllRMStore();
            ViewBag.RMCustomer = LoadComboBox.LoadAllRMCustomer();
            return View();
        }
        public ActionResult MovingAverageBalance(ReportVModel model)
        {
            Session["ReportName"] = "StockMovingAverageBalance";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["MovingAvgBalanceParam"] = param;

            string sql = string.Format("exec StockMovingAverageBalance '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To)+"'");
            var items = context.Database.SqlQuery<StockMovingAverageBalanceReport>(sql).ToList();
            if (items.Count == 0)
            {
                StockMovingAverageBalanceReport report = new StockMovingAverageBalanceReport();
                items.Add(report);
            }
            Session["MovingAvgBalanceData"] = items;
            return Redirect("/CrystalReport/StockMovingAverageBalanceRpt.aspx");
        }
        public ActionResult MovingAverageConsumption(ReportVModel model)
        {
            Session["ReportName"] = "StockMovingAverageConsumption";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["MovingAvgConsumptionParam"] = param;

            string sql = string.Format("exec StockMovingAverageConsumption ");
            var items = context.Database.SqlQuery<StockMovingAverageConsumptionReport>(sql).ToList();
            if (items.Count == 0)
            {
                StockMovingAverageConsumptionReport report = new StockMovingAverageConsumptionReport();
                items.Add(report);
            }
            Session["MovingAvgConsumptionData"] = items;
            return Redirect("/CrystalReport/StoreMovingAverageConsumptionRpt.aspx");
        }
        public ActionResult RMConsumption(ReportVModel model)
        {
            Session["ReportName"] = "RMConsumption";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["RMConsumptionParam"] = param;

            string sql = string.Format("exec RMConsumption" +"'"+ DateTimeFormat.ConvertToDDMMYYYY(model.From) + "'"+","+"'"+ DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<RMConsumptionReport>(sql).ToList();
            if (items.Count == 0)
            {
                RMConsumptionReport report = new RMConsumptionReport();
                items.Add(report);
            }
            Session["RMConsumptionData"] = items;
            return Redirect("/CrystalReport/RMConsumptionRpt.aspx");
        }
        //  store wise rm balance summary
        public ActionResult RMStockBalanceStoreWise(ReportVModel model)
        {
            Session["ReportName"] = "RMStockBalanceStoreWise";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.AsOnDate;
            Session["RMStockBalanceStoreWiseParam"] = param;

            string sql = string.Format("exec RMStockBalanceStoreWise" + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "'" + "," + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.AsOnDate) + "'");
            var items = context.Database.SqlQuery<RMStockBalanceStoreWiseReport>(sql).ToList();
            if (items.Count == 0)
            {
                RMStockBalanceStoreWiseReport report = new RMStockBalanceStoreWiseReport();
                items.Add(report);
            }
            Session["RMStockBalanceStoreWiseData"] = items;
            return Redirect("/CrystalReport/RMStockBalanceStoreWiseRpt.aspx");
        }
        public ActionResult RMStockBalance(ReportVModel model)
        {
            Session["ReportName"] = "RMStockBalance";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.AsOnDate;
            Session["RMStockBalanceParam"] = param;

            string sql = string.Format("exec RMStockBalance" + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "'" + "," + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.AsOnDate) + "'");
            var items = context.Database.SqlQuery<RMStockBalanceReport>(sql).ToList();
            if (items.Count == 0)
            {
                RMStockBalanceReport report = new RMStockBalanceReport();
                items.Add(report);
            }
            Session["RMStockBalanceData"] = items;
            return Redirect("/CrystalReport/RMStockBalanceRpt.aspx");
        }
        public ActionResult RMWeightedStockBalance(ReportVModel model)
        {
            Session["ReportName"] = "RMWeightedStockBalance";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["RMWeightedStockBalanceParam"] = param;
            // old sp name RMWeightedStockBalance
            string sql = string.Format("exec spRMWeightedStockBalance" + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "'" + "," + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<RMWeightedStockBalanceReport>(sql).ToList();
            if (items.Count == 0)
            {
                RMWeightedStockBalanceReport report = new RMWeightedStockBalanceReport();
                items.Add(report);
            }
            Session["RMWeightedStockBalanceData"] = items;
            return Redirect("/CrystalReport/RMWeightedStockBalanceRpt.aspx");
        }
        // Show All GRN Record
        public ActionResult ShowAllGrnSummary(ReportVModel model)
        {
            Session["ReportName"] = "ShowAllGrnSummary";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["AllGrnSummaryParam"] = param;

            string sql = string.Format("exec spAllGRNRecord" + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "'" + "," + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<AllGRNRecordReport>(sql).ToList();
            if (items.Count == 0)
            {
                AllGRNRecordReport report = new AllGRNRecordReport();
                items.Add(report);
            }
            Session["AllGRNRecordReportData"] = items;
            return Redirect("/CrystalReport/AllGRNRecordRpt.aspx");
        }
        // Show Item Wise RM Receive Summary Report
        public ActionResult ItemWiseRMReceiveSummary(ReportVModel model)
        {
            Session["ReportName"] = "ItemWiseRMReceiveSummary";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["ItemWiseRMReceiveSummaryParam"] = param;

            string sql = string.Format("exec spItemWiseReceiveSummary" + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "'" + "," + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<ItemWiseReceiveSummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                ItemWiseReceiveSummaryReport report = new ItemWiseReceiveSummaryReport();
                items.Add(report);
            }
            Session["ItemWiseReceiveSummaryReportData"] = items;
            return Redirect("/CrystalReport/ItemWiseReceiveSummaryRpt.aspx");
        }
        // Show All IRMP Order by IRMP Date
        public ActionResult AllIRMPOrderByDate(ReportVModel model)
        {
            Session["ReportName"] = "AllIRMPOrderByDateR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["AllIRMPOrderByDateParam"] = param;

            string sql = string.Format("exec spShowAllIRMPByIRMPDate" + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "'" + "," + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<AllIRMPOrderByDateReport>(sql).ToList();
            if (items.Count == 0)
            {
                AllIRMPOrderByDateReport report = new AllIRMPOrderByDateReport();
                items.Add(report);
            }
            Session["AllIRMPOrderByDateData"] = items;
            return Redirect("/CrystalReport/AllIRMPOrderByIRMPDateRpt.aspx");
        }
        // Show All RM Sales Record
        public ActionResult AllRMSalesRecord(ReportVModel model)
        {
            Session["ReportName"] = "AllRMSalesRecordR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["AllRMSalesRecordParam"] = param;

            string sql = string.Format("exec spShowAllRMSalesRecord" + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "'" + "," + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<AllRMSalesRecordReport>(sql).ToList();
            if (items.Count == 0)
            {
                AllRMSalesRecordReport report = new AllRMSalesRecordReport();
                items.Add(report);
            }
            Session["AllRMSalesRecordData"] = items;
            return Redirect("/CrystalReport/AllRMSalesRecordRpt.aspx");
        }
        // Show Item Wise RM Sales Summary
        public ActionResult ItemWiseRMSalesSummary(ReportVModel model)
        {
            Session["ReportName"] = "ItemWiseRMSalesSummaryR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["ItemWiseRMSalesSummaryParam"] = param;

            string sql = string.Format("exec spItemWiseRMSalesSummary" + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "'" + "," + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<ItemWiseRMSalesSummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                ItemWiseRMSalesSummaryReport report = new ItemWiseRMSalesSummaryReport();
                items.Add(report);
            }
            Session["ItemWiseRMSalesSummaryData"] = items;
            return Redirect("/CrystalReport/ItemWiseRMSalesSummaryRpt.aspx");
        }
        // Show Customer Wise RM Sales Summary
        public ActionResult CustomerWiseRMSalesSummary(ReportVModel model)
        {
            Session["ReportName"] = "CustomerWiseRMSalesSummaryR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["CustomerWiseRMSalesSummaryParam"] = param;

            string sql = string.Format("exec spCustomerWiseRMSalesSummary" + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "'" + "," + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<CustomerWiseRMSalesSummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                CustomerWiseRMSalesSummaryReport report = new CustomerWiseRMSalesSummaryReport();
                items.Add(report);
            }
            Session["CustomerWiseRMSalesSummaryData"] = items;
            return Redirect("/CrystalReport/CustomerWiseRMSalesSummaryRpt.aspx");
        }
        // Show All IRMP Order by IRMP No
        public ActionResult AllIRMPOrderByIRMPNo(ReportVModel model)
        {
            Session["ReportName"] = "AllIRMPOrderByIRMPNoR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["AllIRMPOrderByIRMPNoParam"] = param;

            string sql = string.Format("exec spShowAllIRMPByIRMPNo" + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "'" + "," + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<ShowAllIRMPPByIRMPNoReport>(sql).ToList();
            if (items.Count == 0)
            {
                ShowAllIRMPPByIRMPNoReport report = new ShowAllIRMPPByIRMPNoReport();
                items.Add(report);
            }
            Session["AllIRMPOrderByIRMPNoData"] = items;
            return Redirect("/CrystalReport/AllIRMPOrderByIRMPNoRpt.aspx");
        }
        // Show Item Wise RM Issue Summary Report
        public ActionResult ItemWiseRMIssueSummary(ReportVModel model)
        {
            Session["ReportName"] = "ItemWiseRMIssueSummaryR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["ItemWiseRMIssueSummaryParam"] = param;

            string sql = string.Format("exec spItemWiseRMIssueSummary" + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "'" + "," + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<ItemWiseRMIssueSummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                ItemWiseRMIssueSummaryReport report = new ItemWiseRMIssueSummaryReport();
                items.Add(report);
            }
            Session["ItemWiseIssueSummaryReportData"] = items;
            return Redirect("/CrystalReport/ItemWiseRMIssueSummaryRpt.aspx");
        }
        // report no 14 - Item wise rm quantity summary
        public ActionResult GetStoreWiseItemQty(ReportVModel model)
        {
            //Session["ReportName"] = "Finish Goods Production Summary";
            ReportParmPersister param = new ReportParmPersister();
            param.AsOnDate = model.AsOnDate;
            //param.ToDate = model.ToDate;
            param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;
            param.RootAccountType = "RM";

            Session["storeParam"] = param;
            string sql = string.Format("exec StoreWiseItemQty '" + DateTimeFormat.ConvertToDDMMYYYY(model.AsOnDate) + "'" + "," + model.StoreId + "");
            var items = context.Database.SqlQuery<StoreWiseItemQtyReport>(sql).ToList();
            if (items.Count == 0)
            {
                StoreWiseItemQtyReport report = new StoreWiseItemQtyReport();
                items.Add(report);
            }
            Session["storeData"] = items;
            return Redirect("/CrystalReport/StoreWiseItemQtyRpt.aspx");
        }
        // Single Item RM Receive Detail
        public ActionResult SingleItemRMReceiveDetail2(ReportVModel model)
        {
            Session["ReportName"] = "SingleItemRMReceiveDetail2";

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            param.ItemID = model.ItemID;
            param.ItemGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
            param.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
            //param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;

            Session["SingleItemRMReceiveDetail2param"] = param;

            string sql = string.Format("exec spSingleItemRMReceiveDetail '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "', '" + model.ItemID + "' ");
            var items = context.Database.SqlQuery<SingleItemRMReceiveDetailsReport>(sql).ToList();
            if (items.Count == 0)
            {
                SingleItemRMReceiveDetailsReport report = new SingleItemRMReceiveDetailsReport();
                items.Add(report);
            }
            Session["SingleItemRMReceiveDetailsReportData"] = items;
            return Redirect("/CrystalReport/SingleItemRMReceiveDetailsReportRpt.aspx");
        }
        // Single Supplier GRN Summary
        public ActionResult SingleSupplierGRNSummary(ReportVModel model)
        {
            Session["ReportName"] = "SingleSupplierGRNSummaryR";

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            //param.ItemID = model.ItemID;
            //param.SupplierID= model.SupplierID
            //param.SupplierName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
            //param.SupplierGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
            //param.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
            //param.ItemGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
            //param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;

            Session["SingleSupplierGRNSummaryparam"] = param;

            string sql = string.Format("exec spSingleSupplierRMReceivesummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "', '" + model.SupplierID + "' ");
            var items = context.Database.SqlQuery<SingleSupplierGRNSummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                SingleSupplierGRNSummaryReport report = new SingleSupplierGRNSummaryReport();
                items.Add(report);
            }
            Session["SingleSupplierGRNSummaryData"] = items;
            return Redirect("/CrystalReport/SingleSupplierGRNSummaryRpt.aspx");
        }
        // Single Customer RM Sales Detail
        public ActionResult SingleCustomerRMSalesDetail(ReportVModel model)
        {
            Session["ReportName"] = "SingleCustomerRMSalesDetailR";

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            //param.ItemID = model.ItemID;
            //param.SupplierID= model.SupplierID
            //param.SupplierName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
            //param.SupplierGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
            //param.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
            //param.ItemGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
            //param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;

            Session["SingleCustomerRMSalesDetailparam"] = param;

            string sql = string.Format("exec spSingleCustomerRMSaleDetail '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "', '" + model.CustomerID + "' ");
            var items = context.Database.SqlQuery<SingleCustomerRMSalesDetailReport>(sql).ToList();
            if (items.Count == 0)
            {
                SingleCustomerRMSalesDetailReport report = new SingleCustomerRMSalesDetailReport();
                items.Add(report);
            }
            Session["SingleCustomerRMSalesDetailData"] = items;
            return Redirect("/CrystalReport/SingleCustomerRMSalesDetailRpt.aspx");
        }
        // single Item RM Issue Detail
        public ActionResult SingleItemRMIssueDetail(ReportVModel model)
        {
            Session["ReportName"] = "SingleItemRMIssueDetailR";

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            param.ItemID = model.ItemID;
            param.ItemGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
            param.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
            //param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;

            Session["SingleItemRMIssueDetailparam"] = param;

            string sql = string.Format("exec spSingleItemRMIssueDetail '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "', '" + model.ItemID + "' ");
            var items = context.Database.SqlQuery<SingleItemRMIssueDetailReport>(sql).ToList();
            if (items.Count == 0)
            {
                SingleItemRMIssueDetailReport report = new SingleItemRMIssueDetailReport();
                items.Add(report);
            }
            Session["SingleItemRMIssueDetailData"] = items;
            return Redirect("/CrystalReport/SingleItemRMIssueDetailRpt.aspx");
        }
        // single Item RM Sales Detail
        public ActionResult SingleItemRMSalesDetail(ReportVModel model)
        {
            Session["ReportName"] = "SingleItemRMSalesDetailR";

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            param.ItemID = model.ItemID;
            param.ItemGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
            param.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
            //param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;

            Session["SingleItemRMSalesDetailparam"] = param;

            string sql = string.Format("exec spSinlgeItemRMSalesDetail '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "', '" + model.ItemID + "' ");
            var items = context.Database.SqlQuery<SingleItemRMSalesDetailReport>(sql).ToList();
            if (items.Count == 0)
            {
                SingleItemRMSalesDetailReport report = new SingleItemRMSalesDetailReport();
                items.Add(report);
            }
            Session["SingleItemRMSalesDetailData"] = items;
            return Redirect("/CrystalReport/SingleItemRMSalesDetailRpt.aspx");
        }
        // Show RM Inventory Ledger
        public ActionResult RMInventoryLedger(ReportVModel model)
        {
            Session["ReportName"] = "RMInventoryLedgerR";

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            //param.ItemID = model.ItemID;
            param.ItemGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
            param.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
            param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;

            Session["RMInventoryLedgerparam"] = param;

            string sql = string.Format("exec RMInventoryItemLedger '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + model.ItemID + "','" + model.StoreId + "' , '" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<RMInventoryLedgerReport>(sql).ToList();
            if (items.Count == 0)
            {
                RMInventoryLedgerReport report = new RMInventoryLedgerReport();
                items.Add(report);
            }
            Session["RMInventoryLedgerData"] = items;
            return Redirect("/CrystalReport/RMInventoryLedgerRpt.aspx");
        }
        // Store Wise  RM Qty Balance Summary
        public ActionResult CombinedRMInventoryBalance(ReportVModel model)
        {
            Session["ReportName"] = "CombinedRMInventoryBalanceR";

            ReportParmPersister param = new ReportParmPersister();
            param.AsOnDate = model.AsOnDate;
            //param.ToDate = model.To;
            //param.ItemID = model.ItemID;
            //param.SupplierID= model.SupplierID
            //param.SupplierName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
            //param.SupplierGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
            //param.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
            //param.ItemGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
            //param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;

            Session["CombinedRMInventoryBalanceparam"] = param;

            string sql = string.Format("exec CombinedRMInventorySummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.AsOnDate) + "' ");
            var items = context.Database.SqlQuery<CombinedRMInventoryBalanceReport>(sql).ToList();
            if (items.Count == 0)
            {
                CombinedRMInventoryBalanceReport report = new CombinedRMInventoryBalanceReport();
                items.Add(report);
            }
            Session["CombinedRMInventoryBalanceData"] = items;
            return Redirect("/CrystalReport/SingleCustomerRMSalesDetailRpt.aspx");
        }
        // Show Combined RM Inventory Qty Balance Detail
        public ActionResult RMInentoryQtyBalancDetail(ReportVModel model)
        {
            Session["ReportName"] = "RMInentoryQtyBalancDetailR";

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            //param.ItemID = model.ItemID;
            //param.SupplierID= model.SupplierID
            //param.SupplierName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
            //param.SupplierGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
            //param.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
            //param.ItemGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
            //param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;

            Session["RMInentoryQtyBalancDetailparam"] = param;

            string sql = string.Format("exec spRMQtyBalanceDetail '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "' ");
            var items = context.Database.SqlQuery<RMInentoryQtyBalancDetailReport>(sql).ToList();
            if (items.Count == 0)
            {
                RMInentoryQtyBalancDetailReport report = new RMInentoryQtyBalancDetailReport();
                items.Add(report);
            }
            Session["RMInentoryQtyBalancDetailData"] = items;
            return Redirect("/CrystalReport/RMInentoryQtyBalancDetailRpt.aspx");
        }
        // Single Store RM Qty Balance Summary
        public ActionResult SingleStoreRMQtySummary(ReportVModel model)
        {
            Session["ReportName"] = "SingleStoreRMQtySummaryR";

            ReportParmPersister param = new ReportParmPersister();
            param.AsOnDate = model.AsOnDate;
           // param.ToDate = model.To;
            param.ItemID = model.StoreId;
            //param.ItemGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
            //param.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
            param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;

            Session["SingleStoreRMQtySummaryparam"] = param;

            string sql = string.Format("exec spStoreWiseQtyBalanceSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.AsOnDate) + "', " + model.StoreId + " ");
            var items = context.Database.SqlQuery<SinglestorermqtybalanceReport>(sql).ToList();
            if (items.Count == 0)
            {
                SinglestorermqtybalanceReport report = new SinglestorermqtybalanceReport();
                items.Add(report);
            }
            Session["SingleStoreRMQtySummaryData"] = items;
            return Redirect("/CrystalReport/SinglestorermqtybalanceRpt.aspx");
        }
        // Single Store RM Amount Balance Summary
        public ActionResult SingleStoreRMAmountSummary(ReportVModel model)
        {
            Session["ReportName"] = "SingleStoreRMAmountSummaryR";

            ReportParmPersister param = new ReportParmPersister();
            param.AsOnDate = model.AsOnDate;
            // param.ToDate = model.To;
            param.ItemID = model.StoreId;
            //param.ItemGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
            //param.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
            param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;

            Session["SingleStoreRMAmountSummaryparam"] = param;

            string sql = string.Format("exec spStoreWiseInventoryBalanceSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.AsOnDate) + "', " + model.StoreId + " ");
            var items = context.Database.SqlQuery<SinglestorermamountbalanceReport>(sql).ToList();
            if (items.Count == 0)
            {
                SinglestorermamountbalanceReport report = new SinglestorermamountbalanceReport();
                items.Add(report);
            }
            Session["SingleStoreRMAmountSummaryData"] = items;
            return Redirect("/CrystalReport/SingleStoreRMAmountSummaryRpt.aspx");
        }
        // Show All RM Stock Adjustment during period
        public ActionResult ShowAllRMStockAdjustment(ReportVModel model)
        {
            Session["ReportName"] = "AllRMStockAdjustmentR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["AllRMStockAdjustmentParam"] = param;

            string sql = string.Format("exec spShowAllRMStockAdjustment" + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "'" + "," + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<AllRMStockAdjustmentReport>(sql).ToList();
            if (items.Count == 0)
            {
                AllRMStockAdjustmentReport report = new AllRMStockAdjustmentReport();
                items.Add(report);
            }
            Session["AllRMStockAdjustmentData"] = items;
            return Redirect("/CrystalReport/AllRMStockAdjustmentRpt.aspx");
        }
    }
}