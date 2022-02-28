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
    public partial class SingleDepotAllRentSummaryRpt : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleDepotAllRentSummaryR report = (SingleDepotAllRentSummaryR)Session["SingleDepotAllRentSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SingleDepotAllRentSummaryR itemS = new SingleDepotAllRentSummaryR();

            itemS.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\SingleDepotAllRentSummaryR.rpt");
            itemS.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SingleDepotAllRentSummaryParam"] as ReportParmPersister;
            List<SingleDepotAllRentSummaryReport> data = HttpContext.Current.Session["SingleDepotAllRentSummaryData"] as List<SingleDepotAllRentSummaryReport>;
            itemS.SetDataSource(data);

            itemS.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            itemS.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            CrystalReportViewer1.ReportSource = itemS;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SingleDepotAllRentSummaryDoc"] = itemS;
        }
    }
}