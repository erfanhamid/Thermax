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
    public partial class CostGroupWiseSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                CostGroupWiseSummaryR report = (CostGroupWiseSummaryR)Session["CostGroupWiseSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            CostGroupWiseSummaryR cgws = new CostGroupWiseSummaryR();

            cgws.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\CostGroupWiseSummaryR.rpt");
            cgws.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["CGWSParam"] as ReportParmPersister;
            List<CostGroupWiseSummaryReport> data = HttpContext.Current.Session["CGWSData"] as List<CostGroupWiseSummaryReport>;
            cgws.SetDataSource(data);

            //cgws.SetParameterValue("asDate", persister.AsOnDate.ToString("dd-MM-yyyy"));
            cgws.SetParameterValue("fDate", persister.ToDate.ToString("dd-MM-yyyy"));
            cgws.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = cgws;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["CostGroupWiseSummaryDoc"] = cgws;
        }
    }
}