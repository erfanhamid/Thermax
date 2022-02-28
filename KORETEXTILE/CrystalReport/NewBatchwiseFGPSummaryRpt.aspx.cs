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
    public partial class NewBatchwiseFGPSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                BatchwiseFGPSummaryR report = (BatchwiseFGPSummaryR)Session["BatchwiseFGPSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            BatchwiseFGPSummaryR batchwiseFGPSummaryR = new BatchwiseFGPSummaryR();

            batchwiseFGPSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\BatchwiseFGPSummaryR.rpt");
            batchwiseFGPSummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["BatchwiseFGPSummaryParam"] as ReportParmPersister;
            List<BatchwiseFGPSummaryReport> data = HttpContext.Current.Session["BatchwiseFGPSummaryData"] as List<BatchwiseFGPSummaryReport>;
            batchwiseFGPSummaryR.SetDataSource(data);

            batchwiseFGPSummaryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            batchwiseFGPSummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = batchwiseFGPSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ItemwiseFGPSummaryDoc"] = batchwiseFGPSummaryR;
        }
    }
}