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
    public partial class DepotWiseSalesRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                DepotWiseSalesR report = (DepotWiseSalesR)Session["SalesReportDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            DepotWiseSalesR Sales = new DepotWiseSalesR();

            Sales.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\DepotWiseSalesR.rpt");
            Sales.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["DepotWiseSalesParam"] as ReportParmPersister;
            List<DepotWiseSalesReport> data = HttpContext.Current.Session["DepotWiseSalesData"] as List<DepotWiseSalesReport>;
            Sales.SetDataSource(data);

            Sales.SetParameterValue("compAddress", persister.CompAddress);
            Sales.SetParameterValue("compName", persister.CompName);
            Sales.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            Sales.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
  



            CrystalReportViewer1.ReportSource = Sales;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SalesReportDoc"] = Sales;
        }
    }
}