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
    public partial class ItemWiseReceiveSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                ItemWiseReceiveSummaryR report = (ItemWiseReceiveSummaryR)Session["ItemWiseReceiveSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            ItemWiseReceiveSummaryR itemWiseReceiveSummaryR = new ItemWiseReceiveSummaryR();

            itemWiseReceiveSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\ItemWiseReceiveSummaryR.rpt");
            itemWiseReceiveSummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["ItemWiseRMReceiveSummaryParam"] as ReportParmPersister;
            List<ItemWiseReceiveSummaryReport> data = HttpContext.Current.Session["ItemWiseReceiveSummaryReportData"] as List<ItemWiseReceiveSummaryReport>;
            itemWiseReceiveSummaryR.SetDataSource(data);

            itemWiseReceiveSummaryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            itemWiseReceiveSummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = itemWiseReceiveSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ItemWiseReceiveSummaryDoc"] = itemWiseReceiveSummaryR;
        }
    }
}