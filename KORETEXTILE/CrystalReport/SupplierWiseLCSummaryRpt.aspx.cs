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
    public partial class SupplierWiseLCSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SupplierWiseLCSummaryR report = (SupplierWiseLCSummaryR)Session["SupplierWiseLCSummaryRDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SupplierWiseLCSummaryR supplierWiseLCSummaryR = new SupplierWiseLCSummaryR();

            supplierWiseLCSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SupplierWiseLCSummaryR.rpt");
            supplierWiseLCSummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SupplierWiseLCSummaryParam"] as ReportParmPersister;
            List<SupplierWiseLCSummary> data = HttpContext.Current.Session["SupplierWiseLCSummaryData"] as List<SupplierWiseLCSummary>;
            supplierWiseLCSummaryR.SetDataSource(data);

            supplierWiseLCSummaryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            supplierWiseLCSummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
           

            CrystalReportViewer1.ReportSource = supplierWiseLCSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SupplierWiseLCSummaryRDoc"] = supplierWiseLCSummaryR;
        }
    }
}