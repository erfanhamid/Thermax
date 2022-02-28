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
    public partial class ImportPISummaryNewRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                ImportPISummaryNewR report = (ImportPISummaryNewR)Session["ImportPISummaryNewDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            ImportPISummaryNewR ipis = new ImportPISummaryNewR();

            ipis.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\ImportPISummaryNewR.rpt");
            ipis.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["IPISParam"] as ReportParmPersister;
            List<ImportPISummaryNewReport> data = HttpContext.Current.Session["IPISData"] as List<ImportPISummaryNewReport>;
            ipis.SetDataSource(data);

            ipis.SetParameterValue("compAddress", persister.CompAddress);
            ipis.SetParameterValue("compName", persister.CompName);
            ipis.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            ipis.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            //supplierwisewosummaryR.SetParameterValue("reportName", Session["ReportName"]);




            CrystalReportViewer1.ReportSource = ipis;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ImportPISummaryNewDoc"] = ipis;
        }
    }
}