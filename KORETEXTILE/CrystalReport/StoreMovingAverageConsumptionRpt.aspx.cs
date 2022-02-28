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
    public partial class StoreMovingAverageConsumptionRpt : System.Web.UI.Page
    {

        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                StockMovingAverageConsumptionR report = (StockMovingAverageConsumptionR)Session["StoreMovingAVGCReportDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            StockMovingAverageConsumptionR SConsumption = new StockMovingAverageConsumptionR();

            SConsumption.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\StockMovingAverageConsumptionR.rpt");
            SConsumption.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["MovingAvgConsumptionParam"] as ReportParmPersister;
            List<StockMovingAverageConsumptionReport> data = HttpContext.Current.Session["MovingAvgConsumptionData"] as List<StockMovingAverageConsumptionReport>;
            SConsumption.SetDataSource(data);

            SConsumption.SetParameterValue("compAddress", persister.CompAddress);
            SConsumption.SetParameterValue("compName", persister.CompName);
            SConsumption.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            SConsumption.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));




            CrystalReportViewer1.ReportSource = SConsumption;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["StoreMovingAVGCReportDoc"] = SConsumption;
        }
    }
}