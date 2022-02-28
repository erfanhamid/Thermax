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
    public partial class MonthWiseAttendanceSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                MonthWiseAttendanceSummaryR report = (MonthWiseAttendanceSummaryR)Session["MonthWiseAttendance"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            MonthWiseAttendanceSummaryR monthwiseattendance = new MonthWiseAttendanceSummaryR();//crystalreport
            monthwiseattendance.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\MonthWiseAttendanceSummaryR.rpt");
            monthwiseattendance.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["MonthWiseAttendanceParam"] as ReportParmPersister;
            List<MonthWiseAttendanceSummary> data = HttpContext.Current.Session["MonthWiseAttendanceSummary"] as List<MonthWiseAttendanceSummary>;
            monthwiseattendance.SetDataSource(data);

            monthwiseattendance.SetParameterValue("Company", persister.CompanyName);
            monthwiseattendance.SetParameterValue("YearMonth", persister.Month);
            

            CrystalReportViewer1.ReportSource = monthwiseattendance;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["MonthWiseAttendance"] = monthwiseattendance;
        }
    }
}