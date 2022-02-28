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
    public partial class BatchDetailsRpt : System.Web.UI.Page
    {

        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                BatchDetailsR report = (BatchDetailsR)Session["BatchDetailsDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            BatchDetailsR batchDetailsR = new BatchDetailsR();

            batchDetailsR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\BatchDetailsR.rpt");
            batchDetailsR.Load(APPPATH);

            List<BatchDetailsReport> data = HttpContext.Current.Session["BatchDetailsReportData"] as List<BatchDetailsReport>;
            batchDetailsR.SetDataSource(data);


            batchDetailsR.SetParameterValue("address", "");
            batchDetailsR.SetParameterValue("compName", "");

            CrystalReportViewer1.ReportSource = batchDetailsR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["BatchDetailsDoc"] = batchDetailsR;
        }
    }
}