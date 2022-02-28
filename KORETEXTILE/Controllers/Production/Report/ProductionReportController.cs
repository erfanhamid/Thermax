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

namespace BEEERP.Controllers.Production.Report
{
    [ShowNotification]
    public class ProductionReportController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: ProductionReport
        public ActionResult ShowProductionReport()
        {
            ViewBag.LoadBatch = LoadComboBox.LoadBatch();
            ViewBag.LoadFGStore = LoadComboBox.LoadAllFGStore();
            ViewBag.LoadStore = LoadComboBox.LoadAllRMStore();
            //ViewBag.LoadStore = LoadComboBox.LoadAllStore();
            //ViewBag.acType = LoadComboBox.LoadAllRootAcType();
            ViewBag.LoadBatch = LoadComboBox.LoadBatch();
            ViewBag.Group = LoadComboBox.LoadItemGroupRM();
            ViewBag.FGItem = LoadComboBox.LoadRmItem(null);
            // for FG Group and Item
            ViewBag.GroupId = LoadComboBox.LoadItemGroup();
            ViewBag.Item = LoadComboBox.LoadItem(null);
            return View();
        }
        public ActionResult GetBatchWiseProduction(int BatchId)
        {
            Session["ReportName"] = "BatchWiseProductionR";

            string sql = string.Format("exec BatchWiseProduction '" + BatchId + "'");
            var items = context.Database.SqlQuery<BatchWiseProductionReport>(sql).ToList();
            if (items.Count == 0)
            {
                BatchWiseProductionReport report = new BatchWiseProductionReport();
                items.Add(report);
            }
            Session["BatchWiseProductionReportData"] = items;
            return Redirect("/CrystalReport/BatchWiseProductionRpt.aspx");
        }

        public ActionResult GetRMCSummary(InventoryReportVModel model)
        {
            Session["ReportName"] = "RMC Summary";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;
            Session["RMCParam"] = param;
            string sql = string.Format("exec RMCSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "'" + ",'" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "'");
            var items = context.Database.SqlQuery<RMCSummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                RMCSummaryReport report = new RMCSummaryReport();
                items.Add(report);
            }
            Session["RMCData"] = items;
            return Redirect("/CrystalReport/RMCSummaryRpt.aspx");
        }

        public ActionResult GetFGPSummary(InventoryReportVModel model)
        {
            Session["ReportName"] = "Finish Goods Production Summary";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;
            Session["FGPParam"] = param;
            string sql = string.Format("exec FGPSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "'" + ",'" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "'");
            var items = context.Database.SqlQuery<FGPSummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                FGPSummaryReport report = new FGPSummaryReport();
                items.Add(report);
            }
            Session["FGPData"] = items;
            return Redirect("/CrystalReport/FGPSummaryRpt.aspx");
        }
        // Show Item Wise RM Consumption Summary
        public ActionResult ItemWiseRMCSummary(InventoryReportVModel model)
        {
            Session["ReportName"] = "ItemWiseRMCSummaryR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;
                            
            Session["ItemWiseRMCSummaryParam"] = param;
            
            string sql = string.Format("exec spItemWiseRMCSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "'" + ",'" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "'");
            var items = context.Database.SqlQuery<ItemWiseRMCSummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                ItemWiseRMCSummaryReport report = new ItemWiseRMCSummaryReport();
                items.Add(report);
            }
            Session["ItemWiseRMCSummaryData"] = items;
            return Redirect("/CrystalReport/ItemWiseRMCSummaryRpt.aspx");
        }
        // Single Item RM Consumption Detail
        public ActionResult SingleItemRMConsumptiondetail(InventoryReportVModel model)
        {
            Session["ReportName"] = "SingleItemRMConsumptiondetailR";

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;
            //param.ItemID = model.ItemID;
            //param.ItemGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
            //param.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
            //param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;

            Session["SingleItemRMConsumptiondetailparam"] = param;

            string sql = string.Format("exec spSingleItemRMCDetail '" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "', '" + model.ItemID + "' ");
            var items = context.Database.SqlQuery<SingleItemRMConsumptiondetailReport>(sql).ToList();
            if (items.Count == 0)
            {
                SingleItemRMConsumptiondetailReport report = new SingleItemRMConsumptiondetailReport();
                items.Add(report);
            }
            Session["SingleItemRMConsumptiondetailData"] = items;
            return Redirect("/CrystalReport/SingleItemRMConsumptiondetailRpt.aspx");
        }
        // Single Batch RM Consumption Detail
        public ActionResult SingleBatchRMCDetail(InventoryReportVModel model)
        {
            Session["ReportName"] = "SingleBatchRMCDetailR";

            //ReportParmPersister param = new ReportParmPersister();
            //param.FromDate = model.FromDate;
            //param.ToDate = model.ToDate;
            //param.ItemID = model.ItemID;
            //param.ItemGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
            //param.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
            //param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;

            //Session["SingleBatchRMCDetailparam"] = param;

            // olde code : string sql = string.Format("exec spSingleBatchRMCDetail '" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "', '" + model.BatchID + "' ");
            string sql = string.Format("exec spSingleBatchRMCDetail '" + model.BatchID + "' ");
            var items = context.Database.SqlQuery<SingleBatchRMCDetailReport>(sql).ToList();
            if (items.Count == 0)
            {
                SingleBatchRMCDetailReport report = new SingleBatchRMCDetailReport();
                items.Add(report);
            }
            Session["SingleBatchRMCDetailData"] = items;
            return Redirect("/CrystalReport/SingleBatchRMCDetailRpt.aspx");
        }
        // Show Item Wise FG Production Summary
        public ActionResult ItemwiseFGPSummary(InventoryReportVModel model)
        {
            Session["ReportName"] = "ItemwiseFGPSummaryR";

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;

            Session["ItemwiseFGPSummaryParam"] = param;

            string sql = string.Format("exec spItemWiseFGPSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "'" + ",'" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "'");
            var items = context.Database.SqlQuery<ItemwiseFGPSummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                ItemwiseFGPSummaryReport report = new ItemwiseFGPSummaryReport();
                items.Add(report);
            }
            Session["ItemwiseFGPSummaryData"] = items;
            return Redirect("/CrystalReport/ItemwiseFGPSummaryRpt.aspx");
        }
        // Show Batch and Item Wise FG Production Summary
        public ActionResult BatchitemwiseFGPSummary(InventoryReportVModel model)
        {
            Session["ReportName"] = "BatchitemwiseFGPSummary";

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;

            Session["BatchitemwiseFGPSummaryParam"] = param;

            string sql = string.Format("exec spBatchItemWiseFGPSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "'" + ",'" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "'");
            var items = context.Database.SqlQuery<BatchitemwiseFGPSummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                BatchitemwiseFGPSummaryReport report = new BatchitemwiseFGPSummaryReport();
                items.Add(report);
            }
            Session["BatchitemwiseFGPSummaryData"] = items;
            return Redirect("/CrystalReport/BatchitemwiseFGPSummaryRpt.aspx");
        }
        //// Show Batch Wise FG Production Summary
        //public ActionResult BatchwiseFGPSummary(InventoryReportVModel model)
        //{
        //    Session["ReportName"] = "BatchwiseFGPSummaryR";

        //    ReportParmPersister param = new ReportParmPersister();
        //    param.FromDate = model.FromDate;
        //    param.ToDate = model.ToDate;

        //    Session["BatchwiseFGPSummaryParam"] = param;

        //    string sql = string.Format("exec spBatchIwiseFGPSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "'" + ",'" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "'");
        //    var items = context.Database.SqlQuery<BatchwiseFGPSummaryReport>(sql).ToList();
        //    if (items.Count == 0)
        //    {
        //        BatchwiseFGPSummaryReport report = new BatchwiseFGPSummaryReport();
        //        items.Add(report);
        //    }
        //    Session["BatchwiseFGPSummaryData"] = items;
        //    return Redirect("/CrystalReport/BatchwiseFGPSummaryRpt.aspx");
        //}
        // Show Batch and Item Wise FG Production Summary
        public ActionResult BatchwiseFGPSummary(InventoryReportVModel model)
        {
            Session["ReportName"] = "BatchwiseFGPSummaryR";

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;

            Session["BatchwiseFGPSummaryParam"] = param;

            string sql = string.Format("exec spBatchIwiseFGPSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "'" + ",'" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "'");
            var items = context.Database.SqlQuery<BatchwiseFGPSummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                BatchwiseFGPSummaryReport report = new BatchwiseFGPSummaryReport();
                items.Add(report);
            }
            Session["BatchwiseFGPSummaryData"] = items;
            return Redirect("/CrystalReport/NewBatchwiseFGPSummaryRpt.aspx");
        }
        // Single Item FG Production Detail
        public ActionResult SingleItemFGPDetail(InventoryReportVModel model)
        {
            Session["ReportName"] = "SingleItemFGPDetailR";

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;
            //param.ItemID = model.ItemID;
            //param.ItemGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
            //param.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
            //param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;

            Session["SingleItemFGPDetailparam"] = param;

            string sql = string.Format("exec spSingleItemFGPDetail '" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "', '" + model.FGItemID + "' ");
            var items = context.Database.SqlQuery<SingleItemFGPDetailReport>(sql).ToList();
            if (items.Count == 0)
            {
                SingleItemFGPDetailReport report = new SingleItemFGPDetailReport();
                items.Add(report);
            }
            Session["SingleItemFGPDetailData"] = items;
            return Redirect("/CrystalReport/SingleItemFGPDetailRpt.aspx");
        }
        // Single Batch FG Production Detail
        public ActionResult SingleBatchFGPDetail(InventoryReportVModel model)
        {
            Session["ReportName"] = "SingleBatchFGPDetailR";

            //ReportParmPersister param = new ReportParmPersister();
            //param.FromDate = model.FromDate;
            //param.ToDate = model.ToDate;
            //param.ItemID = model.ItemID;
            //param.ItemGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
            //param.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
            //param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;

            //Session["SingleBatchRMCDetailparam"] = param;

            // olde code : string sql = string.Format("exec spSingleBatchRMCDetail '" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "', '" + model.BatchID + "' ");
            string sql = string.Format("exec spSingleBatchFGPDetail '" + model.BatchID + "' ");
            var items = context.Database.SqlQuery<SingleBatchFGPDetailReport>(sql).ToList();
            if (items.Count == 0)
            {
                SingleBatchFGPDetailReport report = new SingleBatchFGPDetailReport();
                items.Add(report);
            }
            Session["SingleBatchFGPDetailData"] = items;
            return Redirect("/CrystalReport/SingleBatchFGPDetailRpt.aspx");
        }
        // Single Store RM Balance Summary
        public ActionResult SingleStoreRMBalanceSummary(InventoryReportVModel model)
        {
            Session["ReportName"] = "SingleStoreRMBalanceSummaryR";

            ReportParmPersister param = new ReportParmPersister();

            //param.FromDate = model.FromDate;
            //param.ToDate = model.ToDate;
            param.AsOnDate = model.AsOnDate; 
            //param.ItemID = model.ItemID;
            //param.ItemGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
            //param.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
            //param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;

            Session["SingleStoreRMBalanceparam"] = param;
            

            string sql = string.Format("exec spSingleStoreRMBalanceSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.AsOnDate) + "','" + model.StoreId + "' ");
            var items = context.Database.SqlQuery<SingleStoreRMBSummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                SingleStoreRMBSummaryReport report = new SingleStoreRMBSummaryReport();
                items.Add(report);
            }
            Session["SingleStoreRMBalanceSummaryData"] = items;
            return Redirect("/CrystalReport/SingleStoreRMBalanceSummaryRpt.aspx");
        }
        // Consolidated RM Balance Summary
        public ActionResult ConsolidatedRMBalanceSummary(InventoryReportVModel model)
        {
            Session["ReportName"] = "ConsolidatedRMBalanceSummaryR";

            ReportParmPersister param = new ReportParmPersister();

            //param.FromDate = model.FromDate;
            //param.ToDate = model.ToDate;
            param.AsOnDate = model.AsOnDate;
            //param.ItemID = model.ItemID;
            //param.ItemGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
            //param.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
            //param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;

            Session["ConsolidatedRMBSummaryparam"] = param;


            string sql = string.Format("exec spConsolidatedRMBalanceSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.AsOnDate) + "'");
            var items = context.Database.SqlQuery<ConsolidatedRMBSummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                ConsolidatedRMBSummaryReport report = new ConsolidatedRMBSummaryReport();
                items.Add(report);
            }
            Session["ConsolidatedRMBSummaryData"] = items;
            return Redirect("/CrystalReport/ConsolidatedRMBalanceSummaryRpt.aspx");
        }
        // Single Store FG Balance Summary
        public ActionResult SingleStoreFGBalancesummary(InventoryReportVModel model)
        {
            Session["ReportName"] = "SingleStoreFGBalancesummaryR";

            ReportParmPersister param = new ReportParmPersister();

            //param.FromDate = model.FromDate;
            //param.ToDate = model.ToDate;
            param.AsOnDate = model.AsOnDate;
            //param.ItemID = model.ItemID;
            //param.ItemGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
            //param.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
            //param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;

            Session["SingleStoreFGBsummaryparam"] = param;


            string sql = string.Format("exec spSingleStoreFGBalanceSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.AsOnDate) + "','" + model.FGStoreId + "' ");
            var items = context.Database.SqlQuery<SingleStoreFGBsummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                SingleStoreFGBsummaryReport report = new SingleStoreFGBsummaryReport();
                items.Add(report);
            }
            Session["SingleStoreFGBsummaryData"] = items;
            return Redirect("/CrystalReport/SingleStoreFGBalancesummaryRpt.aspx");
        }
        // Consolidated FG Balance Summary
        public ActionResult ConsolidatedFGBalancesummary(InventoryReportVModel model)
        {
            Session["ReportName"] = "ConsolidatedFGBalancesummaryR";

            ReportParmPersister param = new ReportParmPersister();

            //param.FromDate = model.FromDate;
            //param.ToDate = model.ToDate;
            param.AsOnDate = model.AsOnDate;
            //param.ItemID = model.ItemID;
            //param.ItemGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
            //param.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
            //param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;

            Session["ConsolidatedFGBsummaryparam"] = param;


            string sql = string.Format("exec spConsolidatedFGBalanceSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.AsOnDate) + "'");
            var items = context.Database.SqlQuery<ConsolidatedFGBsummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                ConsolidatedFGBsummaryReport report = new ConsolidatedFGBsummaryReport();
                items.Add(report);
            }
            Session["ConsolidatedFGBsummaryData"] = items;
            return Redirect("/CrystalReport/ConsolidatedFGBalancesummaryRpt.aspx");
        }
    }
}