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
    public partial class CompanyWiseSalarySummaryReportRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                CompanyWiseSalarySummaryR report = (CompanyWiseSalarySummaryR)Session["CompanyWiseSalarySummaryReport"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            CompanyWiseSalarySummaryR companyWiseSalarySummary = new CompanyWiseSalarySummaryR();

            companyWiseSalarySummary.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\CompanyWiseSalarySummaryReport.rpt");
            companyWiseSalarySummary.Load(APPPATH);

            List<CompanyWiseSalarySummaryReport> data = HttpContext.Current.Session["CompanyWiseSalarySummaryData"] as List<CompanyWiseSalarySummaryReport>;

            companyWiseSalarySummary.SetDataSource(data);
            ReportParmPersister persister = HttpContext.Current.Session["CompanyWiseSalarySummaryParam"] as ReportParmPersister;

            companyWiseSalarySummary.SetParameterValue("CompanyName", persister.CompName);
            companyWiseSalarySummary.SetParameterValue("Month", persister.Month);
            companyWiseSalarySummary.SetParameterValue("Year", persister.Year);


            CrystalReportViewer1.ReportSource = companyWiseSalarySummary;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["CompanyWiseSalarySummaryReport"] = companyWiseSalarySummary;
        }
    }
}