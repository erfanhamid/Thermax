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
    public partial class LATRPaymentRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                LATRPaymentR report = (LATRPaymentR)Session["LATRPaymentDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            LATRPaymentR LATRPayment = new LATRPaymentR();

            LATRPayment.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\LATRPaymentR.rpt");
            LATRPayment.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["LATRPaymentParam"] as ReportParmPersister;
            List<LATRPaymentReport> data = HttpContext.Current.Session["LATRPaymentData"] as List<LATRPaymentReport>;
            LATRPayment.SetDataSource(data);

            LATRPayment.SetParameterValue("compAddress", persister.CompAddress);
            LATRPayment.SetParameterValue("compName", persister.CompName);
            LATRPayment.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            LATRPayment.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = LATRPayment;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["LATRPaymentDoc"] = LATRPayment;
        }
    }
}