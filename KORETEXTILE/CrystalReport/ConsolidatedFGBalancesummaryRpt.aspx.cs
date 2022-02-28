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
    public partial class ConsolidatedFGBalancesummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                ConsolidatedFGBalancesummaryR report = (ConsolidatedFGBalancesummaryR)Session["ConsolidatedFGBalancesummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            ConsolidatedFGBalancesummaryR consolidatedFGBalancesummaryR = new ConsolidatedFGBalancesummaryR();

            consolidatedFGBalancesummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\ConsolidatedFGBalancesummaryR.rpt");
            consolidatedFGBalancesummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["ConsolidatedFGBsummaryparam"] as ReportParmPersister;
            List<ConsolidatedFGBsummaryReport> data = HttpContext.Current.Session["ConsolidatedFGBsummaryData"] as List<ConsolidatedFGBsummaryReport>;
            consolidatedFGBalancesummaryR.SetDataSource(data);

            consolidatedFGBalancesummaryR.SetParameterValue("asDate", persister.AsOnDate.ToString("dd-MM-yyyy"));
            //singleStoreRMBalanceSummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = consolidatedFGBalancesummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ConsolidatedFGBalancesummaryDoc"] = consolidatedFGBalancesummaryR;
        }
    }
}