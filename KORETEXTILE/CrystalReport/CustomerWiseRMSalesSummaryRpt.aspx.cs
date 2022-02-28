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
    public partial class CustomerWiseRMSalesSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                CustomerWiseRMSalesSummaryR report = (CustomerWiseRMSalesSummaryR)Session["CustomerWiseRMSalesSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            CustomerWiseRMSalesSummaryR customerWiseRMSalesSummaryR = new CustomerWiseRMSalesSummaryR();

            customerWiseRMSalesSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\customerWiseRMSalesSummaryR.rpt");
            customerWiseRMSalesSummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["CustomerWiseRMSalesSummaryParam"] as ReportParmPersister;
            List<CustomerWiseRMSalesSummaryReport> data = HttpContext.Current.Session["CustomerWiseRMSalesSummaryData"] as List<CustomerWiseRMSalesSummaryReport>;
            customerWiseRMSalesSummaryR.SetDataSource(data);

            customerWiseRMSalesSummaryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            customerWiseRMSalesSummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = customerWiseRMSalesSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["CustomerWiseRMSalesSummaryDoc"] = customerWiseRMSalesSummaryR;
        }
    }
}