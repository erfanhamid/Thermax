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
    public partial class EmployeeOfficialHistoryRpt : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                EmployeeOfficialHistoryR empListByName = (EmployeeOfficialHistoryR)Session["EmployeeOfficialHistory"];
                CrystalReportViewer1.ReportSource = empListByName;
            }

        }
        public void ShowReport()
        {
            EmployeeOfficialHistoryR employeeOfficialHistory = new EmployeeOfficialHistoryR();

            employeeOfficialHistory.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\EmployeeOfficialHistoryR.rpt");
            employeeOfficialHistory.Load(APPPATH);
            ReportParmPersister persister = HttpContext.Current.Session["EmployeeOfficialHistoryParam"] as ReportParmPersister;
            List<EmployeeOfficialHistory> data = HttpContext.Current.Session["EmployeeOfficialHistoryData"] as List<EmployeeOfficialHistory>;
            employeeOfficialHistory.SetDataSource(data);
            employeeOfficialHistory.SetParameterValue("EmployeeId", persister.EmployeeID);
            employeeOfficialHistory.SetParameterValue("EmployeeName", persister.EmployeeName);
            employeeOfficialHistory.SetParameterValue("CompanyName", persister.CompanyName);
            employeeOfficialHistory.SetParameterValue("DateDuration", persister.Description);
            CrystalReportViewer1.ReportSource = employeeOfficialHistory;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["EmployeeOfficialHistory"] = employeeOfficialHistory;
        }
    }
}