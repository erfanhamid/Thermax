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
    public partial class TrialBalanceR : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                TrialBalance report = (TrialBalance)Session["TrialBalanceDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            TrialBalance TLedger = new TrialBalance();
            TLedger.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\TrialBalance.rpt");
            TLedger.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["TrialBalanceparam"] as ReportParmPersister;
            List<TrialBalanceReport> data = HttpContext.Current.Session["TrialBalanceData"] as List<TrialBalanceReport>;
            TLedger.SetDataSource(data);

           // TLedger.SetParameterValue("compName", persister.CompName);
            //TLedger.SetParameterValue("compAddress", persister.CompAddress);
            TLedger.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            TLedger.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = TLedger;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["TrialBalanceDoc"] = TLedger;
        }
    }
}