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
    public partial class IssueReceiveDetailsRpt : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                IssueReceiveDetailsR report = (IssueReceiveDetailsR)Session["IssueReceiveDetailsDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            IssueReceiveDetailsR issueReceiveDetailsR = new IssueReceiveDetailsR();

            issueReceiveDetailsR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\IssueReceiveDetailsR.rpt");
            issueReceiveDetailsR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["IssueReceiveDetailsParam"] as ReportParmPersister;
            List<IssueReceiveDetailsReport> data = HttpContext.Current.Session["IssueReceiveDetailsData"] as List<IssueReceiveDetailsReport>;
            issueReceiveDetailsR.SetDataSource(data);

            issueReceiveDetailsR.SetParameterValue("address", persister.CompAddress);
            issueReceiveDetailsR.SetParameterValue("compName", persister.CompName);
            issueReceiveDetailsR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            issueReceiveDetailsR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            issueReceiveDetailsR.SetParameterValue("storeName", persister.StoreName);

            CrystalReportViewer1.ReportSource = issueReceiveDetailsR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["IssueReceiveDetailsDoc"] = issueReceiveDetailsR;
        }
    }
}