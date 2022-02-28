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
    public partial class SingleDepotRetailerRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleDepotRetailerR report = (SingleDepotRetailerR)Session["SingleDepotRetailerDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SingleDepotRetailerR singleDepotRetailerR = new SingleDepotRetailerR();

            singleDepotRetailerR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SingleDepotRetailerR.rpt");
            singleDepotRetailerR.Load(APPPATH);

            List<SingleDepotRetailerReport> data = HttpContext.Current.Session["SingleDepotRetailerData"] as List<SingleDepotRetailerReport>;
            singleDepotRetailerR.SetDataSource(data);


            //singleDepotRetailerR.SetParameterValue("address", "");
            //singleDepotRetailerR.SetParameterValue("compName", "");

            CrystalReportViewer1.ReportSource = singleDepotRetailerR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SingleDepotRetailerDoc"] = singleDepotRetailerR;
        }
    }
}