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
    public partial class DateWiseSPIssuevRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                DateWiseSPIssueSummaryR report = (DateWiseSPIssueSummaryR)Session["DateWiseSPIssueSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            DateWiseSPIssueSummaryR datewiseSPIssue = new DateWiseSPIssueSummaryR();

            datewiseSPIssue.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\DateWiseSPIssueSummaryR.rpt");
            datewiseSPIssue.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["DateWiseSPIssueparam"] as ReportParmPersister;
            List<DateWiseSPIssueReport> data = HttpContext.Current.Session["DateWiseSPIssueData"] as List<DateWiseSPIssueReport>;
            datewiseSPIssue.SetDataSource(data);

            datewiseSPIssue.SetParameterValue("compAddress", persister.CompAddress);
            datewiseSPIssue.SetParameterValue("compName", persister.CompName);
            datewiseSPIssue.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            datewiseSPIssue.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            //datewiseSPIssue.SetParameterValue("asDate", persister.AsOnDate.ToString("dd-MM-yyyy"));




            CrystalReportViewer1.ReportSource = datewiseSPIssue;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["DateWiseSPIssueSummaryDoc"] = datewiseSPIssue;
        }
    }
}