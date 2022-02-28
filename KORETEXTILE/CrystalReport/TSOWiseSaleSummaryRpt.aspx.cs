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
    public partial class TSOWiseSaleSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                TSOWiseSaleSummaryR report = (TSOWiseSaleSummaryR)Session["ReportDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            TSOWiseSaleSummaryR tsoWiseSaleSummaryR = new TSOWiseSaleSummaryR();

            tsoWiseSaleSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\TSOWiseSaleSummaryR.rpt");
            tsoWiseSaleSummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["TSOWiseSaleSummaryByDate"] as ReportParmPersister;
            List<TSOWiseSaleSummaryReport> data = HttpContext.Current.Session["TSOWiseSaleSummaryReportData"] as List<TSOWiseSaleSummaryReport>;
            tsoWiseSaleSummaryR.SetDataSource(data);


            tsoWiseSaleSummaryR.SetParameterValue("address", "");
            tsoWiseSaleSummaryR.SetParameterValue("compName", "");
            tsoWiseSaleSummaryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            tsoWiseSaleSummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = tsoWiseSaleSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ReportDoc"] = tsoWiseSaleSummaryR;
        }
    }
}