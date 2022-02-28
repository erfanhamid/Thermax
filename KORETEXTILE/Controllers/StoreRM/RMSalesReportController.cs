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

namespace BEEERP.Controllers.StoreRM
{
    [ShowNotification]
    public class RMSalesReportController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: RMSalesReport
        public ActionResult RMSalesReport()
        {
            ViewBag.SGroup = LoadComboBox.LoadAllSupplierGroup();
            ViewBag.Supplier = LoadComboBox.LoadSupplier(null);
            ViewBag.Group = LoadComboBox.LoadItemGroupRM();
            ViewBag.Item = LoadComboBox.LoadRmItem(null);
            ViewBag.Store = LoadComboBox.LoadAllRMStore();
            ViewBag.RMCustomer = LoadComboBox.LoadAllRMCustomer();
            return View();
        }
        //1 Show All RM Sales Record
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
        //2 Show Item Wise RM Sales Summary
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
        //3 Show Customer Wise RM Sales Summary
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
         //5 Single Customer RM Sales Detail
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
        //4 single Item RM Sales Detail
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
       
        //public ActionResult RMInventoryLedger(ReportVModel model)
        //{
        //    Session["ReportName"] = "RMInventoryLedgerR";

        //    ReportParmPersister param = new ReportParmPersister();
        //    param.FromDate = model.From;
        //    param.ToDate = model.To;
        //    //param.ItemID = model.ItemID;
        //    param.ItemGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
        //    param.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
        //    param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;

        //    Session["RMInventoryLedgerparam"] = param;

        //    string sql = string.Format("exec RMInventoryItemLedger '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + model.ItemID + "','" + model.StoreId + "' , '" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
        //    var items = context.Database.SqlQuery<RMInventoryLedgerReport>(sql).ToList();
        //    if (items.Count == 0)
        //    {
        //        RMInventoryLedgerReport report = new RMInventoryLedgerReport();
        //        items.Add(report);
        //    }
        //    Session["RMInventoryLedgerData"] = items;
        //    return Redirect("/CrystalReport/RMInventoryLedgerRpt.aspx");
        //}
    }
}