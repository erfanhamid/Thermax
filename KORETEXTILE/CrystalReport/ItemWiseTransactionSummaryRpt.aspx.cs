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
    public partial class ItemWiseTransactionSummaryRpt : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                ItemWiseTransactionSummaryR report = (ItemWiseTransactionSummaryR)Session["ItemWiseTransactionSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            ItemWiseTransactionSummaryR itemWiseTransactionSummaryR = new ItemWiseTransactionSummaryR();

            itemWiseTransactionSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\ItemWiseTransactionSummaryR.rpt");
            itemWiseTransactionSummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["ItemWiseTransactionSummaryParam"] as ReportParmPersister;
            List<ItemWiseTransactionSummaryReport> data = HttpContext.Current.Session["ItemWiseTransactionSummaryData"] as List<ItemWiseTransactionSummaryReport>;
            itemWiseTransactionSummaryR.SetDataSource(data);

            itemWiseTransactionSummaryR.SetParameterValue("address", persister.CompAddress);
            itemWiseTransactionSummaryR.SetParameterValue("compName", persister.CompName);
            itemWiseTransactionSummaryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            itemWiseTransactionSummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = itemWiseTransactionSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ItemWiseTransactionSummaryDoc"] = itemWiseTransactionSummaryR;
        }
    }
}