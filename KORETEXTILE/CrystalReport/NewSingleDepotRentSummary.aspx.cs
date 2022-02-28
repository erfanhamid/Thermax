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
    public partial class NewSingleDepotRentSummary : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleDepotRenSummaryNewR report = (SingleDepotRenSummaryNewR)Session["SingleDepotRenSummaryNewRDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SingleDepotRenSummaryNewR singleDepotRenSummaryNewR = new SingleDepotRenSummaryNewR();

            singleDepotRenSummaryNewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SingleDepotRenSummaryNewR.rpt");
            singleDepotRenSummaryNewR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SingleDepotRentSummaryParam"] as ReportParmPersister;
            List<SingleDepotAllRentSummaryReport> data = HttpContext.Current.Session["SingleDepotRentSummaryData"] as List<SingleDepotAllRentSummaryReport>;
            singleDepotRenSummaryNewR.SetDataSource(data);

            //ME.SetParameterValue("compAddress", persister.CompAddress);
            //ME.SetParameterValue("compName", persister.CompName);
            singleDepotRenSummaryNewR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            singleDepotRenSummaryNewR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            //ME.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = singleDepotRenSummaryNewR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SingleDepotRenSummaryNewRDoc"] = singleDepotRenSummaryNewR;
        }
    }
}