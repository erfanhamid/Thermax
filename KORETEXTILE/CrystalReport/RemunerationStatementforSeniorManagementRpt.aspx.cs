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
    public partial class RemunerationStatementforSeniorManagementRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                RemunerationStatementforSeniorManagementR report = (RemunerationStatementforSeniorManagementR)Session["RemunerationStatementforSeniorManagementDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            RemunerationStatementforSeniorManagementR remunerationStatementforSeniorManagementR = new RemunerationStatementforSeniorManagementR();

            remunerationStatementforSeniorManagementR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\RemunerationStatementforSeniorManagementR.rpt");
            remunerationStatementforSeniorManagementR.Load(APPPATH);

            List<RemunerationStatementforSeniorManagementReport> data = HttpContext.Current.Session["RemunerationStatementforSeniorManagementData"] as List<RemunerationStatementforSeniorManagementReport>;

            remunerationStatementforSeniorManagementR.SetDataSource(data);
            ReportParmPersister persister = HttpContext.Current.Session["RemunerationStatementforSeniorManagementParam"] as ReportParmPersister;

            remunerationStatementforSeniorManagementR.SetParameterValue("CompName", persister.CompName);
            //remunerationStatementforSeniorManagementR.SetParameterValue("RName", persister.ReportName);
            remunerationStatementforSeniorManagementR.SetParameterValue("mon", persister.Months);
            remunerationStatementforSeniorManagementR.SetParameterValue("yr", persister.Year);
            remunerationStatementforSeniorManagementR.SetParameterValue("DateDuration", persister.Month);

            CrystalReportViewer1.ReportSource = remunerationStatementforSeniorManagementR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["RemunerationStatementforSeniorManagementDoc"] = remunerationStatementforSeniorManagementR;
        }
    }
}