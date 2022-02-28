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
    public partial class StockPositionRawMaterials : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                StockPositionRMR report = (StockPositionRMR)Session["ReportDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            StockPositionRMR stockPositionRMR = new StockPositionRMR();

            stockPositionRMR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\StockPositionRMR.rpt");
            stockPositionRMR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["StockPositionRMOnDate"] as ReportParmPersister;
            List<FGStatusReport> data = HttpContext.Current.Session["StockPositionRMReportData"] as List<FGStatusReport>;
            stockPositionRMR.SetDataSource(data);


            stockPositionRMR.SetParameterValue("address", "");
            stockPositionRMR.SetParameterValue("compName", "");
            stockPositionRMR.SetParameterValue("AsOnDate", persister.AsOnDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = stockPositionRMR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ReportDoc"] = stockPositionRMR;
        }
    }
}