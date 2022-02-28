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
    public partial class SupplierwisewosummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SupplierwisewosummaryR report = (SupplierwisewosummaryR)Session["SupplierwisewosummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SupplierwisewosummaryR supplierwisewosummaryR = new SupplierwisewosummaryR();

            supplierwisewosummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SupplierwisewosummaryR.rpt");
            supplierwisewosummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["Supplierwisewosummaryparam"] as ReportParmPersister;
            List<SupplierwisewosummaryReport> data = HttpContext.Current.Session["SupplierwisewosummaryData"] as List<SupplierwisewosummaryReport>;
            supplierwisewosummaryR.SetDataSource(data);

            //supplierwisewosummaryR.SetParameterValue("compAddress", persister.CompAddress);
            //supplierwisewosummaryR.SetParameterValue("compName", persister.CompName);
            supplierwisewosummaryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            supplierwisewosummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            //supplierwisewosummaryR.SetParameterValue("reportName", Session["ReportName"]);




            CrystalReportViewer1.ReportSource = supplierwisewosummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SupplierwisewosummaryDoc"] = supplierwisewosummaryR;
        }
    }
}