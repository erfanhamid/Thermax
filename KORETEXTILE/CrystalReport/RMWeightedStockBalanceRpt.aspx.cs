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
    public partial class RMWeightedStockBalanceRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                RMWeightedStockBalanceR report = (RMWeightedStockBalanceR)Session["RMWeightedStockBalanceRDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            RMWeightedStockBalanceR RMW = new RMWeightedStockBalanceR();

            RMW.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\RMWeightedStockBalanceR.rpt");
            RMW.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["RMWeightedStockBalanceParam"] as ReportParmPersister;
            List<RMWeightedStockBalanceReport> data = HttpContext.Current.Session["RMWeightedStockBalanceData"] as List<RMWeightedStockBalanceReport>;
            RMW.SetDataSource(data);

            RMW.SetParameterValue("compAddress", persister.CompAddress);
            RMW.SetParameterValue("compName", persister.CompName);
            RMW.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            RMW.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = RMW;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["RMWeightedStockBalanceRDoc"] = RMW;
        }
    }
}