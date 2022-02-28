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

namespace BEEERP.Controllers.Procurement.Report
{
    [ShowNotification]
    public class SupplierReportController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: SupplierReport
        public ActionResult SupplierReport()
        {
            ViewBag.SupplierGroup = LoadComboBox.LoadAllSupplierGroup();
            ViewBag.Supplier = LoadComboBox.LoadSupplier(null);
            return View();
           
        }
        public ActionResult GetSupplierByGroupId(int sGroupId)
        {
            return Json(LoadComboBox.LoadSupplier(sGroupId), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSupplierContactsummary(InventoryReportVModel model)
        {
            string sql = string.Format("exec spSupplierContactSummary");
            var items = context.Database.SqlQuery<SupplierContactSummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                SupplierContactSummaryReport report = new SupplierContactSummaryReport();
                items.Add(report);
            }
            Session["SupplierContactSummaryData"] = items;
            return Redirect("/CrystalReport/SupplierContactSummaryRpt.aspx");
        }
        public ActionResult ShowSupplierLedger(InventoryReportVModel model)
        {
            Session["ReportName"] = "SupplierLedgerR";

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;
            param.ID = model.SupplierID;
            param.SupplierName = context.Supplier.FirstOrDefault(m => m.Id == model.SupplierID).SupplierName;
            var groupid = context.Supplier.FirstOrDefault(x => x.Id == model.SupplierID).GroupID;
            param.SupplierGroup = context.SupplierGroups.FirstOrDefault(x => x.SgroupId == groupid).SgroupName;

            Session["SupplierLedgerParam"] = param;
            string sql = string.Format("exec SupplierLedger '" + model.SupplierID + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "' ");
            var items = context.Database.SqlQuery<SupplierLedgerReport>(sql).ToList();
            if (items.Count == 0)
            {
                SupplierLedgerReport report = new SupplierLedgerReport();
                items.Add(report);
            }
            Session["SupplierLedgerData"] = items;
            return Redirect("/CrystalReport/SupplierLedgerRpt.aspx");
        }

        public ActionResult GetSupplierBalanceDetails(InventoryReportVModel model)
        {
        
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;
            param.SupplierGroup = context.SupplierGroups.FirstOrDefault(x => x.SgroupId == model.SupplierGroup).SgroupName;
            Session["Supbalanceparam"] = param;
            string sql = string.Format("exec SupplierBalanceDetails '" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "'" + ",'" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "',"+model.SupplierGroup);
            var items = context.Database.SqlQuery<SupplierBalanceDetailsReport>(sql).ToList();
            if (items.Count == 0)
            {
                SupplierBalanceDetailsReport report = new SupplierBalanceDetailsReport();
                items.Add(report);
            }
            Session["SupbalanceData"] = items;
            return Redirect("/CrystalReport/SupplierBalanceDetailsRpt.aspx");
        }
        public ActionResult GetSupplierBalanceSummary(InventoryReportVModel model)
        {

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;
            param.SupplierGroup = context.SupplierGroups.FirstOrDefault(x => x.SgroupId == model.SupplierGroup).SgroupName;
            Session["SupbalanceSumparam"] = param;
            string sql = string.Format("exec SupplierBalanceDetails '" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "'" + ",'" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "'," + model.SupplierGroup);
            var items = context.Database.SqlQuery<SupplierBalanceDetailsReport>(sql).ToList();
            if (items.Count == 0)
            {
                SupplierBalanceDetailsReport report = new SupplierBalanceDetailsReport();
                items.Add(report);
            }
            Session["SupbalanceSumData"] = items;
            return Redirect("/CrystalReport/SupplierBalanceSummaryRpt.aspx");
        }

        // Single Group Supplier Aging Summary
        public ActionResult SingleGroupSupplierAging(InventoryReportVModel model)
        {

            Session["ReportName"] = "SingleGroupSupplierAgingR";

            ReportParmPersister param = new ReportParmPersister();
            //param.FromDate = model.FromDate;
            //param.ToDate = model.ToDate;
            param.AsOnDate = model.AsOnDate;
            //param.SupplierGroupID = model.SupplierGroup;
            //param.SupplierGroup = context.SupplierGroups.FirstOrDefault(x => x.SgroupId == model.SupplierGroup).SgroupName;
            Session["SGSAparam"] = param;
            string sql = string.Format("exec spSingleGroupSupplierAging '" + DateTimeFormat.ConvertToDDMMYYYY(model.AsOnDate) + "'" + ",'" + model.SupplierGroup + "'");
            var items = context.Database.SqlQuery<SingleGroupSupplierAgingReport>(sql).ToList();
            if (items.Count == 0)
            {
                SingleGroupSupplierAgingReport report = new SingleGroupSupplierAgingReport();
                items.Add(report);
            }
            Session["SGSAData"] = items;
            return Redirect("/CrystalReport/SingleGroupSupplierAgingRpt.aspx");
        }


        // Single Supplier Bill Aging Summary
        public ActionResult SingleSupplierBillAging(InventoryReportVModel model)
        {

            Session["ReportName"] = "SingleSupplierBillAgingR";

            ReportParmPersister param = new ReportParmPersister();
            //param.FromDate = model.FromDate;
            //param.ToDate = model.ToDate;
            param.AsOnDate = model.AsOnDate;
            //param.SupplierGroupID = model.SupplierGroup;
            param.SupplierGroup = context.SupplierGroups.FirstOrDefault(x => x.SgroupId == model.SupplierGroup).SgroupName;
            Session["SSBAparam"] = param;
            string sql = string.Format("exec spSingleSupplierBillAging '" + DateTimeFormat.ConvertToDDMMYYYY(model.AsOnDate) + "'" + ",'" + model.SupplierID + "'");
            var items = context.Database.SqlQuery<SingleSupplierBillAgingReport>(sql).ToList();
            if (items.Count == 0)
            {
                SingleSupplierBillAgingReport report = new SingleSupplierBillAgingReport();
                items.Add(report);
            }
            Session["SSBAData"] = items;
            return Redirect("/CrystalReport/SingleSupplierBillAgingRpt.aspx");
        }
        public ActionResult DateWiseSPVSummary(InventoryReportVModel model)
        {
            Session["ReportName"] = "DateWiseSPVSummaryR";

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;
            //param.SupplierGroup = context.SupplierGroups.FirstOrDefault(x => x.SgroupId == model.SupplierGroup).SgroupName;
            Session["dwsspvparam"] = param;
            string sql = string.Format("exec spDateWiseSPVSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "'" + ",'" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "'");
            var items = context.Database.SqlQuery<DateWiseSPVSummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                DateWiseSPVSummaryReport report = new DateWiseSPVSummaryReport();
                items.Add(report);
            }

            Session["dwspvsData"] = items;
            return Redirect("/CrystalReport/DateWiseSPVSummaryRpt.aspx");
        }

        public ActionResult BillWiseSPVDetail(InventoryReportVModel model)
        {
            Session["ReportName"] = "BillWiseSPVDetailR";

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;
            //param.SupplierGroup = context.SupplierGroups.FirstOrDefault(x => x.SgroupId == model.SupplierGroup).SgroupName;
            Session["bwspvdparam"] = param;
            string sql = string.Format("exec spBillWiseSPVDetail '" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "'" + ",'" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "'");
            var items = context.Database.SqlQuery<BillWiseSPVDetailReport>(sql).ToList();
            if (items.Count == 0)
            {
                BillWiseSPVDetailReport report = new BillWiseSPVDetailReport();
                items.Add(report);
            }
            Session["bwspvdData"] = items;
            return Redirect("/CrystalReport/BillWiseSPVDetailRpt.aspx");
        }


        public ActionResult UnadjustedSupplierAdvance(InventoryReportVModel model)
        {
            Session["ReportName"] = "UnadjustedSupplierAdvanceR";

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;
            //param.SupplierGroup = context.SupplierGroups.FirstOrDefault(x => x.SgroupId == model.SupplierGroup).SgroupName;
            Session["usaparam"] = param;

            string sql = string.Format("exec spSupplierWiseUnadjustedSPV '" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "'" + ",'" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "'");
            var items = context.Database.SqlQuery<UnadjustedSupplierAdvanceReport>(sql).ToList();
            if (items.Count == 0)
            {
                UnadjustedSupplierAdvanceReport report = new UnadjustedSupplierAdvanceReport();
                items.Add(report);
            }

            Session["usaData"] = items;
            return Redirect("/CrystalReport/UnadjustedSupplierAdvanceRpt.aspx");
        }

        // Date Wise Supplier Advance Summary
        public ActionResult DateWisSADASummaryNew(InventoryReportVModel model)
        {
            Session["ReportName"] = "DateWisSADASummaryR";

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;
            //param.SupplierGroup = context.SupplierGroups.FirstOrDefault(x => x.SgroupId == model.SupplierGroup).SgroupName;
            Session["SADASummaryparam"] = param;

            string sql = string.Format("exec spDateWiseSADASummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "'" + ",'" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "'");
            //string sql = string.Format("exec spTestSupplier");
            var items = context.Database.SqlQuery<DateWiseSADASummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                DateWiseSADASummaryReport report = new DateWiseSADASummaryReport();
                items.Add(report);
            }
            Session["SADASummaryData"] = items;
            return Redirect("/CrystalReport/DateWisSADASummaryNewRpt.aspx");
        }
        //Bill Wise SADA Summary
        public ActionResult BillWisSADASummary(InventoryReportVModel model)
        {
            Session["ReportName"] = "BillWisSADASummaryR";

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;
            //param.SupplierGroup = context.SupplierGroups.FirstOrDefault(x => x.SgroupId == model.SupplierGroup).SgroupName;
            Session["BillWisSADASummaryparam"] = param;

            string sql = string.Format("exec spBillWiseSADASummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "'" + ",'" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "'");
            var items = context.Database.SqlQuery<BillWiseSADASummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                BillWiseSADASummaryReport report = new BillWiseSADASummaryReport();
                items.Add(report);
            }

            Session["BillWisSADASummaryData"] = items;
            return Redirect("/CrystalReport/BillWisSADASummaryRpt.aspx");
        }
        public ActionResult SingleSupplierBillAgingNew(InventoryReportVModel model)
        {

            Session["ReportName"] = "SingleSupplierBillAgingNewR";

            ReportParmPersister param = new ReportParmPersister();
            //param.FromDate = model.FromDate;
            //param.ToDate = model.ToDate;
            param.AsOnDate = model.AsOnDate;
            //param.SupplierGroupID = model.SupplierGroup;
            param.SupplierGroup = context.SupplierGroups.FirstOrDefault(x => x.SgroupId == model.SupplierGroup).SgroupName;
            Session["SSBANewparam"] = param;
            string sql = string.Format("exec spSingleSupplierBillAgingNew '" + DateTimeFormat.ConvertToDDMMYYYY(model.AsOnDate) + "','" + model.SupplierID + "'");
            var items = context.Database.SqlQuery<SingleSupplierBillAgingNewReport>(sql).ToList();
            if (items.Count == 0)
            {
                SingleSupplierBillAgingNewReport report = new SingleSupplierBillAgingNewReport();
                items.Add(report);
            }
            Session["SSBANewData"] = items;
            return Redirect("/CrystalReport/SingleSupplierBillAgingNewRpt.aspx");
        }

        public ActionResult SingleGroupSupplierBillAging(InventoryReportVModel model)
        {

            Session["ReportName"] = "SingleGroupSupplierBillAgingR";

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;
            //param.AsOnDate = model.AsOnDate;
            param.SupplierGroupID = model.SupplierGroup;
            param.SupplierGroup = context.SupplierGroups.FirstOrDefault(x => x.SgroupId == model.SupplierGroup).SgroupName;
            Session["SGSBAparam"] = param;
            string sql = string.Format("exec spSingleGroupSupplierAgingUpdate '" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "','" + model.SupplierGroup + "'");
            var items = context.Database.SqlQuery<SingleGroupSupplierBillAgingReport>(sql).ToList();
            if (items.Count == 0)
            {
                SingleGroupSupplierBillAgingReport report = new SingleGroupSupplierBillAgingReport();
                items.Add(report);
            }
            Session["SGSBAData"] = items;
            return Redirect("/CrystalReport/SingleGroupSupplierBillAgingRpt.aspx");
        }

    }
}