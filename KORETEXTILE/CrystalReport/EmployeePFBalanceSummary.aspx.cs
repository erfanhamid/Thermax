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
    public partial class EmployeePFBalanceSummary : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                EPFBalanceSummaryR report = (EPFBalanceSummaryR)Session["EPFBalanceSummaryRDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            EPFBalanceSummaryR ePFBalanceSummaryR = new EPFBalanceSummaryR();

            ePFBalanceSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\EPFBalanceSummaryR.rpt");
            ePFBalanceSummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["EPFBalanceSummaryParam"] as ReportParmPersister;
            List<EmployeePFBalanceSummary> data = HttpContext.Current.Session["empListByNameReportData"] as List<EmployeePFBalanceSummary>;
            ePFBalanceSummaryR.SetDataSource(data);


            //ePFBalanceSummaryR.SetParameterValue("address", "");
            //ePFBalanceSummaryR.SetParameterValue("compName", "");
            //ePFBalanceSummaryR.SetParameterValue("Date", persister.AsOnDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = ePFBalanceSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["EPFBalanceSummaryRDoc"] = ePFBalanceSummaryR;
        }
    }
}