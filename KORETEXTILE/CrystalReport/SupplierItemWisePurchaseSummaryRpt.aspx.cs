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
    public partial class SupplierItemWisePurchaseSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SupplierItemWisePurchaseSummaryR report = (SupplierItemWisePurchaseSummaryR)Session["SupplierItemWisePurchaseSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SupplierItemWisePurchaseSummaryR spItem = new SupplierItemWisePurchaseSummaryR();
            spItem.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\SupplierItemWisePurchaseSummaryR.rpt");
            spItem.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SupplierItemWisePurchaseSummaryparam"] as ReportParmPersister;
            List<SupplierItemWisePurchaseSummaryReport> data = HttpContext.Current.Session["SupplierItemWisePurchaseSummaryData"] as List<SupplierItemWisePurchaseSummaryReport>;
            spItem.SetDataSource(data);

            spItem.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            spItem.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));



            CrystalReportViewer1.ReportSource = spItem;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SupplierItemWisePurchaseSummaryDoc"] = spItem;
        }
    }
}