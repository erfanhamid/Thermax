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
    public partial class UserModuelWiseRoleListRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                UserModuleWiseRoleListR report = (UserModuleWiseRoleListR)Session["UserModuleWiseRoleListDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            UserModuleWiseRoleListR RS = new UserModuleWiseRoleListR();
            RS.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\UserModuleWiseRoleListR.rpt");
            RS.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["UserModuelWiseRoleListParam"] as ReportParmPersister;
            List<UserModuleWiseRoleListReport> data = HttpContext.Current.Session["UserModuelWiseRoleListData"] as List<UserModuleWiseRoleListReport>;
            RS.SetDataSource(data);

            RS.SetParameterValue("compName", persister.CompName);
            RS.SetParameterValue("compAddress", persister.CompAddress);
            //RS.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            //RS.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            //RS.SetParameterValue("reportName", Session["ReportName"]);
            CrystalReportViewer1.ReportSource = RS;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["UserModuleWiseRoleListDoc"] = RS;
        }
    }
}