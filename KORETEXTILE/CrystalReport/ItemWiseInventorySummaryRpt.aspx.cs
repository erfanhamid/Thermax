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
    public partial class ItemWiseInventorySummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                ItemWiseInventorySummaryR report = (ItemWiseInventorySummaryR)Session["ReportDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            ItemWiseInventorySummaryR item = new ItemWiseInventorySummaryR();

            item.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\ItemWiseInventorySummaryR.rpt");
            item.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["ItemParam"] as ReportParmPersister;
            List<ItemWiseInventorySummaryReport> data = HttpContext.Current.Session["ItemReportData"] as List<ItemWiseInventorySummaryReport>;
            item.SetDataSource(data);


            //item.SetParameterValue("address", "");
            //item.SetParameterValue("compName", "");
            item.SetParameterValue("AsDate", persister.AsOnDate.ToString("dd-MM-yyyy"));
       

            CrystalReportViewer1.ReportSource = item;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ReportDoc"] = item;
        }
    }
}