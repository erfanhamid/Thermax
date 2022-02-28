using BEEERP.CrystalReport.ReportFormat;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.ViewModel.Location.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Location.Report
{
    [ShowNotification]
    public class TrackerReportController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: TrackerReport
        public ActionResult Search()
        {
            var depot = ScreenSessionData.GetEmployeeDepot();
            if (depot == null)
            {
                ViewBag.Emp = LoadComboBox.LoadEmployeeByDepot(null);
            }
            else
            {
                ViewBag.Emp = LoadComboBox.LoadEmployeeByDepot(depot);
            }
            return View();
        }

        public ActionResult ShowTSOWiseTrackingAll(TrackerReportVModel model)
        {
            var tsoId = 0;
            Session["ReportName"] = "TSOWiseTrackingHistoryAll";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;
            param.TsoId = model.TsoID;
            if(model.TsoID != 0)
            {
                tsoId = model.TsoID;
            }
            Session["TSOWiseTrackingHistoryAll"] = param;
            string sql = string.Format("exec TSOWiseTrackingHistoryALL  '" + tsoId + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "'");
            var items = context.Database.SqlQuery<TsoWiseTrackingHistory>(sql).ToList();
            if (items.Count == 0)
            {
                TsoWiseTrackingHistory report = new TsoWiseTrackingHistory();
                items.Add(report);
            }
            Session["TSOWiseTrackingHistoryAllData"] = items;
            return Redirect("/CrystalReport/TsoWiseTrackHistoryAllRpt.aspx");
        }

        public ActionResult ShowTSOWiseTracking(TrackerReportVModel model)
        {
            var tsoId = 0;
            Session["ReportName"] = "TSOWiseTrackingHistory";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;
            param.TsoId = model.TsoID;
            if (model.TsoID != 0)
            {
                tsoId = model.TsoID;
            }
            Session["TSOWiseTrackingHistory"] = param;
            string sql = string.Format("exec TSOWiseTrackingHistory  '" + tsoId + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.ToDate) + "'");
            var items = context.Database.SqlQuery<TsoWiseTrackingHistory>(sql).ToList();
            if (items.Count == 0)
            {
                TsoWiseTrackingHistory report = new TsoWiseTrackingHistory();
                items.Add(report);
            }
            Session["TSOWiseTrackingHistoryData"] = items;
            return Redirect("/CrystalReport/TsoWiseTrackHistoryRpt.aspx");
        }
    }
}