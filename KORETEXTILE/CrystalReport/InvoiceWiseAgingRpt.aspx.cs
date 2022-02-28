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
    public partial class InvoiceWiseAgingRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                InvoiceWiseAgingR report = (InvoiceWiseAgingR)Session["ReportDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            InvoiceWiseAgingR invoiceWiseAgingR = new InvoiceWiseAgingR();

            invoiceWiseAgingR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\InvoiceWiseAgingR.rpt");
            invoiceWiseAgingR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["InvoiceWiseAging"] as ReportParmPersister;
            List<InvoiceWiseAgingReport> data = HttpContext.Current.Session["InvoiceWiseAgingReportData"] as List<InvoiceWiseAgingReport>;
            invoiceWiseAgingR.SetDataSource(data);

            invoiceWiseAgingR.SetParameterValue("address", persister.CompAddress);
            invoiceWiseAgingR.SetParameterValue("compName", persister.CompName);
            invoiceWiseAgingR.SetParameterValue("asOnDate", persister.AsOnDate.ToString("dd-MM-yyyy"));
            invoiceWiseAgingR.SetParameterValue("custId", persister.CustomerId);
            invoiceWiseAgingR.SetParameterValue("custName", persister.CustName);

            CrystalReportViewer1.ReportSource = invoiceWiseAgingR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ReportDoc"] = invoiceWiseAgingR;
        }
    }
}