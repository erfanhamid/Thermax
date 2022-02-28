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
    public partial class SupplierWisePaymentSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SupplierWisePaymentSummaryR report = (SupplierWisePaymentSummaryR)Session["SupplierWisePaymentSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SupplierWisePaymentSummaryR supplierWisePaymentSummaryR = new SupplierWisePaymentSummaryR();

            supplierWisePaymentSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SupplierWisePaymentSummaryR.rpt");
            supplierWisePaymentSummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SupplierWisePaymentSummaryParam"] as ReportParmPersister;
            List<SupplierWisePaymentSummaryReport> data = HttpContext.Current.Session["SupplierWisePaymentSummaryReportData"] as List<SupplierWisePaymentSummaryReport>;
            supplierWisePaymentSummaryR.SetDataSource(data);

            supplierWisePaymentSummaryR.SetParameterValue("address", persister.CompAddress);
            supplierWisePaymentSummaryR.SetParameterValue("compName", persister.CompName);
            supplierWisePaymentSummaryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            supplierWisePaymentSummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = supplierWisePaymentSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SupplierWisePaymentSummaryDoc"] = supplierWisePaymentSummaryR;
        }
    }
}