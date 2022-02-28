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
    public partial class InvoiceWiseSaleSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                InvoiceWiseSaleSummaryR report = (InvoiceWiseSaleSummaryR)Session["ReportDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            InvoiceWiseSaleSummaryR invoiceWiseSaleSummaryR = new InvoiceWiseSaleSummaryR();

            invoiceWiseSaleSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\InvoiceWiseSaleSummaryR.rpt");
            invoiceWiseSaleSummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["InvoiceWiseSaleSummaryByDate"] as ReportParmPersister;
            List<InvoiceWiseSaleSummaryReport> data = HttpContext.Current.Session["InvoiceWiseSaleSummaryReportData"] as List<InvoiceWiseSaleSummaryReport>;
            invoiceWiseSaleSummaryR.SetDataSource(data);


            invoiceWiseSaleSummaryR.SetParameterValue("address", "");
            invoiceWiseSaleSummaryR.SetParameterValue("compName", "");
            invoiceWiseSaleSummaryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            invoiceWiseSaleSummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = invoiceWiseSaleSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ReportDoc"] = invoiceWiseSaleSummaryR;
        }
    }
}