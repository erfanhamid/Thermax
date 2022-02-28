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
    public partial class DepotWiseReceiveableSummaryRpt : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                DepotWiseReceiveableSummaryR report = (DepotWiseReceiveableSummaryR)Session["DepotWiseReceiveableSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            DepotWiseReceiveableSummaryR itemS = new DepotWiseReceiveableSummaryR();

            itemS.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\DepotWiseReceiveableSummaryR.rpt");
            itemS.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["DepotWiseReceiveableSummaryParam"] as ReportParmPersister;
            List<DepotWiseReceiveableSummaryReport> data = HttpContext.Current.Session["DepotWiseReceiveableSummaryData"] as List<DepotWiseReceiveableSummaryReport>;
            itemS.SetDataSource(data);

            itemS.SetParameterValue("AsOn", persister.AsOnDate.ToString("dd-MM-yyyy"));
            CrystalReportViewer1.ReportSource = itemS;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["DepotWiseReceiveableSummaryDoc"] = itemS;
        }
    }
}