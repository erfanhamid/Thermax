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
    public partial class RMStockBalanceStoreWiseRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                RMStockBalanceStoreWiseR report = (RMStockBalanceStoreWiseR)Session["RMStockBalanceStoreWiseDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            RMStockBalanceStoreWiseR RMBS = new RMStockBalanceStoreWiseR();

            RMBS.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\RMStockBalanceStoreWiseR.rpt");
            RMBS.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["RMStockBalanceStoreWiseParam"] as ReportParmPersister;
            List<RMStockBalanceStoreWiseReport> data = HttpContext.Current.Session["RMStockBalanceStoreWiseData"] as List<RMStockBalanceStoreWiseReport>;
            RMBS.SetDataSource(data);

            RMBS.SetParameterValue("compAddress", persister.CompAddress);
            RMBS.SetParameterValue("compName", persister.CompName);
            RMBS.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            RMBS.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = RMBS;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["RMStockBalanceStoreWiseDoc"] = RMBS;
        }
    }
}