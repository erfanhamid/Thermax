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
    public partial class MonthlyExpenseReportRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                MonthlyExpenseReportR report = (MonthlyExpenseReportR)Session["MonthlyExpenseReportDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            MonthlyExpenseReportR ME = new MonthlyExpenseReportR();

            ME.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\MonthlyExpenseReportR.rpt");
            ME.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["MonthlyExpenseReportParam"] as ReportParmPersister;
            List<MonthlyExpenseReport> data = HttpContext.Current.Session["MonthlyExpenseReportData"] as List<MonthlyExpenseReport>;
            ME.SetDataSource(data);

            ME.SetParameterValue("compAddress", persister.CompAddress);
            ME.SetParameterValue("compName", persister.CompName);
            ME.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            ME.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = ME;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["MonthlyExpenseReportDoc"] = ME;
        }
    }
}