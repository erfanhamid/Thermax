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
    public class ProcurementReportController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: ProcurementReport
        public ActionResult ProcurementReport()
        {
            ViewBag.SupplierGroup = LoadComboBox.LoadAllSupplierGroup();
            ViewBag.Supplier = LoadComboBox.LoadSupplier(null);
            return View();
        }
        public ActionResult GetSupplierByGroupId(int sGroupId)
        {
            return Json(LoadComboBox.LoadSupplier(sGroupId), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Supplierwisewosummary(InventoryReportVModel model)
        {

            Session["ReportName"] = "SupplierwisewosummaryR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;
            // param.SupplierGroup = context.SupplierGroups.FirstOrDefault(x => x.SgroupId == model.SupplierGroup).SgroupName;
            Session["Supplierwisewosummaryparam"] = param;
            string sql = string.Format("exec spSupplierwisewosummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "'" + ",'" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "'");
            var items = context.Database.SqlQuery<SupplierwisewosummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                SupplierwisewosummaryReport report = new SupplierwisewosummaryReport();
                items.Add(report);
            }
            Session["SupplierwisewosummaryData"] = items;
            return Redirect("/CrystalReport/SupplierwisewosummaryRpt.aspx");
        }
        public ActionResult Supplierwisewodetails(InventoryReportVModel model)
        {

            Session["ReportName"] = "SupplierwisewodetailsR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;
            // param.SupplierGroup = context.SupplierGroups.FirstOrDefault(x => x.SgroupId == model.SupplierGroup).SgroupName;
            Session["Supplierwisewodetailsparam"] = param;
            string sql = string.Format("exec spSupplierwisewodetails '" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "'" + ",'" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "'");
            var items = context.Database.SqlQuery<SupplierwisewodetailsReport>(sql).ToList();
            if (items.Count == 0)
            {
                SupplierwisewodetailsReport report = new SupplierwisewodetailsReport();
                items.Add(report);
            }
            Session["SupplierwisewodetailsData"] = items;
            return Redirect("/CrystalReport/SupplierwisewodetailsRpt.aspx");
        }
        // Item wise work order summary
        public ActionResult Itemwisegwosummary(InventoryReportVModel model)
        {

            Session["ReportName"] = "ItemwisegwosummaryR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;
            // param.SupplierGroup = context.SupplierGroups.FirstOrDefault(x => x.SgroupId == model.SupplierGroup).SgroupName;
            Session["Itemwisegwosummaryparam"] = param;
            string sql = string.Format("exec spItemwisewosummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "'" + ",'" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "'");
            var items = context.Database.SqlQuery<ItemwisewosummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                ItemwisewosummaryReport report = new ItemwisewosummaryReport();
                items.Add(report);
            }
            Session["ItemwisegwosummaryData"] = items;
            return Redirect("/CrystalReport/ItemwisegwosummaryRpt.aspx");
        }
        // Item wise work order summary
        public ActionResult showAllWorkOrderByWONo(InventoryReportVModel model)
        {

            Session["ReportName"] = "AllWorkOrderByWONoR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;
            // param.SupplierGroup = context.SupplierGroups.FirstOrDefault(x => x.SgroupId == model.SupplierGroup).SgroupName;
            Session["AllWorkOrderByWONoparam"] = param;
            string sql = string.Format("exec spAllGoodsWorkOrder '" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "'" + ",'" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "'");
            var items = context.Database.SqlQuery<Allworkorderbywono>(sql).ToList();
            if (items.Count == 0)
            {
                Allworkorderbywono report = new Allworkorderbywono();
                items.Add(report);
            }
            Session["AllWorkOrderByWONoData"] = items;
            return Redirect("/CrystalReport/AllworkorderbywonoRpt.aspx");
        }
        // Yet to receive work order
        public ActionResult Yettoreceiveworkorder(InventoryReportVModel model)
        {

            Session["ReportName"] = "YettoreceiveworkorderR";
            //ReportParmPersister param = new ReportParmPersister();
            //param.FromDate = model.FromDate;
            //param.ToDate = model.ToDate;
            // param.SupplierGroup = context.SupplierGroups.FirstOrDefault(x => x.SgroupId == model.SupplierGroup).SgroupName;
            //Session["Yettoreceiveworkorderparam"] = param;
            string sql = string.Format("exec spYettoreceiveworkorder ");
            var items = context.Database.SqlQuery<Yettoreceiveworkorderreport>(sql).ToList();
            if (items.Count == 0)
            {
                Yettoreceiveworkorderreport report = new Yettoreceiveworkorderreport();
                items.Add(report);
            }
            Session["YettoreceiveworkorderData"] = items;
            return Redirect("/CrystalReport/YettoreceiveworkorderRpt.aspx");
        }
        public ActionResult GetSupplierItemPurchaseSummary(InventoryReportVModel model)
        {

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;
            // param.SupplierGroup = context.SupplierGroups.FirstOrDefault(x => x.SgroupId == model.SupplierGroup).SgroupName;
            Session["SupplierItemWisePurchaseSummaryparam"] = param;
            string sql = string.Format("exec SupplierItemWisePurchaseSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "'" + ",'" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "'," + model.SupplierGroup);
            var items = context.Database.SqlQuery<SupplierItemWisePurchaseSummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                SupplierItemWisePurchaseSummaryReport report = new SupplierItemWisePurchaseSummaryReport();
                items.Add(report);
            }
            Session["SupplierItemWisePurchaseSummaryData"] = items;
            return Redirect("/CrystalReport/SupplierItemWisePurchaseSummaryRpt.aspx");
        }
    }
}