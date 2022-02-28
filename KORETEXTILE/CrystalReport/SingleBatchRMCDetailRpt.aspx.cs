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
    public partial class SingleBatchRMCDetailRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleBatchRMCDetailR report = (SingleBatchRMCDetailR)Session["SingleBatchRMCDetailDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SingleBatchRMCDetailR singleBatchRMCDetailR = new SingleBatchRMCDetailR();

            singleBatchRMCDetailR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\ItemWiseRMIssueSummaryR.rpt");
            singleBatchRMCDetailR.Load(APPPATH);

            //ReportParmPersister persister = HttpContext.Current.Session["SingleBatchRMCDetailparam"] as ReportParmPersister;
            List<SingleBatchRMCDetailReport> data = HttpContext.Current.Session["SingleBatchRMCDetailData"] as List<SingleBatchRMCDetailReport>;
            singleBatchRMCDetailR.SetDataSource(data);

            //singleBatchRMCDetailR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            //singleBatchRMCDetailR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = singleBatchRMCDetailR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SingleBatchRMCDetailDoc"] = singleBatchRMCDetailR;
        }
    }
}