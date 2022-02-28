using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.CommonInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BEEERP.CrystalReport
{
    public partial class TargetAchievementRpt : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                TargetAchievementR report = (TargetAchievementR)Session["ReportDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            TargetAchievementR targetAchievementR = new TargetAchievementR();

            targetAchievementR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\TargetAchievementR.rpt");
            targetAchievementR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["targetAchievement"] as ReportParmPersister;
            List<TargetAchievementDetailsReport> data = HttpContext.Current.Session["TargetAchievementDetailsReportData"] as List<TargetAchievementDetailsReport>;
            targetAchievementR.SetDataSource(data);

            targetAchievementR.SetParameterValue("compName", persister.CompName);
            targetAchievementR.SetParameterValue("compAddress", persister.CompAddress);
            targetAchievementR.SetParameterValue("TsoName", persister.TsoName);
            targetAchievementR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            targetAchievementR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = targetAchievementR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ReportDoc"] = targetAchievementR;
        }
    }
}