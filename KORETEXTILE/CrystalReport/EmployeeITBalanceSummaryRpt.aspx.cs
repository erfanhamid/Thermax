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
    public partial class EmployeeITBalanceSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                EITBalanceSummary report = (EITBalanceSummary)Session["EITBalanceSummaryRDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            EITBalanceSummary eITBalanceSummary = new EITBalanceSummary();

            eITBalanceSummary.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\EITBalanceSummary.rpt");
            eITBalanceSummary.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["EITBalanceSummaryParam"] as ReportParmPersister;
            List<BEEERP.CrystalReport.ReportFormat.EmployeeITBalanceSummary> data = HttpContext.Current.Session["hello"] as List<BEEERP.CrystalReport.ReportFormat.EmployeeITBalanceSummary>;
            eITBalanceSummary.SetDataSource(data);


            //ePFBalanceSummaryR.SetParameterValue("address", "");
            //ePFBalanceSummaryR.SetParameterValue("compName", "");
            eITBalanceSummary.SetParameterValue("Date", persister.AsOnDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = eITBalanceSummary;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["EITBalanceSummaryRDoc"] = eITBalanceSummary;
        }
    }
}