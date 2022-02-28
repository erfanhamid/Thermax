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
    public partial class AccountLedger : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                AccountsLedgerR report = (AccountsLedgerR)Session["AccountLedgerDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            AccountsLedgerR custLedger = new AccountsLedgerR();
            custLedger.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\AccountsLedgerR.rpt");
            custLedger.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["AccountLedgerparam"] as ReportParmPersister;
            List<AccountsLedgerReport> data = HttpContext.Current.Session["AccountLedgerReportData"] as List<AccountsLedgerReport>;
            custLedger.SetDataSource(data);

            custLedger.SetParameterValue("accountName", persister.AccountName);
            custLedger.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            custLedger.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            custLedger.SetParameterValue("rootAcType", persister.RootAccountType);
            

            CrystalReportViewer1.ReportSource = custLedger;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["AccountLedgerDoc"] = custLedger;
        }
    }
}