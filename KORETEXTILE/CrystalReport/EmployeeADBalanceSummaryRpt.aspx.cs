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
    public partial class EmployeeADBalanceSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                EADBalanceSummaryR report = (EADBalanceSummaryR)Session["EADBalanceSummaryRDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            EADBalanceSummaryR eADBalanceSummaryR = new EADBalanceSummaryR();

            eADBalanceSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\EADBalanceSummaryR.rpt");
            eADBalanceSummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["EADBalanceSummaryParam"] as ReportParmPersister;
            List<BEEERP.CrystalReport.ReportFormat.EmployeeADBalanceSummary> data = HttpContext.Current.Session["hello"] as List<BEEERP.CrystalReport.ReportFormat.EmployeeADBalanceSummary>;
            eADBalanceSummaryR.SetDataSource(data);


            //ePFBalanceSummaryR.SetParameterValue("address", "");
            //ePFBalanceSummaryR.SetParameterValue("compName", "");
            eADBalanceSummaryR.SetParameterValue("Date", persister.AsOnDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = eADBalanceSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["EADBalanceSummaryRDoc"] = eADBalanceSummaryR;
        }
    }
}