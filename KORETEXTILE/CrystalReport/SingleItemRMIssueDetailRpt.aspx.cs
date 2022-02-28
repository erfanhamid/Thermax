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
    public partial class SingleItemRMIssueDetailRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleItemRMIssueDetailR report = (SingleItemRMIssueDetailR)Session["SingleItemRMIssueDetailDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SingleItemRMIssueDetailR singleItemRMIssueDetailR = new SingleItemRMIssueDetailR();

            singleItemRMIssueDetailR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SingleItemRMIssueDetailR.rpt");
            singleItemRMIssueDetailR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SingleItemRMIssueDetailparam"] as ReportParmPersister;
            List<SingleItemRMIssueDetailReport> data = HttpContext.Current.Session["SingleItemRMIssueDetailData"] as List<SingleItemRMIssueDetailReport>;
            singleItemRMIssueDetailR.SetDataSource(data);

            singleItemRMIssueDetailR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            singleItemRMIssueDetailR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            singleItemRMIssueDetailR.SetParameterValue("item", persister.ItemName);


            CrystalReportViewer1.ReportSource = singleItemRMIssueDetailR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SingleItemRMIssueDetailDoc"] = singleItemRMIssueDetailR;
        }
    }
}