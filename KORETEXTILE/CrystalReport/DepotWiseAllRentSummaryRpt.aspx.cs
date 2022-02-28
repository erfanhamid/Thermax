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
    public partial class DepotWiseAllRentSummaryRpt : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                DepotWiseAllRentSummaryR report = (DepotWiseAllRentSummaryR)Session["DepotWiseAllRentSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            DepotWiseAllRentSummaryR depotWiseAllRentSummaryR = new DepotWiseAllRentSummaryR();

            depotWiseAllRentSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\DepotWiseReceiveableSummaryR.rpt");
            depotWiseAllRentSummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["DepotWiseallRentSummaryParam"] as ReportParmPersister;
            List<DepotWiseAllRentSummaryReport> data = HttpContext.Current.Session["DepotWiseallRentSummaryData"] as List<DepotWiseAllRentSummaryReport>;
            depotWiseAllRentSummaryR.SetDataSource(data);

            depotWiseAllRentSummaryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            depotWiseAllRentSummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            CrystalReportViewer1.ReportSource = depotWiseAllRentSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["DepotWiseAllRentSummaryDoc"] = depotWiseAllRentSummaryR;
        }
    }
}