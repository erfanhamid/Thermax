using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.CommonInformation;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BEEERP.CrystalReport
{
    public partial class IncomeStatement : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                IncomeStatementR report = (IncomeStatementR)Session["IncomeStatementDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            IncomeStatementR incomeStatement = new IncomeStatementR();
            incomeStatement.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\IncomeStatementR.rpt");
            incomeStatement.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["IncomeStatementparam"] as ReportParmPersister;
            List<IncomeStatementReport> data = HttpContext.Current.Session["IncomeStatementData"] as List<IncomeStatementReport>;
            incomeStatement.SetDataSource(data);



            incomeStatement.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            incomeStatement.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            incomeStatement.SetParameterValue("address", persister.CompAddress);
            incomeStatement.SetParameterValue("compName", persister.CompName);

            CrystalReportViewer1.ReportSource = incomeStatement;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["IncomeStatementDoc"] = incomeStatement;
        }

    }
}