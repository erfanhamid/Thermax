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
    public partial class CategoryWiseSalesRpt : System.Web.UI.Page
    {

        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                CategoryWiseSalesReportR report = (CategoryWiseSalesReportR)Session["CategoryWiseSalesReport"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            CategoryWiseSalesReportR categoryWiseSales = new CategoryWiseSalesReportR();

            categoryWiseSales.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\CategoryWiseSalesReportR.rpt");
            categoryWiseSales.Load(APPPATH);

            List<CategoryWiseSalesReport> data = HttpContext.Current.Session["CategoryWiseSalesReportData"] as List<CategoryWiseSalesReport>;
            categoryWiseSales.SetDataSource(data);

            ReportParmPersister persister = HttpContext.Current.Session["CategoryWiseSalesReportParam"] as ReportParmPersister;

            categoryWiseSales.SetParameterValue("CompAddress", persister.CompAddress);
            categoryWiseSales.SetParameterValue("CompanyName", persister.CompName);
            categoryWiseSales.SetParameterValue("DateDuration", persister.Description);
            CrystalReportViewer1.ReportSource = categoryWiseSales;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["CategoryWiseSalesReport"] = categoryWiseSales;
        }
    }
}