using BEEERP.CrystalReport.ReportFormat;
using BEEERP.Models.CommonInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BEEERP.CrystalReport
{
    public partial class SupplierwisewodetailsRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SupplierwisewodetailsR report = (SupplierwisewodetailsR)Session["SupplierwisewodetailsDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SupplierwisewodetailsR supplierwisewodetailsR = new SupplierwisewodetailsR();

            supplierwisewodetailsR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SupplierwisewodetailsR.rpt");
            supplierwisewodetailsR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["Supplierwisewodetailsparam"] as ReportParmPersister;
            List<SupplierwisewodetailsReport> data = HttpContext.Current.Session["SupplierwisewodetailsData"] as List<SupplierwisewodetailsReport>;
            supplierwisewodetailsR.SetDataSource(data);

            //supplierwisewosummaryR.SetParameterValue("compAddress", persister.CompAddress);
            //supplierwisewosummaryR.SetParameterValue("compName", persister.CompName);
            supplierwisewodetailsR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            supplierwisewodetailsR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            //supplierwisewosummaryR.SetParameterValue("reportName", Session["ReportName"]);




            CrystalReportViewer1.ReportSource = supplierwisewodetailsR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SupplierwisewodetailsDoc"] = supplierwisewodetailsR;
        }
    }
}