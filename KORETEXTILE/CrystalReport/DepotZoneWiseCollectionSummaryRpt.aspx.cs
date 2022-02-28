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
    public partial class DepotZoneWiseCollectionSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                DepotZoneWiseCollectionSummaryR report = (DepotZoneWiseCollectionSummaryR)Session["DepotZoneWiseCollectionSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }

        public void ShowReport()
        {
            DepotZoneWiseCollectionSummaryR itemS = new DepotZoneWiseCollectionSummaryR();

            itemS.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\DepotZoneWiseCollectionSummaryR.rpt");
            itemS.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["DepotZoneParam"] as ReportParmPersister;
            List<DepotZoneWiseCollectionSummary> data = HttpContext.Current.Session["DepotZoneData"] as List<DepotZoneWiseCollectionSummary>;
            itemS.SetDataSource(data);

            itemS.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            itemS.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = itemS;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["DepotZoneWiseCollectionSummaryDoc"] = itemS;
        }
    }
}