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
    public partial class SupplierBalanceSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SupplierBalanceSummaryR report = (SupplierBalanceSummaryR)Session["SupplierBalanceSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SupplierBalanceSummaryR supbalanceSummary = new SupplierBalanceSummaryR();
            supbalanceSummary.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\SupplierBalanceSummaryR.rpt");
            supbalanceSummary.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SupbalanceSumparam"] as ReportParmPersister;
            List<SupplierBalanceDetailsReport> data = HttpContext.Current.Session["SupbalanceSumData"] as List<SupplierBalanceDetailsReport>;
            supbalanceSummary.SetDataSource(data);

            supbalanceSummary.SetParameterValue("SupplierGroup", persister.SupplierGroup);
            supbalanceSummary.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            supbalanceSummary.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));



            CrystalReportViewer1.ReportSource = supbalanceSummary;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SupplierBalanceSummaryDoc"] = supbalanceSummary;
        }
    }
}