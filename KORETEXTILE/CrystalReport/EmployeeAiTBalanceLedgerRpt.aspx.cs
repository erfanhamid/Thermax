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
    public partial class EmployeeAiTBalanceLedgerRpt : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                EmployeeAiTBalanceLedger report = (EmployeeAiTBalanceLedger)Session["ReportDoc"];
                CrystalReportViewer1.ReportSource = report;
            }

        }

        public void ShowReport()
        {
            EmployeeAiTBalanceLedger empAitBalLedger = new EmployeeAiTBalanceLedger();

            empAitBalLedger.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\EmployeeTaxBalanceLedger.rpt");
            empAitBalLedger.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["EmployeeAiTBalanceLedgerParam"] as ReportParmPersister;
            List<EmployeeAITBalanceLedgerReport> data = HttpContext.Current.Session["EmployeeAiTBalanceLedgerData"] as List<EmployeeAITBalanceLedgerReport>;
            empAitBalLedger.SetDataSource(data);


            empAitBalLedger.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            empAitBalLedger.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            empAitBalLedger.SetParameterValue("empId", persister.EmployeeID);

            CrystalReportViewer1.ReportSource = empAitBalLedger;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ReportDoc"] = empAitBalLedger;
        }
    }
}