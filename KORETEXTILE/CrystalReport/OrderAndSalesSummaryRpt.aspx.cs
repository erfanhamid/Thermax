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
    public partial class OrderAndSalesSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                OrderAndSalesSummaryR report = (OrderAndSalesSummaryR)Session["OrderAndSalesSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            OrderAndSalesSummaryR orderAndSalesSummaryR = new OrderAndSalesSummaryR();

            orderAndSalesSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\OrderAndSalesSummaryR.rpt");
            orderAndSalesSummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["OrderAndSalesSummaryParam"] as ReportParmPersister;
            List<OrderAndSalesSummaryReport> data = HttpContext.Current.Session["OrderAndSalesSummaryReport"] as List<OrderAndSalesSummaryReport>;
            orderAndSalesSummaryR.SetDataSource(data);

            orderAndSalesSummaryR.SetParameterValue("address", persister.CompAddress);
            orderAndSalesSummaryR.SetParameterValue("compName", persister.CompName);
            orderAndSalesSummaryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            orderAndSalesSummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = orderAndSalesSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["OrderAndSalesSummaryDoc"] = orderAndSalesSummaryR;
        }
    }
}