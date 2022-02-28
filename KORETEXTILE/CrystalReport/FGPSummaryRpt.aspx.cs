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
    public partial class FGPSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                FGPSummaryR report = (FGPSummaryR)Session["FGPSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            FGPSummaryR FGP = new FGPSummaryR();

            FGP.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\FGPSummaryR.rpt");
            FGP.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["FGPParam"] as ReportParmPersister;
            List<FGPSummaryReport> data = HttpContext.Current.Session["FGPData"] as List<FGPSummaryReport>;
            FGP.SetDataSource(data);

            FGP.SetParameterValue("compAddress", persister.CompAddress);
            FGP.SetParameterValue("compName", persister.CompName);
            FGP.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            FGP.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            FGP.SetParameterValue("reportName", Session["ReportName"]);




            CrystalReportViewer1.ReportSource = FGP;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["FGPSummaryDoc"] = FGP;
        }
    }
}