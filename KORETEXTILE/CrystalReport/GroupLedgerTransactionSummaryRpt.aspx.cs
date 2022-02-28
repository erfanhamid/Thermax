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
    public partial class GroupLedgerTransactionSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                GroupLedgerTransactionSummaryR report = (GroupLedgerTransactionSummaryR)Session["GroupLedgerTransactionSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            GroupLedgerTransactionSummaryR grpledger = new GroupLedgerTransactionSummaryR();
            grpledger.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\GroupLedgerTransactionSummaryR.rpt");
            grpledger.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["GroupLedgerTransactionSummaryparam"] as ReportParmPersister;
            List<GroupLedgerTransactionSummaryReport> data = HttpContext.Current.Session["GroupLedgerTransactionSummaryData"] as List<GroupLedgerTransactionSummaryReport>;
            grpledger.SetDataSource(data);

            grpledger.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            grpledger.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));



            CrystalReportViewer1.ReportSource = grpledger;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["GroupLedgerTransactionSummaryDoc"] = grpledger;
        }
    }
}