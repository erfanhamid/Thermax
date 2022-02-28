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
    public partial class SalarySheetSummaryReportRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SalarySheetSummaryR report = (SalarySheetSummaryR)Session["ReportDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SalarySheetSummaryR salarySheetSummaryR = new SalarySheetSummaryR();

            salarySheetSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SalarySheetSummaryR.rpt");
            salarySheetSummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SalarySheetSummaryParam"] as ReportParmPersister;
            List<SalarySheetSummaryReport> data = HttpContext.Current.Session["SalarySheetSummaryReportData"] as List<SalarySheetSummaryReport>;
            salarySheetSummaryR.SetDataSource(data);

            salarySheetSummaryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            salarySheetSummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
          

            CrystalReportViewer1.ReportSource = salarySheetSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ReportDoc"] = salarySheetSummaryR;
        }
    }
}