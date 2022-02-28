using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.ViewModel.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BEEERP.CrystalReport
{
    public partial class SalaryDeductionSummaryReportRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SalaryDeductionSummaryR report = (SalaryDeductionSummaryR)Session["SalaryDeductionSummaryRDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SalaryDeductionSummaryR salaryDeductionSummary = new SalaryDeductionSummaryR();

            salaryDeductionSummary.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SalaryDeductionSummaryR.rpt");
            salaryDeductionSummary.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SalaryDeductionSummaryParam"] as ReportParmPersister;
            List<BEEERP.CrystalReport.ReportFormat.SalarydeductionReport> data = HttpContext.Current.Session["SalaryDeductionSummaryReportData"] as List<BEEERP.CrystalReport.ReportFormat.SalarydeductionReport>;
            salaryDeductionSummary.SetDataSource(data);


            //ePFBalanceSummaryR.SetParameterValue("address", "");
            //ePFBalanceSummaryR.SetParameterValue("compName", "");
            //salaryDeductionSummary.SetParameterValue("Date", persister.AsOnDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = salaryDeductionSummary;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SalaryDeductionSummaryRDoc"] = salaryDeductionSummary;
        }
    }
}