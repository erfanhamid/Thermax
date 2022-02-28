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
    public partial class SingleDepotMonthWiseCostRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleDepotMonthWiseCostR report = (SingleDepotMonthWiseCostR)Session["SingleDepotMonthWiseCostDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SingleDepotMonthWiseCostR sdmwc = new SingleDepotMonthWiseCostR();

            sdmwc.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SingleDepotMonthWiseCostR.rpt");
            sdmwc.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SDMWCParam"] as ReportParmPersister;
            List<SingleDepotMonthWiseCostReport> data = HttpContext.Current.Session["SDMWCData"] as List<SingleDepotMonthWiseCostReport>;
            sdmwc.SetDataSource(data);

            //sdcs.SetParameterValue("compAddress", persister.CompAddress);
            //sdcs.SetParameterValue("compName", persister.CompName);
            //sdcs.SetParameterValue("AsDate", persister.AsOnDate.ToString("dd-MM-yyyy"));
            sdmwc.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            sdmwc.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            sdmwc.SetParameterValue("depotName", persister.BranchName.ToString());


            CrystalReportViewer1.ReportSource = sdmwc;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SingleDepotMonthWiseCostDoc"] = sdmwc;
        }
    }
}