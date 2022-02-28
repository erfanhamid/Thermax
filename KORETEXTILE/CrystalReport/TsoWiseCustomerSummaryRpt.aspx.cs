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
    public partial class TsoWiseCustomerSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                TsoWiseCustomerSummaryR report = (TsoWiseCustomerSummaryR)Session["ReportDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            TsoWiseCustomerSummaryR tsoWiseCustomerSummaryR = new TsoWiseCustomerSummaryR();

            tsoWiseCustomerSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\TsoWiseCustomerSummaryR.rpt");
            tsoWiseCustomerSummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["TsoWiseCustomerSummary"] as ReportParmPersister;
            List<TsoWiseCustSummryReport> data = HttpContext.Current.Session["TsoWiseCustomerSummaryRptData"] as List<TsoWiseCustSummryReport>;
            tsoWiseCustomerSummaryR.SetDataSource(data);


            tsoWiseCustomerSummaryR.SetParameterValue("address", "");
            tsoWiseCustomerSummaryR.SetParameterValue("compName", "");
            tsoWiseCustomerSummaryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            tsoWiseCustomerSummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = tsoWiseCustomerSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ReportDoc"] = tsoWiseCustomerSummaryR;
        }
    }
}