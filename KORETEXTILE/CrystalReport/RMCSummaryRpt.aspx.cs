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
    public partial class RMCSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                RMCSummaryR report = (RMCSummaryR)Session["RMCSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            RMCSummaryR RMC = new RMCSummaryR();

            RMC.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\RMCSummaryR.rpt");
            RMC.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["RMCParam"] as ReportParmPersister;
            List<RMCSummaryReport> data = HttpContext.Current.Session["RMCData"] as List<RMCSummaryReport>;
            RMC.SetDataSource(data);

            RMC.SetParameterValue("compAddress", persister.CompAddress);
            RMC.SetParameterValue("compName", persister.CompName);
            RMC.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            RMC.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            RMC.SetParameterValue("reportName", Session["ReportName"]);
          



            CrystalReportViewer1.ReportSource = RMC;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["RMCSummaryDoc"] = RMC;
        }
    }
}