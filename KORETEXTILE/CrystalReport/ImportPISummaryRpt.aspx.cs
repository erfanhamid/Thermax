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
    public partial class ImportPISummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                ImportPISummaryR report = (ImportPISummaryR)Session["ImportPISummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            ImportPISummaryR importPISummaryR = new ImportPISummaryR();

            importPISummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\ImportPISummaryNewR.rpt");
            importPISummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["ImportPISummaryParam"] as ReportParmPersister;
            List<ImportPISummaryReport> data = HttpContext.Current.Session["ImportPISummaryData"] as List<ImportPISummaryReport>;
            importPISummaryR.SetDataSource(data);

            //importPISummaryR.SetParameterValue("address", persister.CompAddress);
            //importPISummaryR.SetParameterValue("compName", persister.CompName);
            importPISummaryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            importPISummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = importPISummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ImportPISummaryDoc"] = importPISummaryR;
        }
    }
}