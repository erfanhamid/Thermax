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
    public partial class TsoWiseSalesReturnRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                TsoWiseSalesReturnR report = (TsoWiseSalesReturnR)Session["ReportDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            TsoWiseSalesReturnR tsoWiseSalesReturnR = new TsoWiseSalesReturnR();

            tsoWiseSalesReturnR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\TsoWiseSalesReturnR.rpt");
            tsoWiseSalesReturnR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["TsoWiseSalesReturn"] as ReportParmPersister;
            List<TsoWiseSalesItemReport> data = HttpContext.Current.Session["TsoWiseSalesReturnReportData"] as List<TsoWiseSalesItemReport>;
            tsoWiseSalesReturnR.SetDataSource(data);

            tsoWiseSalesReturnR.SetParameterValue("address", "");
            tsoWiseSalesReturnR.SetParameterValue("compName", "");
            tsoWiseSalesReturnR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            tsoWiseSalesReturnR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = tsoWiseSalesReturnR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ReportDoc"] = tsoWiseSalesReturnR;
        }
    }
}