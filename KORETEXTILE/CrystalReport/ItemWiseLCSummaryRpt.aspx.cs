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
    public partial class ItemWiseLCSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                ItemWiseLCSummaryR report = (ItemWiseLCSummaryR)Session["ItemWiseLCSummaryRDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            ItemWiseLCSummaryR itemWiseLCSummaryR = new ItemWiseLCSummaryR();

            itemWiseLCSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\ItemWiseLCSummaryR.rpt");
            itemWiseLCSummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["ItemWiseLCSummaryParam"] as ReportParmPersister;
            List<ItemWiseLCSummary> data = HttpContext.Current.Session["ItemWiseLCSummaryData"] as List<ItemWiseLCSummary>;
            itemWiseLCSummaryR.SetDataSource(data);

            itemWiseLCSummaryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            itemWiseLCSummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = itemWiseLCSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ItemWiseLCSummaryRDoc"] = itemWiseLCSummaryR;
        }
    }
}