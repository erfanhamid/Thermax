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
    public partial class ConsolidatedRMBalanceSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                ConsolidatedRMBalanceSummaryR report = (ConsolidatedRMBalanceSummaryR)Session["ConsolidatedRMBalanceSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            ConsolidatedRMBalanceSummaryR consolidatedRMBalanceSummaryR = new ConsolidatedRMBalanceSummaryR();

            consolidatedRMBalanceSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\ConsolidatedRMBalanceSummaryR.rpt");
            consolidatedRMBalanceSummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["ConsolidatedRMBSummaryparam"] as ReportParmPersister;
            List<ConsolidatedRMBSummaryReport> data = HttpContext.Current.Session["ConsolidatedRMBSummaryData"] as List<ConsolidatedRMBSummaryReport>;
            consolidatedRMBalanceSummaryR.SetDataSource(data);

            consolidatedRMBalanceSummaryR.SetParameterValue("asDate", persister.AsOnDate.ToString("dd-MM-yyyy"));
            //singleStoreRMBalanceSummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = consolidatedRMBalanceSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ConsolidatedRMBalanceSummaryDoc"] = consolidatedRMBalanceSummaryR;
        }
    }
}