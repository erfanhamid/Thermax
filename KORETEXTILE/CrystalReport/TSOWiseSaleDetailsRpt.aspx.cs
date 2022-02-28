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
    public partial class TSOWiseSaleDetailsRpt : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                TSOWiseSaleDetailsR report = (TSOWiseSaleDetailsR)Session["TSOWiseSaleDetailsDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            TSOWiseSaleDetailsR tsoWiseSaleDetailsR = new TSOWiseSaleDetailsR();

            tsoWiseSaleDetailsR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\TSOWiseSaleDetailsR.rpt");
            tsoWiseSaleDetailsR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["TsoWiseSalesDetails"] as ReportParmPersister;
            List<TSOWiseSaleDetailsReport> data = HttpContext.Current.Session["TSOWiseSaleDetailsReportData"] as List<TSOWiseSaleDetailsReport>;
            tsoWiseSaleDetailsR.SetDataSource(data);


            tsoWiseSaleDetailsR.SetParameterValue("address", "");
            tsoWiseSaleDetailsR.SetParameterValue("compName", "");
            tsoWiseSaleDetailsR.SetParameterValue("tsoName", persister.TsoName);


            CrystalReportViewer1.ReportSource = tsoWiseSaleDetailsR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["TSOWiseSaleDetailsDoc"] = tsoWiseSaleDetailsR;
        }
    }
}