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
    public partial class SinglestorermqtybalanceRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SinglestorermqtybalanceR report = (SinglestorermqtybalanceR)Session["SinglestorermqtybalanceDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SinglestorermqtybalanceR singlestorermqtybalanceR = new SinglestorermqtybalanceR();

            singlestorermqtybalanceR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\CompanyWiseSalarySummaryReport.rpt");
            singlestorermqtybalanceR.Load(APPPATH);

            List<SinglestorermqtybalanceReport> data = HttpContext.Current.Session["SingleStoreRMQtySummaryData"] as List<SinglestorermqtybalanceReport>;

            singlestorermqtybalanceR.SetDataSource(data);
            ReportParmPersister persister = HttpContext.Current.Session["SingleStoreRMQtySummaryparam"] as ReportParmPersister;

            //singlestorermqtybalanceR.SetParameterValue("CompanyName", persister.CompName);
            //singlestorermqtybalanceR.SetParameterValue("Month", persister.Month);
            //singlestorermqtybalanceR.SetParameterValue("Year", persister.Year);

            singlestorermqtybalanceR.SetParameterValue("asOnDate", persister.AsOnDate.ToString("dd-MM-yyyy"));
            singlestorermqtybalanceR.SetParameterValue("StoreName", persister.StoreName.ToString());

            CrystalReportViewer1.ReportSource = singlestorermqtybalanceR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SinglestorermqtybalanceDoc"] = singlestorermqtybalanceR;
        }
    }
}