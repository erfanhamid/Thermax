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
    public partial class ItemWiseRMIssueSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                ItemWiseRMIssueSummaryR report = (ItemWiseRMIssueSummaryR)Session["ItemWiseIssueSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            ItemWiseRMIssueSummaryR itemWiseRMIssueSummaryR = new ItemWiseRMIssueSummaryR();

            itemWiseRMIssueSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\ItemWiseRMIssueSummaryR.rpt");
            itemWiseRMIssueSummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["ItemWiseRMIssueSummaryParam"] as ReportParmPersister;
            List<ItemWiseRMIssueSummaryReport> data = HttpContext.Current.Session["ItemWiseIssueSummaryReportData"] as List<ItemWiseRMIssueSummaryReport>;
            itemWiseRMIssueSummaryR.SetDataSource(data);

            itemWiseRMIssueSummaryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            itemWiseRMIssueSummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = itemWiseRMIssueSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ItemWiseIssueSummaryDoc"] = itemWiseRMIssueSummaryR;
        }
    }
}