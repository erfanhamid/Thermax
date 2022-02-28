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
    public partial class DepotWiseSingleCostGroupDetailRpt : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                DepotWiseSingleCostGroupDetailR report = (DepotWiseSingleCostGroupDetailR)Session["DepotWiseSingleCostGroupDetailDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            DepotWiseSingleCostGroupDetailR dwscgd = new DepotWiseSingleCostGroupDetailR();

            dwscgd.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\DepotWiseSingleCostGroupDetailR.rpt");
            dwscgd.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["DWSCGDetailParam"] as ReportParmPersister;
            List<DepotWiseSingleCGDetailReport> data = HttpContext.Current.Session["DWSCGDetailData"] as List<DepotWiseSingleCGDetailReport>;
            dwscgd.SetDataSource(data);

            dwscgd.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            dwscgd.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            dwscgd.SetParameterValue("cGroup", persister.CostGroup.ToString());


            CrystalReportViewer1.ReportSource = dwscgd;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["DepotWiseSingleCostGroupDetailDoc"] = dwscgd;
        }
    }
}