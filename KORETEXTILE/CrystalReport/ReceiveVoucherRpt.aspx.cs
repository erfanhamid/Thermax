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
    public partial class ReceiveVoucherRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                ReceiveVoucherR report = (ReceiveVoucherR)Session["ReceiveVoucherDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            ReceiveVoucherR receiveVoucherR = new ReceiveVoucherR();

            receiveVoucherR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\ReceiveVoucherR.rpt");
            receiveVoucherR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["ReceiveVoucherReportParam"] as ReportParmPersister;
            List<ReceiveVoucherReport> data = HttpContext.Current.Session["ReceiveVoucherReportData"] as List<ReceiveVoucherReport>;
            receiveVoucherR.SetDataSource(data);

            receiveVoucherR.SetParameterValue("address", persister.CompAddress);
            receiveVoucherR.SetParameterValue("compName", persister.CompName);
            receiveVoucherR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            receiveVoucherR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = receiveVoucherR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ReceiveVoucherDoc"] = receiveVoucherR;
        }
    }
}