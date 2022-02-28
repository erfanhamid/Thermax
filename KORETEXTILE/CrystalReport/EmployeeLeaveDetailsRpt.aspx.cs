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
    public partial class EmployeeLeaveDetailsRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                EmployeeLeaveDetailsR report = (EmployeeLeaveDetailsR)Session["EmployeeLeaveDetailsDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            EmployeeLeaveDetailsR employeeLeaveDetailsR = new EmployeeLeaveDetailsR();

            employeeLeaveDetailsR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\EmployeeLeaveDetailsR.rpt");
            employeeLeaveDetailsR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["EmployeeLeaveDetailsParam"] as ReportParmPersister;
            List<EmployeeLeaveDetailsReport> data = HttpContext.Current.Session["EmployeeLeaveDetailsData"] as List<EmployeeLeaveDetailsReport>;
            employeeLeaveDetailsR.SetDataSource(data);

            employeeLeaveDetailsR.SetParameterValue("compName", persister.CompName);
            employeeLeaveDetailsR.SetParameterValue("address", persister.CompAddress);
            employeeLeaveDetailsR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            employeeLeaveDetailsR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = employeeLeaveDetailsR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["EmployeeLeaveDetailsDoc"] = employeeLeaveDetailsR;
        }
    }
}