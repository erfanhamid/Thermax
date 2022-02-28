using BEEERP.CrystalReport.ReportFormat;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using BEEERP.Models.ViewModel.Sales.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Models.SalesModule.Report
{
    public class TargetAchievementReportController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: TargetAchievementReport
        public ActionResult ShowTargetAchievement(ReportVModel model)
        {
            Session["reportName"] = "Target Achievement";

            //var items = context.Database.SqlQuery<TargetAchievementDetailsReport>(string.Format("exec TargetAchievement '" + model.TSOId  + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "'," +  DateTimeFormat.ConvertToDDMMYYYY(model.To) + "")).ToList();
            var items = context.Database.SqlQuery<TargetAchievementDetailsReport>(string.Format("exec TargetAchievement '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'," + model.TSOId + "")).ToList();
            ReportParmPersister param = new ReportParmPersister();

            param.FromDate = model.From;
            param.ToDate = model.To;
            param.TsoName = context.Employees.FirstOrDefault(x => x.Id == model.TSOId).Name;
            Session["TargetAchievementDetailsReportData"] = items;
            Session["targetAchievement"] = param;

            return Redirect("/CrystalReport/TargetAchievementRpt.aspx");
        }
    }
}