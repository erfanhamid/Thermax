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
    public partial class AllSupplierPaymentVouchersRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                AllSupplierPaymentVouchersR report = (AllSupplierPaymentVouchersR)Session["AllSupplierPaymentVouchersDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            AllSupplierPaymentVouchersR allSupplierPaymentVouchersR = new AllSupplierPaymentVouchersR();
            allSupplierPaymentVouchersR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\AllSupplierPaymentVouchersR.rpt");
            allSupplierPaymentVouchersR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["AllSupplierPaymentVouchersParam"] as ReportParmPersister;
            List<AllSupplierPaymentVouchersReport> data = HttpContext.Current.Session["AllSupplierPaymentVouchersReportData"] as List<AllSupplierPaymentVouchersReport>;
            allSupplierPaymentVouchersR.SetDataSource(data);

            allSupplierPaymentVouchersR.SetParameterValue("address", persister.CompAddress);
            allSupplierPaymentVouchersR.SetParameterValue("compName", persister.CompName);
            allSupplierPaymentVouchersR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            allSupplierPaymentVouchersR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = allSupplierPaymentVouchersR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["AllSupplierPaymentVouchersDoc"] = allSupplierPaymentVouchersR;
        }
    }
}