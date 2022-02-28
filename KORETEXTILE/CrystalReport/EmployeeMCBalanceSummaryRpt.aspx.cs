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
    public partial class EmployeeMCBalanceSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                EMCBalanceSummaryR report = (EMCBalanceSummaryR)Session["EMCBalanceSummaryRDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            EMCBalanceSummaryR eMCBalanceSummaryR = new EMCBalanceSummaryR();

            eMCBalanceSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\EMCBalanceSummaryR.rpt");
            eMCBalanceSummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["EMCBalanceSummaryParam"] as ReportParmPersister;
            List<BEEERP.CrystalReport.ReportFormat.EmployeeMCBalanceSummary> data = HttpContext.Current.Session["hello"] as List<BEEERP.CrystalReport.ReportFormat.EmployeeMCBalanceSummary>;
            eMCBalanceSummaryR.SetDataSource(data);


            //ePFBalanceSummaryR.SetParameterValue("address", "");
            //ePFBalanceSummaryR.SetParameterValue("compName", "");
            eMCBalanceSummaryR.SetParameterValue("Date", persister.AsOnDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = eMCBalanceSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["EMCBalanceSummaryRDoc"] = eMCBalanceSummaryR;
        }
    }
}