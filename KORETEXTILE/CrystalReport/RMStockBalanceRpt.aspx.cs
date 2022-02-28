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
    public partial class RMStockBalanceRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                RMStockBalanceR report = (RMStockBalanceR)Session["RMStockBalanceDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            RMStockBalanceR RMB = new RMStockBalanceR();

            RMB.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\RMConsumptionR.rpt");
            RMB.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["RMStockBalanceParam"] as ReportParmPersister;
            List<RMStockBalanceReport> data = HttpContext.Current.Session["RMStockBalanceData"] as List<RMStockBalanceReport>;
            RMB.SetDataSource(data);

            RMB.SetParameterValue("compAddress", persister.CompAddress);
            RMB.SetParameterValue("compName", persister.CompName);
            RMB.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            RMB.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = RMB;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["RMStockBalanceDoc"] = RMB;
        }
    }
}