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
    public partial class SingleDepotDealerCollectionSummaryRpt : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleDepotCollectionSummaryR report = (SingleDepotCollectionSummaryR)Session["CollectionDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SingleDepotCollectionSummaryR collection = new SingleDepotCollectionSummaryR();
            collection.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\SingleDepotCollectionSummaryR.rpt");
            collection.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["CollectionParam"] as ReportParmPersister;
            List<SingleDepotCollectionSummary> data = HttpContext.Current.Session["CollectionData"] as List<SingleDepotCollectionSummary>;
            collection.SetDataSource(data);

            collection.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            collection.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = collection;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["CollectionDoc"] = collection;
        }
    }
}