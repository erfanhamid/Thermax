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
    public partial class ItemLedgerRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                ItemLedgerR report = (ItemLedgerR)Session["ReportDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            ItemLedgerR itemLedger = new ItemLedgerR();

            itemLedger.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\ItemLedgerR.rpt");
            itemLedger.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["ItemLedgerReportByDate"] as ReportParmPersister;
            List<ItemLedgerReport> data = HttpContext.Current.Session["ItemLedgerReportData"] as List<ItemLedgerReport>;
            itemLedger.SetDataSource(data);


            itemLedger.SetParameterValue("ItemID", persister.ItemID);
            itemLedger.SetParameterValue("ItemGroup", persister.ItemGroup);
            itemLedger.SetParameterValue("ItemName", persister.ItemName);
            itemLedger.SetParameterValue("Store", persister.StoreName);
            itemLedger.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            itemLedger.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = itemLedger;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ReportDoc"] = itemLedger;
        }
    }
}