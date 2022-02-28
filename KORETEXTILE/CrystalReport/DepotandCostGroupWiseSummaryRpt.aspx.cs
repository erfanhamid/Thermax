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
    public partial class DepotandCostGroupWiseSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                DepotandCostGroupWiseSummaryR report = (DepotandCostGroupWiseSummaryR)Session["DepotandCostGroupWiseSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            DepotandCostGroupWiseSummaryR dacgwiesum = new DepotandCostGroupWiseSummaryR();

            dacgwiesum.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\DepotandCostGroupWiseSummaryR.rpt");
            dacgwiesum.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["DepotandCostGroupWiseSummaryParam"] as ReportParmPersister;
            List<DepotandCostGroupWiseSummaryReport> data = HttpContext.Current.Session["DepotandCostGroupWiseSummaryData"] as List<DepotandCostGroupWiseSummaryReport>;
            dacgwiesum.SetDataSource(data);

            //sdcs.SetParameterValue("compAddress", persister.CompAddress);
            //sdcs.SetParameterValue("compName", persister.CompName);
            //sdcs.SetParameterValue("AsDate", persister.AsOnDate.ToString("dd-MM-yyyy"));
            dacgwiesum.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            dacgwiesum.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = dacgwiesum;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["DepotandCostGroupWiseSummaryDoc"] = dacgwiesum;
        }
    }
}