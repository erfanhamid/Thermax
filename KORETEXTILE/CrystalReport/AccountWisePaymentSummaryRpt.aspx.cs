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
    public partial class AccountWisePaymentSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                AccountWisePaymentSummaryR report = (AccountWisePaymentSummaryR)Session["AccountWisePaymentSummaryRDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            AccountWisePaymentSummaryR accountWisePaymentSummaryR = new AccountWisePaymentSummaryR();

            accountWisePaymentSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\AccountWisePaymentSummaryR.rpt");
            accountWisePaymentSummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["AccountWisePaymentSummaryParam"] as ReportParmPersister;
            List<AccountWisePaymentSummaryReport> data = HttpContext.Current.Session["AccountWisePaymentSummaryReportData"] as List<AccountWisePaymentSummaryReport>;
            accountWisePaymentSummaryR.SetDataSource(data);

            accountWisePaymentSummaryR.SetParameterValue("address", persister.CompAddress);
            accountWisePaymentSummaryR.SetParameterValue("compName", persister.CompName);
            accountWisePaymentSummaryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            accountWisePaymentSummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = accountWisePaymentSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["AccountWisePaymentSummaryRDoc"] = accountWisePaymentSummaryR;
        }
    }
}