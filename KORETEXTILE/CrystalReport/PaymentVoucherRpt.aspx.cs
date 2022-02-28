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
    public partial class PaymentVoucherRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                PaymentVoucherR report = (PaymentVoucherR)Session["PaymentVoucherDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            PaymentVoucherR paymentVoucherR = new PaymentVoucherR();
            paymentVoucherR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\PaymentVoucherR.rpt");
            paymentVoucherR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["PaymentVoucherParam"] as ReportParmPersister;
            List<PaymentVoucherReport> data = HttpContext.Current.Session["PaymentVoucherReportData"] as List<PaymentVoucherReport>;
            paymentVoucherR.SetDataSource(data);

            paymentVoucherR.SetParameterValue("address", persister.CompAddress);
            paymentVoucherR.SetParameterValue("compName", persister.CompName);
            paymentVoucherR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            paymentVoucherR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = paymentVoucherR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["PaymentVoucherDoc"] = paymentVoucherR;
        }
    }
}