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
    public partial class StockMovingAverageBalanceRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                StockMovingAverageBalanceR report = (StockMovingAverageBalanceR)Session["StoreMovingAVGBReportDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            StockMovingAverageBalanceR Sbalance = new StockMovingAverageBalanceR();

            Sbalance.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\StockMovingAverageBalanceR.rpt");
            Sbalance.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["MovingAvgBalanceParam"] as ReportParmPersister;
            List<StockMovingAverageBalanceReport> data = HttpContext.Current.Session["MovingAvgBalanceData"] as List<StockMovingAverageBalanceReport>;
            Sbalance.SetDataSource(data);

            Sbalance.SetParameterValue("compAddress", persister.CompAddress);
            Sbalance.SetParameterValue("compName", persister.CompName);
            Sbalance.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            Sbalance.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));




            CrystalReportViewer1.ReportSource = Sbalance;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["StoreMovingAVGBReportDoc"] = Sbalance;
        }
    }
}