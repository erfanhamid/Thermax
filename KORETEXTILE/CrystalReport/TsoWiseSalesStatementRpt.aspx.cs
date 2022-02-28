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
    public partial class TsoWiseSalesStatementRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                TsoWiseSalesStatementR report = (TsoWiseSalesStatementR)Session["ReportDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            TsoWiseSalesStatementR tsoWiseSalesStatementR = new TsoWiseSalesStatementR();

            tsoWiseSalesStatementR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\TsoWiseSalesStatementR.rpt");
            tsoWiseSalesStatementR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["TsoWiseSalesStatement"] as ReportParmPersister;
            List<TsoWiseSalesStatementReport> data = HttpContext.Current.Session["TsoWiseSalesStatementReportData"] as List<TsoWiseSalesStatementReport>;
            tsoWiseSalesStatementR.SetDataSource(data);

            tsoWiseSalesStatementR.SetParameterValue("address", "");
            tsoWiseSalesStatementR.SetParameterValue("compName", "");
            tsoWiseSalesStatementR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            tsoWiseSalesStatementR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = tsoWiseSalesStatementR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ReportDoc"] = tsoWiseSalesStatementR;
        }
    }
}