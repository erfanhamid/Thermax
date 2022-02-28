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
    public partial class DepotWiseSalesSumryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                DepotWiseSalesSummaryR report = (DepotWiseSalesSummaryR)Session["ReportDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            DepotWiseSalesSummaryR depotWiseSalesSummaryR = new DepotWiseSalesSummaryR();

            depotWiseSalesSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\DepotWiseSalesSummaryR.rpt");
            depotWiseSalesSummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["DepotWiseSalesSumryByDate"] as ReportParmPersister;
            List<DepotWiseSalesSumryReport> data = HttpContext.Current.Session["DepotWiseSalesSumryReportData"] as List<DepotWiseSalesSumryReport>;
            depotWiseSalesSummaryR.SetDataSource(data);


            depotWiseSalesSummaryR.SetParameterValue("address", "");
            depotWiseSalesSummaryR.SetParameterValue("compName", "");
            depotWiseSalesSummaryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            depotWiseSalesSummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = depotWiseSalesSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ReportDoc"] = depotWiseSalesSummaryR;
        }
    }
}