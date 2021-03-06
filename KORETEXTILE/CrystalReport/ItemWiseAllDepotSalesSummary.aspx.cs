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
    public partial class ItemWiseAllDepotSalesSummary : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                ItemWiseAllDepotSalesSummaryR report = (ItemWiseAllDepotSalesSummaryR)Session["ItemWiseAllDepotSalesSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            ItemWiseAllDepotSalesSummaryR itemS = new ItemWiseAllDepotSalesSummaryR();

            itemS.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\ItemWiseAllDepotSalesSummaryR.rpt");
            itemS.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["ItemParam"] as ReportParmPersister;
            List<ItemWiseAllDepotSalesSummaryReport> data = HttpContext.Current.Session["ItemData"] as List<ItemWiseAllDepotSalesSummaryReport>;
            itemS.SetDataSource(data);

            itemS.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            itemS.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = itemS;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ItemWiseAllDepotSalesSummaryDoc"] = itemS;
        }
    }
}