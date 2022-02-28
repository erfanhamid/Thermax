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
    public partial class PlayerSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                PlayerSummaryR report = (PlayerSummaryR)Session["PlayerSummarySession"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            PlayerSummaryR playerSummary = new PlayerSummaryR();

            playerSummary.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\PlayerSummaryR.rpt");
            playerSummary.Load(APPPATH);

            List<PlayerSummaryReport> data = HttpContext.Current.Session["PlayerSummaryData"] as List<PlayerSummaryReport>;
            playerSummary.SetDataSource(data);

            CrystalReportViewer1.ReportSource = playerSummary;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["PlayerSummarySession"] = playerSummary;
        }
    }
}