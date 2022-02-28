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
    public partial class LeaveStatusRpt : System.Web.UI.Page
    {

        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                LeaveStatusR report = (LeaveStatusR)Session["LeaveStausDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            LeaveStatusR leave = new LeaveStatusR();
            leave.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\LeaveStatusR.rpt");
            leave.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["LeaveStatusParam"] as ReportParmPersister;
            List<LeaveStatus> data = HttpContext.Current.Session["LeaveStatusData"] as List<LeaveStatus>;
            leave.SetDataSource(data);

            leave.SetParameterValue("compName", persister.CompName);
            leave.SetParameterValue("address", persister.CompAddress);
            

            CrystalReportViewer1.ReportSource = leave;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["LeaveStausDoc"] = leave;
        }
    }
}