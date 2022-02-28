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
    public partial class ImportLCSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                ImportLCSummaryR report = (ImportLCSummaryR)Session["ImportLCSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            ImportLCSummaryR importLCSummaryR = new ImportLCSummaryR();

            importLCSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\ImportLCSummaryR.rpt");
            importLCSummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["ImportLCSummaryParam"] as ReportParmPersister;
            List<ImportLCSummaryReport> data = HttpContext.Current.Session["ImportLCSummaryData"] as List<ImportLCSummaryReport>;
            importLCSummaryR.SetDataSource(data);

            importLCSummaryR.SetParameterValue("address", persister.CompAddress);
            importLCSummaryR.SetParameterValue("compName", persister.CompName);
            importLCSummaryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            importLCSummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = importLCSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ImportLCSummaryDoc"] = importLCSummaryR;
        }
    }
}