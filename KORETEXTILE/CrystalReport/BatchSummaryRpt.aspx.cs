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
    public partial class BatchSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                BatchSummaryR report = (BatchSummaryR)Session["BatchSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            BatchSummaryR batchSummaryR = new BatchSummaryR();

            batchSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\BatchSummaryR.rpt");
            batchSummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["BatchSummary"] as ReportParmPersister;
            List<BatchSummaryReport> data = HttpContext.Current.Session["BatchSummaryReportData"] as List<BatchSummaryReport>;
            batchSummaryR.SetDataSource(data);


            batchSummaryR.SetParameterValue("address", "");
            batchSummaryR.SetParameterValue("compName", "");
            batchSummaryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            batchSummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = batchSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["BatchSummaryDoc"] = batchSummaryR;
        }
    }
}