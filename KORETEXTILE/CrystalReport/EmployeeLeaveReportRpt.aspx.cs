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
    public partial class EmployeeLeaveReportRpt : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                EmployeeLeaveReportR empLeave = (EmployeeLeaveReportR)Session["empLeaveReportDoc"];
                CrystalReportViewer1.ReportSource = empLeave;
            }

        }
        public void ShowReport()
        {
            EmployeeLeaveReportR empleave = new EmployeeLeaveReportR();

            empleave.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\EmployeeLeaveReportR.rpt");
            empleave.Load(APPPATH);

            List<EmployeeLeaveReport> data = HttpContext.Current.Session["empLeaveReportData"] as List<EmployeeLeaveReport>;
            empleave.SetDataSource(data);
            ReportParmPersister persister = HttpContext.Current.Session["empLeaveParam"] as ReportParmPersister;
            empleave.SetParameterValue("CompName", persister.CompName);
            empleave.SetParameterValue("CompAddress", persister.CompAddress);
            empleave.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            empleave.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = empleave;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["empLeaveReportDoc"] = empleave;
        }
    }
}