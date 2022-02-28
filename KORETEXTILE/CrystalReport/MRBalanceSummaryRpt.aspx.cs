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
    public partial class MRBalanceSummaryRpt : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                EmployeeMRBalanceSummaryR report = (EmployeeMRBalanceSummaryR)Session["EmployeeMRBalanceSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            EmployeeMRBalanceSummaryR mrbs = new EmployeeMRBalanceSummaryR();

            mrbs.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\EmployeeMRBalanceSummaryR.rpt");
            mrbs.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["MRBalanceSummaryParam"] as ReportParmPersister;
            List<MRBalanceSummaryReport> data = HttpContext.Current.Session["MRBalanceSummaryData"] as List<MRBalanceSummaryReport>;
            mrbs.SetDataSource(data);

            //mrbs.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            //mrbs.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            mrbs.SetParameterValue("asDate", persister.AsOnDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = mrbs;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["EmployeeMRBalanceSummaryDoc"] = mrbs;
        }
    }
}