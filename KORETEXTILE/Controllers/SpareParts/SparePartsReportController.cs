using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.ViewModel.Sales.Report;

namespace BEEERP.Controllers.SpareParts
{
    [ShowNotification]
    public class SparePartsReportController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: SparePartsReport
        public ActionResult SparePartsReportView()
        {
            ViewBag.SPDept = LoadComboBox.LoadAllSparePartsDepartment();
            ViewBag.Store = LoadComboBox.LoadAllFGStores();
            ViewBag.GroupId = LoadComboBox.LoadAllSPGroup();
            ViewBag.Item = LoadComboBox.LoadItem(null);
            ViewBag.SPType = LoadComboBox.LoadAllTypeId();

            ViewBag.Company = LoadComboBox.LoadAllCompanyID();
            ViewBag.Machine = LoadComboBox.LoadAllMachineID();
            return View();
        }
        public ActionResult TypeWiseSPInvSummary(InventoryReportVModel model)
        {
            Session["ReportName"] = "DeptWiseSPInvSummaryR";

            ReportParmPersister param = new ReportParmPersister();
            param.AsOnDate = model.AsOnDate;
            //param.FromDate = model.FromDate;
            //param.ToDate = model.ToDate;
            //param.ItemID = model.ItemID;
            //param.ItemGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
            //param.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
            //param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;

            Session["DeptWiseSPInvSummaryparam"] = param;

            string sql = string.Format("exec spTypeWiseSPInventorySummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.AsOnDate) + "'");
            var items = context.Database.SqlQuery<TypeWiseSPInvSumReport>(sql).ToList();
            if (items.Count == 0)
            {
                TypeWiseSPInvSumReport report = new TypeWiseSPInvSumReport();
                items.Add(report);
            }
            Session["DeptWiseSPInvSummaryData"] = items;
            return Redirect("/CrystalReport/DeptWiseSPInvSummaryRpt.aspx");
        }
        public ActionResult CompanyWiseSPInvSummary(InventoryReportVModel model)
        {
            Session["ReportName"] = "CompanyWiseSPInvSummaryR";

            ReportParmPersister param = new ReportParmPersister();
            param.AsOnDate = model.AsOnDate;
            //param.FromDate = model.FromDate;
            //param.ToDate = model.ToDate;
            //param.ItemID = model.ItemID;
            //param.ItemGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
            //param.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
            //param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;

            Session["CompWiseSPInvSummaryparam"] = param;

            string sql = string.Format("exec spCompanyWiseSPInventorySummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.AsOnDate) + "'");
            var items = context.Database.SqlQuery<CompWiseSPInvSumReport>(sql).ToList();
            if (items.Count == 0)
            {
                CompWiseSPInvSumReport report = new CompWiseSPInvSumReport();
                items.Add(report);
            }
            Session["CompWiseSPInvSummaryData"] = items;
            return Redirect("/CrystalReport/CompanyWiseSPInvRpt.aspx");
        }
        public ActionResult StoreWiseSPInvSummary(InventoryReportVModel model)
        {
            Session["ReportName"] = "StoreWiseSPInvSummaryR";

            ReportParmPersister param = new ReportParmPersister();
            param.AsOnDate = model.AsOnDate;
            //param.FromDate = model.FromDate;
            //param.ToDate = model.ToDate;
            //param.ItemID = model.ItemID;
            //param.ItemGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
            //param.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
            //param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;

            Session["StoreWiseSPInvSummaryparam"] = param;

            string sql = string.Format("exec spStoreWiseSPInventorySummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.AsOnDate) + "'");
            var items = context.Database.SqlQuery<StoreWiseSPInvSumReport>(sql).ToList();
            if (items.Count == 0)
            {
                StoreWiseSPInvSumReport report = new StoreWiseSPInvSumReport();
                items.Add(report);
            }
            Session["StoreWiseSPInvSummaryData"] = items;
            return Redirect("/CrystalReport/StoreWiseSPInvRpt.aspx");
        }

        public ActionResult DateWiseSPIssueSummary(InventoryReportVModel model)
        {
            Session["ReportName"] = "DateWiseSPIssueSummaryR";

            ReportParmPersister param = new ReportParmPersister();
            //param.AsOnDate = model.AsOnDate;
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;
            //param.ItemID = model.ItemID;
            //param.ItemGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
            //param.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
            //param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;

            Session["DateWiseSPIssueparam"] = param;

            string sql = string.Format("exec spDateWiseSPIssueSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "'");
            var items = context.Database.SqlQuery<DateWiseSPIssueReport>(sql).ToList();
            if (items.Count == 0)
            {
                DateWiseSPIssueReport report = new DateWiseSPIssueReport();
                items.Add(report);
            }
            Session["DateWiseSPIssueData"] = items;
            return Redirect("/CrystalReport/DateWiseSPIssuevRpt.aspx");
        }
    }
}