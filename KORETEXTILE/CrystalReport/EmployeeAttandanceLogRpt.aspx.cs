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
    public partial class EmployeeAttandanceLogRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                EmpoyeeAttandanceLogR report = (EmpoyeeAttandanceLogR)Session["EmpAttandanceLog"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            EmpoyeeAttandanceLogR empAttandance = new EmpoyeeAttandanceLogR();
            empAttandance.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\EmployeeAttandanceLogR.rpt");
            empAttandance.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["EmpAttandanceLogParam"] as ReportParmPersister;
            List<EmployeeAttandanceLog> data = HttpContext.Current.Session["EmpAttandanceLogData"] as List<EmployeeAttandanceLog>;
            empAttandance.SetDataSource(data);

            empAttandance.SetParameterValue("compName", persister.CompName);
            empAttandance.SetParameterValue("compAddress", persister.CompAddress);
            empAttandance.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            empAttandance.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = empAttandance;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["EmpAttandanceLog"] = empAttandance;
        }
    }
}