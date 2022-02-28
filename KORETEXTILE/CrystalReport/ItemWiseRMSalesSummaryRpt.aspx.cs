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
    public partial class ItemWiseRMSalesSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                ItemWiseRMSalesSummaryR report = (ItemWiseRMSalesSummaryR)Session["ItemWiseRMSalesSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            ItemWiseRMSalesSummaryR itemWiseRMSalesSummaryR = new ItemWiseRMSalesSummaryR();

            itemWiseRMSalesSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\itemWiseRMSalesSummaryR.rpt");
            itemWiseRMSalesSummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["ItemWiseRMSalesSummaryParam"] as ReportParmPersister;
            List<ItemWiseRMSalesSummaryReport> data = HttpContext.Current.Session["ItemWiseRMSalesSummaryData"] as List<ItemWiseRMSalesSummaryReport>;
            itemWiseRMSalesSummaryR.SetDataSource(data);

            itemWiseRMSalesSummaryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            itemWiseRMSalesSummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = itemWiseRMSalesSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ItemWiseRMSalesSummaryDoc"] = itemWiseRMSalesSummaryR;
        }
    }
}