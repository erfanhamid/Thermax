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
    public partial class DealerWiseSalesSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                DealerWiseSalesSummaryR report = (DealerWiseSalesSummaryR)Session["DealerWiseSalesSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            DealerWiseSalesSummaryR salesSummary = new DealerWiseSalesSummaryR();
            salesSummary.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\DealerWiseSalesSummaryR.rpt");
            salesSummary.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SalesSummaryParam"] as ReportParmPersister;
            List<DealerWiseSalesSummaryReport> data = HttpContext.Current.Session["SalesSummaryData"] as List<DealerWiseSalesSummaryReport>;
            salesSummary.SetDataSource(data);

            //vExpense.SetParameterValue("accountName", persister.AccountName);
            salesSummary.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            salesSummary.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            //salesDetails.SetParameterValue("BranchName", persister.BranchName);

            CrystalReportViewer1.ReportSource = salesSummary;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["DealerWiseSalesSummaryDoc"] = salesSummary;
        }
    }
}