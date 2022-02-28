using BEEERP.CrystalReport.ReportFormat;
using BEEERP.Models.CommonInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BEEERP.CrystalReport.ReportRdlc
{
    public partial class ImportLCReceiveDetailsRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                ImportLCReceiveDetailsR report = (ImportLCReceiveDetailsR)Session["ImportLCReceiveDetails"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            ImportLCReceiveDetailsR importLC = new ImportLCReceiveDetailsR();
            importLC.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\ImportLCReceiveDetailsR.rpt");
            importLC.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["ImportLCReceiveDetailsParam"] as ReportParmPersister;
            List<ImportLCReceiveDetailsReport> data = HttpContext.Current.Session["ImportLCReceiveDetailsData"] as List<ImportLCReceiveDetailsReport>;
            importLC.SetDataSource(data);

            importLC.SetParameterValue("DateDuration", persister.Description);
            importLC.SetParameterValue("address", persister.CompAddress);
            importLC.SetParameterValue("compName", persister.CompName);
            CrystalReportViewer1.ReportSource = importLC;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ImportLCReceiveDetails"] = importLC;
        }
    }
}