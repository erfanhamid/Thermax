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
    public partial class DealerIncentiveLedgerRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                DealerIncentiveLedgerR report = (DealerIncentiveLedgerR)Session["IncentiveSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            DealerIncentiveLedgerR Inc = new DealerIncentiveLedgerR();

            Inc.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\DealerIncentiveLedgerR.rpt");
            Inc.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["IncParam"] as ReportParmPersister;
            List<DealerIncentiveLedgerReport> data = HttpContext.Current.Session["IncData"] as List<DealerIncentiveLedgerReport>;
            Inc.SetDataSource(data);
            Inc.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            Inc.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            Inc.SetParameterValue("ReportName", Session["ReportName"]);
            Inc.SetParameterValue("Depot", persister.BranchName);
            CrystalReportViewer1.ReportSource = Inc;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["IncentiveLedgerDoc"] = Inc;
        }
    }
}