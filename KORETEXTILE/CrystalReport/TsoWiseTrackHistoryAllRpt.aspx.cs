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
    public partial class TsoWiseTrackHistoryAllRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                TSOWiseTrackingReportR report = (TSOWiseTrackingReportR)Session["TSOWiseTrackingAllDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }

        public void ShowReport()
        {
            TSOWiseTrackingReportR trackhistory = new TSOWiseTrackingReportR();
            trackhistory.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\TSOWiseTrackingReportR.rpt");
            trackhistory.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["TSOWiseTrackingHistoryAll"] as ReportParmPersister;
            List<TsoWiseTrackingHistory> data = HttpContext.Current.Session["TSOWiseTrackingHistoryAllData"] as List<TsoWiseTrackingHistory>;
            trackhistory.SetDataSource(data);
            trackhistory.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            trackhistory.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = trackhistory;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["TSOWiseTrackingAllDoc"] = trackhistory;
        }
    }
}