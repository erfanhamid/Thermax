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

namespace BEEERP.Controllers.CommercialModule.Report
{
    [ShowNotification]
    public class LCReportController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: LCReport
        public ActionResult LCReport()
        {
            ViewBag.SupplierGroup = LoadComboBox.LoadAllSupplierGroup();
            ViewBag.Supplier = LoadComboBox.LoadSupplier(null);
            //ViewBag.Supplier = LoadComboBox.LoadAllSupplier();
            ViewBag.ILC = LoadComboBox.LoadAllILC();
            ViewBag.Account = LoadComboBox.LoadAllDebitAccount();
            ViewBag.SubLedger = LoadComboBox.LoadAllSubLedgerAccount(null);
            return View();
        }

        public ActionResult GetILCById(int Id)
        {
            var ilc = context.ImportLC.FirstOrDefault(x => x.ILCId == Id);
            if (ilc != null)
            {
                return Json(new { iLCNo = ilc.ILCNO }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ShowImportLCSummary(ReportVModel model)
        {
            Session["ReportName"] = "ImportLCSummaryR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["ImportLCSummaryParam"] = param;

            string sql = string.Format("exec ImportLCSummary  '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<ImportLCSummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                ImportLCSummaryReport report = new ImportLCSummaryReport();
                items.Add(report);
            }
            Session["ImportLCSummaryData"] = items;
            return Redirect("/CrystalReport/ImportLCSummaryRpt.aspx");
        }
        public ActionResult ShowImportLCReceiveDetails(ReportVModel model)
        {
            ReportParmPersister param = new ReportParmPersister();
            if (model.ILCID <= 0)
            {  
                model.ILCID = 0;
            }
            string sql = string.Format("exec ImportLCReceiveDetails " + model.ILCID + ",'" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<ImportLCReceiveDetailsReport>(sql).ToList();
            if (items.Count == 0)
            {
                ImportLCReceiveDetailsReport report = new ImportLCReceiveDetailsReport();
                if (model.ILCID > 0)
                {
                    var importlc = context.ImportLC.FirstOrDefault(x => x.ILCId == model.ILCID);
                    report.LCId = importlc.ILCId;
                    report.ILCNO = importlc.ILCNO;
                }
                items.Add(report);
            }
            param.Description = "From " + model.From.Date.ToString("dd-MM-yyyy") + " to " + model.To.Date.ToString("dd-MM-yyyy");
            Session["ImportLCReceiveDetailsParam"] = param;
            Session["ImportLCReceiveDetailsData"] = items;
            return Redirect("/CrystalReport/ImportLCReceiveDetailsRpt.aspx");
        }

        public ActionResult ShowImportLCLedger(ReportVModel model)
        {
            var ilc = context.ImportLC.FirstOrDefault(x => x.ILCId == model.ILCID);
            Session["ReportName"] = "ImportLCLedgerR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            param.ILCId = model.ILCID;

            if (ilc != null)
            {
                param.ILCNo = ilc.ILCNO;
                //param.LCType = context.ImportProformaInvoices.FirstOrDefault(x => x.IPIId == ilc.IPIId).LCType;
                param.SupplierName = context.Supplier.FirstOrDefault(x => x.Id == ilc.SupplierId).SupplierName;
            }
            else
            {
                param.ILCNo = "";
                param.LCType = "";
                param.SupplierName = "";
            }

            Session["ImportLCLedgerParam"] = param;

            string sql = string.Format("exec ImportLCLedger '" + model.ILCID + "',  '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<ImportLCLedgerReport>(sql).ToList();
            if (items.Count == 0)
            {
                ImportLCLedgerReport report = new ImportLCLedgerReport();
                items.Add(report);
            }
            Session["ImportLCLedgerData"] = items;
            return Redirect("/CrystalReport/ImportLCLedgerRpt.aspx");
        }

        public ActionResult ShowLCCostingDetails(ReportVModel model)
        {
            var ilc = context.ImportLC.FirstOrDefault(x => x.ILCId == model.ILCID);
            Session["ReportName"] = "LCCostingDetailsR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            param.ILCId = model.ILCID;

            if (ilc != null)
            {
                param.ILCNo = ilc.ILCNO;
                param.IALCNo = ilc.IALCNO;
            }
            else
            {
                param.ILCNo = "";
                param.IALCNo = "";
            }

            Session["LCCostingDetailsParam"] = param;

            string sql = string.Format("exec LCCostingDetails '" + model.ILCID + "',  '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<LCCostingDetailsReport>(sql).ToList();
            if (items.Count == 0)
            {
                LCCostingDetailsReport report = new LCCostingDetailsReport();
                items.Add(report);
            }
            Session["LCCostingDetailsData"] = items;
            return Redirect("/CrystalReport/LCCostingDetailsRpt.aspx");
        }
        

        public ActionResult ShowPurchaseOrderDetails(ReportVModel model)
        {
            ReportParmPersister param = new ReportParmPersister();
            if (model.SupplierID <= 0)
            {
                model.SupplierID = 0;
            }
            if (model.SupplierID > 0)
            {
                string supplier = context.Supplier.FirstOrDefault(x => x.Id == model.SupplierID).SupplierName.ToString();
                param.SupplierName = supplier;
            }
            else
            {
                param.SupplierName = "ALL";
            }
            string sql = string.Format("exec PurchaseOrderDetails " + model.SupplierID + ",'" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<PurchaseOrderDetails>(sql).ToList();
            if (items.Count == 0)
            {
                PurchaseOrderDetails report = new PurchaseOrderDetails();
                items.Add(report);
            }
            param.Description = "From " + model.From.Date.ToString("dd-MM-yyyy") + " to " + model.To.Date.ToString("dd-MM-yyyy");
            Session["PurchaseOrderDetailsParam"] = param;
            Session["PurchaseOrderDetailsData"] = items;
            return Redirect("/CrystalReport/PurchaseOrderDetailsRpt.aspx");
        }

        public ActionResult ShowILCBDetails(ReportVModel model)
        {
            ReportParmPersister param = new ReportParmPersister();
           
            if (model.ILCID <= 0)
            {
                model.ILCID = 0;
                
            }
            

            string sql = string.Format("exec ILCBDetails " + "'"+  DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "','" + model.ILCID + "'");
            var items = context.Database.SqlQuery<ILCPPreviewReport>(sql).ToList();
            if (items.Count == 0)
            {
                ILCPPreviewReport report = new ILCPPreviewReport();
                items.Add(report);
            }
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["ILCBDetailsparam"] = param;
            Session["ILCBDetailsReportData"] = items;
            return Redirect("/CrystalReport/ILCBDetailsRpt.aspx");
        }

        public ActionResult ShowILCSubLedgerItem(ReportVModel model)
        {
            ReportParmPersister param = new ReportParmPersister();

            //var subLedger = context.ImportCostItems.FirstOrDefault(x => x.SlnNo == model.SubLedgerID);
            //if(model.DebitAccID == "")
            //{

            //}
            string sql = string.Format("exec ILCSubLedgerItem " + "'" + model.DebitAccID + "' , '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<ILCSubLedgerItemReport>(sql).ToList();
            if (items.Count == 0)
            {
                ILCSubLedgerItemReport report = new ILCSubLedgerItemReport();
                items.Add(report);
            }
            param.FromDate = model.From;
            param.ToDate = model.To;
            if(model.DebitAccID != 0)
            {
                param.AccountName = context.ChartOfAccount.FirstOrDefault(x => x.Id == model.DebitAccID).Name;
            }
            else
            {
                param.AccountName = "";
            }

            Session["ILCSubLedgerItemParam"] = param;
            Session["ILCSubLedgerItemData"] = items;
            return Redirect("/CrystalReport/ILCSubLedgerItemRpt.aspx");
        }
    }
}