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
    public partial class TsoWiseTrackHistoryRpt : System.Web.UI.Page
    {
        
            public void Page_Init(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    ShowReport();
                }
                else
                {
                    TSOWiseTrackingR report = (TSOWiseTrackingR)Session["TSOWiseTrackingDoc"];
                    CrystalReportViewer1.ReportSource = report;
                }
            }

            public void ShowReport()
            {
                TSOWiseTrackingR trackhistory = new TSOWiseTrackingR();
                trackhistory.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
                String APPPATH = Server.MapPath(@"\ReportRdlc\TSOWiseTrackingR.rpt");
                trackhistory.Load(APPPATH);

                ReportParmPersister persister = HttpContext.Current.Session["TSOWiseTrackingHistory"] as ReportParmPersister;
                List<TsoWiseTrackingHistory> data = HttpContext.Current.Session["TSOWiseTrackingHistoryData"] as List<TsoWiseTrackingHistory>;
                trackhistory.SetDataSource(data);
                trackhistory.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
                trackhistory.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

                CrystalReportViewer1.ReportSource = trackhistory;
                CrystalReportViewer1.EnableParameterPrompt = false;
                Session["TSOWiseTrackingDoc"] = trackhistory;
            
        }
    }
}