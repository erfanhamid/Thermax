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
    public partial class TsoWiseSalesAndCollectionRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                TsoWiseSalesAndCollectionR report = (TsoWiseSalesAndCollectionR)Session["ReportDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            TsoWiseSalesAndCollectionR tsoWiseSalesAndCollectionR = new TsoWiseSalesAndCollectionR();

            tsoWiseSalesAndCollectionR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\TsoWiseSalesAndCollectionR.rpt");
            tsoWiseSalesAndCollectionR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["TsoWiseSalesAndCollection"] as ReportParmPersister;
            List<TsoWiseSalesAndCollectionReport> data = HttpContext.Current.Session["TsoWiseSalesAndCollectionReportData"] as List<TsoWiseSalesAndCollectionReport>;
            tsoWiseSalesAndCollectionR.SetDataSource(data);

            tsoWiseSalesAndCollectionR.SetParameterValue("address", "");
            tsoWiseSalesAndCollectionR.SetParameterValue("compName", "");
            tsoWiseSalesAndCollectionR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            tsoWiseSalesAndCollectionR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = tsoWiseSalesAndCollectionR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ReportDoc"] = tsoWiseSalesAndCollectionR;
        }
    }
}