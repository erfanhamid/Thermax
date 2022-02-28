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
    [Authorize(Roles = "ProAdmin,ProOperator,ProViewer,ProApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    public class BatchDetailsController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: BatchDetails
        public ActionResult ShowBatchDetails()
        {
            ViewBag.LoadBatch = LoadComboBox.LoadBatch();
            return View();
        }


        public ActionResult GetBatchDetails(int BatchId)
        {
            Session["ReportName"] = "BatchDetailsR";

            string sql = string.Format("exec BatchDetails '" + BatchId + "'");
            var items = context.Database.SqlQuery<BatchDetailsReport>(sql).ToList();
            if (items.Count == 0)
            {
                BatchDetailsReport report = new BatchDetailsReport();
                items.Add(report);
            }
            Session["BatchDetailsReportData"] = items;
            return Redirect("/CrystalReport/BatchDetailsRpt.aspx");
        }

        public ActionResult GetBatchSummary(InventoryReportVModel model)
        {
            Session["ReportName"] = "BatchSummaryR";

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;
            Session["BatchSummary"] = param;

            string sql = string.Format("exec BatchSummary  '" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "' ");
            var items = context.Database.SqlQuery<BatchSummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                BatchSummaryReport report = new BatchSummaryReport();
                items.Add(report);
            }
            Session["BatchSummaryReportData"] = items;
            return Redirect("/CrystalReport/BatchSummaryRpt.aspx");
        }

    }
}