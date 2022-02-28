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
    public partial class ILCBDetailsRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                ILCBDetailsR report = (ILCBDetailsR)Session["ILCBDetailsDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            ILCBDetailsR ILCB = new ILCBDetailsR();
            ILCB.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\AccountsLedgerR.rpt");
            ILCB.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["ILCBDetailsparam"] as ReportParmPersister;
            List<ILCPPreviewReport> data = HttpContext.Current.Session["ILCBDetailsReportData"] as List<ILCPPreviewReport>;
            ILCB.SetDataSource(data);

            ILCB.SetParameterValue("CompName", persister.CompName);
            ILCB.SetParameterValue("CompAddress", persister.CompAddress);
            ILCB.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            ILCB.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = ILCB;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ILCBDetailsDoc"] = ILCB;
        }
    }
}