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
    public partial class DepotandCostGroupWiseSummaryyyRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                DepotandCostGroupWiseSummaryyyR report = (DepotandCostGroupWiseSummaryyyR)Session["DepotandCostGroupWiseSummaryyyDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            DepotandCostGroupWiseSummaryyyR depotcostgrupsumm = new DepotandCostGroupWiseSummaryyyR();

            depotcostgrupsumm.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\DepotandCostGroupWiseSummaryyyR.rpt");
            depotcostgrupsumm.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["DepotandCostGroupWiseSummaryyyParam"] as ReportParmPersister;
            List<DepotWiseCostGroupSummaryReport> data = HttpContext.Current.Session["DepotandCostGroupWiseSummaryyyData"] as List<DepotWiseCostGroupSummaryReport>;
            depotcostgrupsumm.SetDataSource(data);

            //sdcs.SetParameterValue("compAddress", persister.CompAddress);
            //sdcs.SetParameterValue("compName", persister.CompName);
            //sdcs.SetParameterValue("AsDate", persister.AsOnDate.ToString("dd-MM-yyyy"));
            depotcostgrupsumm.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            depotcostgrupsumm.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = depotcostgrupsumm;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["DepotandCostGroupWiseSummaryDoc"] = depotcostgrupsumm;
        }
    }
}