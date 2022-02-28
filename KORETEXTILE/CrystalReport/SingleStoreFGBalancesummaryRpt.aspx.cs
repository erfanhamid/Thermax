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
    public partial class SingleStoreFGBalancesummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleStoreFGBalancesummaryR report = (SingleStoreFGBalancesummaryR)Session["SingleStoreFGBalancesummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SingleStoreFGBalancesummaryR singleStoreFGBalancesummaryR = new SingleStoreFGBalancesummaryR();

            singleStoreFGBalancesummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SingleStoreFGBalancesummaryR.rpt");
            singleStoreFGBalancesummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SingleStoreFGBsummaryparam"] as ReportParmPersister;
            List<SingleStoreFGBsummaryReport> data = HttpContext.Current.Session["SingleStoreFGBsummaryData"] as List<SingleStoreFGBsummaryReport>;
            singleStoreFGBalancesummaryR.SetDataSource(data);

            singleStoreFGBalancesummaryR.SetParameterValue("asDate", persister.AsOnDate.ToString("dd-MM-yyyy"));
            //singleStoreRMBalanceSummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = singleStoreFGBalancesummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SingleStoreFGBalancesummaryDoc"] = singleStoreFGBalancesummaryR;
        }
    }
}