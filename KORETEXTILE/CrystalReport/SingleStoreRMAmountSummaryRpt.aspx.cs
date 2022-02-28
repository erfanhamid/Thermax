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
    public partial class SingleStoreRMAmountSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleStoreRMAmountSummaryR report = (SingleStoreRMAmountSummaryR)Session["SingleStoreRMAmountSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SingleStoreRMAmountSummaryR singleStoreRMAmountSummaryR = new SingleStoreRMAmountSummaryR();

            singleStoreRMAmountSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\CompanyWiseSalarySummaryReport.rpt");
            singleStoreRMAmountSummaryR.Load(APPPATH);

            List<SinglestorermamountbalanceReport> data = HttpContext.Current.Session["SingleStoreRMAmountSummaryData"] as List<SinglestorermamountbalanceReport>;

            singleStoreRMAmountSummaryR.SetDataSource(data);
            ReportParmPersister persister = HttpContext.Current.Session["SingleStoreRMAmountSummaryparam"] as ReportParmPersister;

            //singlestorermqtybalanceR.SetParameterValue("CompanyName", persister.CompName);
            //singlestorermqtybalanceR.SetParameterValue("Month", persister.Month);
            //singlestorermqtybalanceR.SetParameterValue("Year", persister.Year);

            singleStoreRMAmountSummaryR.SetParameterValue("asOnDate", persister.AsOnDate.ToString("dd-MM-yyyy"));
            singleStoreRMAmountSummaryR.SetParameterValue("StoreName", persister.StoreName.ToString());

            CrystalReportViewer1.ReportSource = singleStoreRMAmountSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SingleStoreRMAmountSummaryDoc"] = singleStoreRMAmountSummaryR;
        }
    }
}