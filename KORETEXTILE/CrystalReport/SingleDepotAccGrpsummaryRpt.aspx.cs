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
    public partial class SingleDepotAccGrpsummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleDepotAccountGroupSummaryR report = (SingleDepotAccountGroupSummaryR)Session["SingleDepotAccountGroupSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SingleDepotAccountGroupSummaryR sdags = new SingleDepotAccountGroupSummaryR();

            sdags.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SingleDepotAccountGroupSummaryR.rpt");
            sdags.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SDAGSParam"] as ReportParmPersister;
            List<SingleDepotAccGroupSummaryReport> data = HttpContext.Current.Session["SDAGSData"] as List<SingleDepotAccGroupSummaryReport>;
            sdags.SetDataSource(data);

            //sdcs.SetParameterValue("compAddress", persister.CompAddress);
            //sdcs.SetParameterValue("compName", persister.CompName);
            //sdcs.SetParameterValue("AsDate", persister.AsOnDate.ToString("dd-MM-yyyy"));
            sdags.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            sdags.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = sdags;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SingleDepotAccountGroupSummaryDoc"] = sdags;
        }
    }
}