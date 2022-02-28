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
    public partial class UserWiseLogRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                UserWiseLogR report = (UserWiseLogR)Session["UserLogDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            UserWiseLogR log = new UserWiseLogR();
            log.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\UserWiseLogR.rpt");
            log.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["UserLogparam"] as ReportParmPersister;
            List<UserWiseLogReport> data = HttpContext.Current.Session["UserLogData"] as List<UserWiseLogReport>;
            log.SetDataSource(data);

            log.SetParameterValue("EmpID", persister.EmployeeID);
            log.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            log.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            log.SetParameterValue("EmpName", persister.EmployeeName);
            CrystalReportViewer1.ReportSource = log;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["UserLogDoc"] = log;
        }
    }
}