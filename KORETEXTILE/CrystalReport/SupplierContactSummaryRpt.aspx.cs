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
    public partial class SupplierContactSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SupplierContactSummaryR report = (SupplierContactSummaryR)Session["SuppliercontactSession"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SupplierContactSummaryR supplierContactSummaryR = new SupplierContactSummaryR();

            supplierContactSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SupplierContactSummaryR.rpt");
            supplierContactSummaryR.Load(APPPATH);

            List<SupplierContactSummaryReport> data = HttpContext.Current.Session["SupplierContactSummaryData"] as List<SupplierContactSummaryReport>;
            supplierContactSummaryR.SetDataSource(data);


            CrystalReportViewer1.ReportSource = supplierContactSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SuppliercontactSession"] = supplierContactSummaryR;
        }
    }
}