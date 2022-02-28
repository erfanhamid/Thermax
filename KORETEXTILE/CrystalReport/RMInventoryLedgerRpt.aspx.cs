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
    public partial class RMInventoryLedgerRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                RMInventoryLedgerR report = (RMInventoryLedgerR)Session["RMInventoryLedgerDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            RMInventoryLedgerR rMInventoryLedgerR = new RMInventoryLedgerR();

            rMInventoryLedgerR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\RMInventoryLedgerR.rpt");
            rMInventoryLedgerR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["RMInventoryLedgerparam"] as ReportParmPersister;
            List<RMInventoryLedgerReport> data = HttpContext.Current.Session["RMInventoryLedgerData"] as List<RMInventoryLedgerReport>;
            rMInventoryLedgerR.SetDataSource(data);

            rMInventoryLedgerR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            rMInventoryLedgerR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            rMInventoryLedgerR.SetParameterValue("itmGroup", persister.ItemGroup);
            rMInventoryLedgerR.SetParameterValue("itmName", persister.ItemName);
            rMInventoryLedgerR.SetParameterValue("strName", persister.StoreName);


            CrystalReportViewer1.ReportSource = rMInventoryLedgerR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["RMInventoryLedgerDoc"] = rMInventoryLedgerR;
        }
    }
}