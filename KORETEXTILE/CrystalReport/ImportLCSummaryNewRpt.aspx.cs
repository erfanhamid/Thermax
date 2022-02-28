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
    public partial class ImportLCSummaryNewRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                ImportLCSummaryNewR report = (ImportLCSummaryNewR)Session["ImportLCSummaryNewDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            ImportLCSummaryNewR ilcs = new ImportLCSummaryNewR();

            ilcs.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\BatchitemwiseFGPSummaryR.rpt");
            ilcs.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["ILCSParam"] as ReportParmPersister;
            List<ImportLCSummaryNewReport> data = HttpContext.Current.Session["ILCSData"] as List<ImportLCSummaryNewReport>;
            ilcs.SetDataSource(data);

            ilcs.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            ilcs.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = ilcs;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ImportLCSummaryNewDoc"] = ilcs;
        }
    }
}