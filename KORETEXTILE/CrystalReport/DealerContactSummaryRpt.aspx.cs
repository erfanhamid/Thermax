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
    public partial class DealerContactSummaryRpt : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                DealerContactSummaryR empListByName = (DealerContactSummaryR)Session["DealerContactSummaryDoc"];
                CrystalReportViewer1.ReportSource = empListByName;
            }

        }
        public void ShowReport()
        {
            DealerContactSummaryR dealerContactSummaryR = new DealerContactSummaryR();

            dealerContactSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\DealerContactSummaryR.rpt");
            dealerContactSummaryR.Load(APPPATH);
            List<DealerContactSummaryReport> data = HttpContext.Current.Session["DealerContactSummaryReportData"] as List<DealerContactSummaryReport>;
            dealerContactSummaryR.SetDataSource(data);
            CrystalReportViewer1.ReportSource = dealerContactSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["DealerContactSummaryDoc"] = dealerContactSummaryR;
        }
    }
}