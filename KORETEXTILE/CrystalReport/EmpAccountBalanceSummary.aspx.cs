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
    public partial class EmpAccountBalanceSummary : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                EmpAccBalanceSummary report = (EmpAccBalanceSummary)Session["ReportDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            EmpAccBalanceSummary empAccBalanceSummary = new EmpAccBalanceSummary();

            empAccBalanceSummary.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\EmpAccBalanceSummary.rpt");
            empAccBalanceSummary.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["EmployeeBalanceSummaryDate"] as ReportParmPersister;
            List<EmpBalanceSummaryReport> data = HttpContext.Current.Session["empBalanceSummaryReportData"] as List<EmpBalanceSummaryReport>;
            empAccBalanceSummary.SetDataSource(data);


            empAccBalanceSummary.SetParameterValue("address", "");
            empAccBalanceSummary.SetParameterValue("compName", "");
            empAccBalanceSummary.SetParameterValue("AsOnDate", persister.AsOnDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = empAccBalanceSummary;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ReportDoc"] = empAccBalanceSummary;
        }
    }
}