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
    public partial class ItemWiseSalesSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                ItemWiseSalesSummaryR report = (ItemWiseSalesSummaryR)Session["ReportDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            ItemWiseSalesSummaryR itemWiseSalesSummaryR = new ItemWiseSalesSummaryR();

            itemWiseSalesSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\ItemWiseSalesSummaryR.rpt");
            itemWiseSalesSummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["ItemWiseSalesSummary"] as ReportParmPersister;
            List<ItemWiseSalesSummaryReport> data = HttpContext.Current.Session["ItemWiseSalesSummaryData"] as List<ItemWiseSalesSummaryReport>;
            itemWiseSalesSummaryR.SetDataSource(data);

            itemWiseSalesSummaryR.SetParameterValue("address", persister.CompAddress);
            itemWiseSalesSummaryR.SetParameterValue("compName", persister.CompName);
            itemWiseSalesSummaryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            itemWiseSalesSummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            itemWiseSalesSummaryR.SetParameterValue("depot", persister.BranchName);

            CrystalReportViewer1.ReportSource = itemWiseSalesSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ReportDoc"] = itemWiseSalesSummaryR;
        }
    }
}