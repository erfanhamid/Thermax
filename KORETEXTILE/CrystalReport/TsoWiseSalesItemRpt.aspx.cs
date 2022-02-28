using BEEERP.Models.CommonInformation;
using BEEERP.CrystalReport.ReportRdlc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BEEERP.CrystalReport.ReportFormat;

namespace BEEERP.CrystalReport
{
    public partial class TsoWiseSalesItemRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                TsoWiseSalesItemR report = (TsoWiseSalesItemR)Session["ReportDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            TsoWiseSalesItemR tsoWiseSalesItemR = new TsoWiseSalesItemR();

            tsoWiseSalesItemR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\TsoWiseSalesItemR.rpt");
            tsoWiseSalesItemR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["TsoWiseSalesItem"] as ReportParmPersister;
            List<TsoWiseSalesItemReport> data = HttpContext.Current.Session["TsoWiseSalesItemReportData"] as List<TsoWiseSalesItemReport>;
            tsoWiseSalesItemR.SetDataSource(data);

            tsoWiseSalesItemR.SetParameterValue("address", "");
            tsoWiseSalesItemR.SetParameterValue("compName", "");
            tsoWiseSalesItemR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            tsoWiseSalesItemR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = tsoWiseSalesItemR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ReportDoc"] = tsoWiseSalesItemR;
        }
    }
}