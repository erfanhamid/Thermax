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
    public partial class StockSummaryRpt : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                StockSummaryR report = (StockSummaryR)Session["StockSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            StockSummaryR stockSummaryR = new StockSummaryR();

            stockSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\StockSummaryR.rpt");
            stockSummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["StockSummaryReportByDate"] as ReportParmPersister;
            List<StockSummaryReport> data = HttpContext.Current.Session["StockSummaryReportData"] as List<StockSummaryReport>;
            stockSummaryR.SetDataSource(data);

            stockSummaryR.SetParameterValue("address", "");
            stockSummaryR.SetParameterValue("compName", "");
            stockSummaryR.SetParameterValue("Store", persister.StoreName);
            stockSummaryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            stockSummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = stockSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["StockSummaryDoc"] = stockSummaryR;
        }
    }
}