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
    public partial class SalaryIncrementReport : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SalaryIncrementR report = (SalaryIncrementR)Session["SalaryIncrementReport"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SalaryIncrementR salaryIncrement = new SalaryIncrementR();

            salaryIncrement.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SalaryIncrementR.rpt");
            salaryIncrement.Load(APPPATH);

            List<EmployeeSalaryIncrementReport> data = HttpContext.Current.Session["SalaryIncrementReportData"] as List<EmployeeSalaryIncrementReport>;

            salaryIncrement.SetDataSource(data);
            ReportParmPersister persister = HttpContext.Current.Session["SalaryIncrementReportParam"] as ReportParmPersister;

            salaryIncrement.SetParameterValue("CompanyName", persister.CompName);
            salaryIncrement.SetParameterValue("DateDuration", persister.Month);
            salaryIncrement.SetParameterValue("EmployeeId", persister.EmployeeID);
            salaryIncrement.SetParameterValue("EmployeeName", persister.Description);

            CrystalReportViewer1.ReportSource = salaryIncrement;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SalaryIncrementReport"] = salaryIncrement;
        }
    }
}