using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.ViewModel.Sales.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BEEERP.CrystalReport
{
    public partial class CustomerSalesDetails : System.Web.UI.Page
    {

        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                CustWiseSalesDetails report = (CustWiseSalesDetails)Session["CustWiseSalesDetailsDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            CustWiseSalesDetails custLedger = new CustWiseSalesDetails();

            custLedger.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);

            string repotPath = "";

            List<DepotAndDealerWiseIncentiveSummaryReport> data = HttpContext.Current.Session["CustWiseSalesDetailsList"] as List<DepotAndDealerWiseIncentiveSummaryReport>;

            ReportParmPersister persister = HttpContext.Current.Session["CustWiseSalesDetailsParam"] as ReportParmPersister;

            if (HttpContext.Current.Session["ReportName"] != null)
            {
                repotPath = @"\ReportRdlc\" + HttpContext.Current.Session["ReportName"].ToString() + ".rpt";
                custLedger.Load(Server.MapPath(repotPath));
            }

            custLedger.SetDataSource(data);

            custLedger.SetParameterValue("fDate", persister.FromDate.ToString("dd-MMM-yyyy"));
            custLedger.SetParameterValue("tDate", persister.ToDate.ToString("dd-MMM-yyyy"));
            custLedger.SetParameterValue("BranchName", persister.BranchName);

            CrystalReportViewer1.ReportSource = custLedger;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["CustWiseSalesDetailsDoc"] = custLedger;
        }
    }
}