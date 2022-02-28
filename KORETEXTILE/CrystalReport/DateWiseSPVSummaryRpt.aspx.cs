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
    public partial class DateWiseSPVSummaryRpt : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                DateWiseSPVSummaryR report = (DateWiseSPVSummaryR)Session["DateWiseSPVSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            DateWiseSPVSummaryR dwspvs = new DateWiseSPVSummaryR();

            dwspvs.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\DateWiseSPVSummaryNewR.rpt");
            dwspvs.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["dwsspvparam"] as ReportParmPersister;
            List<DateWiseSPVSummaryReport> data = HttpContext.Current.Session["dwspvsData"] as List<DateWiseSPVSummaryReport>;
            dwspvs.SetDataSource(data);

            dwspvs.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            dwspvs.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = dwspvs;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["DateWiseSPVSummaryDoc"] = dwspvs;
        }
    }
}