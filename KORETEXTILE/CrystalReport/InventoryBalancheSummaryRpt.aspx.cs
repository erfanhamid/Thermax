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
    public partial class InventoryBalancheSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                InventoryBalancheSummaryR report = (InventoryBalancheSummaryR)Session["InventoryBalancheSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            InventoryBalancheSummaryR inventoryBalancheSummaryR = new InventoryBalancheSummaryR();

            inventoryBalancheSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\InventoryBalancheSummaryR.rpt");
            inventoryBalancheSummaryR.Load(APPPATH);

            List<InventoryBalancheSummaryReport> data = HttpContext.Current.Session["InventoryBalancheSummaryData"] as List<InventoryBalancheSummaryReport>;
            inventoryBalancheSummaryR.SetDataSource(data);

            ReportParmPersister persister = new ReportParmPersister();
            inventoryBalancheSummaryR.SetParameterValue("address", persister.CompAddress);
            inventoryBalancheSummaryR.SetParameterValue("compName", persister.CompName);

            CrystalReportViewer1.ReportSource = inventoryBalancheSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["InventoryBalancheSummaryDoc"] = inventoryBalancheSummaryR;
        }
    }
}