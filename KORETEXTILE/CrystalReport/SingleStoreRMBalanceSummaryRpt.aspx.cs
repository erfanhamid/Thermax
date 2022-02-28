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
    public partial class SingleStoreRMBalanceSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleStoreRMBalanceSummaryR report = (SingleStoreRMBalanceSummaryR)Session["SingleStoreRMBalanceSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SingleStoreRMBalanceSummaryR singleStoreRMBalanceSummaryR = new SingleStoreRMBalanceSummaryR();

            singleStoreRMBalanceSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SingleStoreRMBalanceSummaryR.rpt");
            singleStoreRMBalanceSummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SingleStoreRMBalanceparam"] as ReportParmPersister;
            List<SingleStoreRMBSummaryReport> data = HttpContext.Current.Session["SingleStoreRMBalanceSummaryData"] as List<SingleStoreRMBSummaryReport>;
            singleStoreRMBalanceSummaryR.SetDataSource(data);

            singleStoreRMBalanceSummaryR.SetParameterValue("asDate", persister.AsOnDate.ToString("dd-MM-yyyy"));
            //singleStoreRMBalanceSummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = singleStoreRMBalanceSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SingleStoreRMBalanceSummaryDoc"] = singleStoreRMBalanceSummaryR;
        }
    }
}