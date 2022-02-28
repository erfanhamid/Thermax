using BEEERP.CrystalReport.ReportRdlc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BEEERP.CrystalReport
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ChartOfInventory dsChartOfInv = new ChartOfInventory();
            //dynamic sample = Activator.CreateInstance(null, "BEEERP.CrystalReport.ReportRdlc."+ HttpContext.Current.Session["ReportName"].ToString()).Unwrap();
            //CrystalReportViewer1.ReportSource = sample;
            //sample.SetDatabaseLogon("rabbani", "12345678", @"AVALANCHE\SQLEXPRESS", "ALMADINA_PHARMA_2018", true);
            //CrystalReportViewer1.RefreshReport();

        }
    }
}