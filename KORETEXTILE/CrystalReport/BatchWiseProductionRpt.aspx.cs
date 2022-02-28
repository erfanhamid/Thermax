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
    public partial class BatchWiseProductionRpt : System.Web.UI.Page
    {

        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                BatchWiseProductionR report = (BatchWiseProductionR)Session["BatchWiseProductionDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            BatchWiseProductionR batchWiseProductionR = new BatchWiseProductionR();

            batchWiseProductionR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\BatchWiseProductionR.rpt");
            batchWiseProductionR.Load(APPPATH);

            List<BatchWiseProductionReport> data = HttpContext.Current.Session["BatchWiseProductionReportData"] as List<BatchWiseProductionReport>;
            batchWiseProductionR.SetDataSource(data);


            batchWiseProductionR.SetParameterValue("address", "");
            batchWiseProductionR.SetParameterValue("compName", "");

            CrystalReportViewer1.ReportSource = batchWiseProductionR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["BatchWiseProductionDoc"] = batchWiseProductionR;
        }
    }
}