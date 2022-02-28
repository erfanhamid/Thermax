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
    public partial class FundTransferVoucherRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                FundTransferVoucherR report = (FundTransferVoucherR)Session["FundTransferVoucherDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            FundTransferVoucherR fundTransferVoucherR = new FundTransferVoucherR();

            fundTransferVoucherR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\FundTransferVoucherR.rpt");
            fundTransferVoucherR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["FundTransferVoucherParam"] as ReportParmPersister;
            List<FundTransferVoucherReport> data = HttpContext.Current.Session["FundTransferVoucherReportData"] as List<FundTransferVoucherReport>;
            fundTransferVoucherR.SetDataSource(data);

            fundTransferVoucherR.SetParameterValue("address", persister.CompAddress);
            fundTransferVoucherR.SetParameterValue("compName", persister.CompName);
            fundTransferVoucherR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            fundTransferVoucherR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = fundTransferVoucherR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["FundTransferVoucherDoc"] = fundTransferVoucherR;
        }
    }
}