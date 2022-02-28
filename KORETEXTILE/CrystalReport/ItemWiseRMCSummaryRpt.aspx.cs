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
    public partial class ItemWiseRMCSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                ItemWiseRMCSummaryR report = (ItemWiseRMCSummaryR)Session["ItemWiseRMCSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            ItemWiseRMCSummaryR itemWiseRMCSummaryR = new ItemWiseRMCSummaryR();

            itemWiseRMCSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\ItemWiseRMIssueSummaryR.rpt");
            itemWiseRMCSummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["ItemWiseRMCSummaryParam"] as ReportParmPersister;
            List<ItemWiseRMCSummaryReport> data = HttpContext.Current.Session["ItemWiseRMCSummaryData"] as List<ItemWiseRMCSummaryReport>;
            itemWiseRMCSummaryR.SetDataSource(data);

            itemWiseRMCSummaryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            itemWiseRMCSummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = itemWiseRMCSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ItemWiseRMCSummaryDoc"] = itemWiseRMCSummaryR;
        }
    }
}