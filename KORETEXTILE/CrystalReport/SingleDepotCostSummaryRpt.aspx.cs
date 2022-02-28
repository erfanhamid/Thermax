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
    public partial class SingleDepotCostSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleDepotCostSummaryR report = (SingleDepotCostSummaryR)Session["SingleDepotCostSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SingleDepotCostSummaryR sdcs = new SingleDepotCostSummaryR();

            sdcs.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SingleDepotCostSummaryR.rpt");
            sdcs.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SingleDepotCostSummaryParam"] as ReportParmPersister;
            List<SingleDepotCostSummary> data = HttpContext.Current.Session["SingleDepotCostSummaryData"] as List<SingleDepotCostSummary>;
            sdcs.SetDataSource(data);

            //sdcs.SetParameterValue("compAddress", persister.CompAddress);
            //sdcs.SetParameterValue("compName", persister.CompName);
            //sdcs.SetParameterValue("AsDate", persister.AsOnDate.ToString("dd-MM-yyyy"));
            sdcs.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            sdcs.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = sdcs;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SingleDepotCostSummaryDoc"] = sdcs;
        }
    }
}