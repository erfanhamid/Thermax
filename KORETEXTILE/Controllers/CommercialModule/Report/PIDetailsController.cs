using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.ViewModel.CommercialVM.Report;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.Models.Database;

namespace BEEERP.Controllers.CommercialModule.Report
{
    [ShowNotification]
    public class PIDetailsController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: PIDetails
        public ActionResult ShowPIDetails()
        {
            ViewBag.lcType = LoadComboBox.LoadLCType();
            ViewBag.Supplier = LoadComboBox.LoadAllSupplier();
            //ViewBag.item = LoadComboBox.LoadItemBtType(null);
            return View();
        }
        public ActionResult GetItemByType(string type)
        {
            return Json(LoadComboBox.LoadItemBtType(type), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowImportPISummary(PIReportVModel model)
        {
            Session["ReportName"] = "ImportPISummaryR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["ImportPISummaryParam"] = param;

            string sql = string.Format("exec ImportPISummary  '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<ImportPISummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                ImportPISummaryReport report = new ImportPISummaryReport();
                items.Add(report);
            }
            Session["ImportPISummaryData"] = items;
            return Redirect("/CrystalReport/ImportPISummaryRpt.aspx");
        }
        public ActionResult ShowImportPIDetails(PIReportVModel model)
        {
            Session["ReportName"] = "ImportPIDetailsR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            if(model.SupplierId > 0)
            {
                param.SupplierName = context.Supplier.FirstOrDefault(x => x.Id == model.SupplierId).SupplierName;
            }
            else
            {
                param.SupplierName = "All Suppliers";
            }
            
            Session["ImportPIReportparam"] = param;

            string sql = string.Format("exec ImportPIDetails  '" + model.SupplierId + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<ImportPIDetailsReport>(sql).ToList();
            if (items.Count == 0)
            {
                ImportPIDetailsReport report = new ImportPIDetailsReport();
                items.Add(report);
            }
            Session["ImportPIReportData"] = items;
            return Redirect("/CrystalReport/ImportPIDetailsRpt.aspx");
        }
    }
}