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
    public partial class IssueReceiveSumamryRpt : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                IssueReceiveSumamryR report = (IssueReceiveSumamryR)Session["issueReceiveSumamryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            IssueReceiveSumamryR issueReceiveSumamryR = new IssueReceiveSumamryR();

            issueReceiveSumamryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\EmpInfoDetails.rpt");
            issueReceiveSumamryR.Load(APPPATH);

            List<IssueReceiveSumamryReport> data = HttpContext.Current.Session["IssueReceiveSumamryData"] as List<IssueReceiveSumamryReport>;
            issueReceiveSumamryR.SetDataSource(data);
            ReportParmPersister persister = HttpContext.Current.Session["IssueReceiveSumamryParam"] as ReportParmPersister;
            issueReceiveSumamryR.SetParameterValue("tDate", persister.ToDate);
            issueReceiveSumamryR.SetParameterValue("fDate", persister.FromDate);
            issueReceiveSumamryR.SetParameterValue("address", persister.CompAddress);
            issueReceiveSumamryR.SetParameterValue("compName", persister.CompName);
            issueReceiveSumamryR.SetParameterValue("storeName",persister.StoreName);

            CrystalReportViewer1.ReportSource = issueReceiveSumamryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["issueReceiveSumamryDoc"] = issueReceiveSumamryR;
        }
    }
}