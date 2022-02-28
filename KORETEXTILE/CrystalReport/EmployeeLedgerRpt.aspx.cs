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
    public partial class EmployeeBalanceCalculationRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                EmployeeLedgerR report = (EmployeeLedgerR)Session["EmpBalCalReportDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {

            EmployeeLedgerR empbalance = new EmployeeLedgerR();

            empbalance.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\EmployeeLedgerR.rpt");
            empbalance.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["EmployeeBalanceCalculationParam"] as ReportParmPersister;
            List<EmployeeLedger> data = HttpContext.Current.Session["EmployeeBalanceCalculationData"] as List<EmployeeLedger>;
            empbalance.SetDataSource(data);

            empbalance.SetParameterValue("employeeid", persister.EmployeeID);
            empbalance.SetParameterValue("employeename", persister.EmployeeName);
            empbalance.SetParameterValue("address", persister.CompAddress);
            empbalance.SetParameterValue("compName", persister.CompName);
            empbalance.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            empbalance.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = empbalance;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["EmpBalCalReportDoc"] = empbalance;
        }
    }
}