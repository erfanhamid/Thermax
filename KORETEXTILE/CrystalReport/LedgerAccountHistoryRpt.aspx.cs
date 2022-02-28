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
    public partial class LedgerAccountHistoryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                LedgerAccountHistoryR report = (LedgerAccountHistoryR)Session["LedgerAccountHistoryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            LedgerAccountHistoryR accountHistory = new LedgerAccountHistoryR();
            accountHistory.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\LedgerAccountHistoryR.rpt");
            accountHistory.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["lahistoryParam"] as ReportParmPersister;
            List<AccountHistoryReport> data = HttpContext.Current.Session["lahistoryData"] as List<AccountHistoryReport>;
            accountHistory.SetDataSource(data);

            //vExpense.SetParameterValue("accountName", persister.AccountName);
            accountHistory.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            accountHistory.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            accountHistory.SetParameterValue("costCenterName", persister.CostCenterName);
            accountHistory.SetParameterValue("accName", persister.AccountName.ToString());

            CrystalReportViewer1.ReportSource = accountHistory;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["LedgerAccountHistoryDoc"] = accountHistory;
        }
    }
}