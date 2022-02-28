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
    public partial class EmployeeTaxBalanceLedgerRpt : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                EmployeeTaxBalanceLedger report = (EmployeeTaxBalanceLedger)Session["ReportDoc"];
                CrystalReportViewer1.ReportSource = report;
            }

        }

        public void ShowReport()
        {
            EmployeeTaxBalanceLedger empTaxBalLedger = new EmployeeTaxBalanceLedger();

            empTaxBalLedger.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\EmployeeTaxBalanceLedger.rpt");
            empTaxBalLedger.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["EmployeeTaxBalanceLedgerParam"] as ReportParmPersister;
            List<EmployeeTaxBalanceLedgerReport> data = HttpContext.Current.Session["EmployeeTaxBalanceLedgerData"] as List<EmployeeTaxBalanceLedgerReport>;
            empTaxBalLedger.SetDataSource(data);


            empTaxBalLedger.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            empTaxBalLedger.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            empTaxBalLedger.SetParameterValue("empId", persister.EmployeeID);

            CrystalReportViewer1.ReportSource = empTaxBalLedger;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ReportDoc"] = empTaxBalLedger;
        }
    }
}