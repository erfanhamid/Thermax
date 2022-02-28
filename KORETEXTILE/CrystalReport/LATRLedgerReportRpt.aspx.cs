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
    public partial class LATRLedgerReportRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                LATRLedgerR report = (LATRLedgerR)Session["LATRLedgerDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            LATRLedgerR lATRLedgerR = new LATRLedgerR();

            lATRLedgerR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\LATRLedgerR.rpt");
            lATRLedgerR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["LATRLedgerParam"] as ReportParmPersister;
            List<LATRLedgerReport> data = HttpContext.Current.Session["LATRLedgerReportData"] as List<LATRLedgerReport>;
            lATRLedgerR.SetDataSource(data);

            lATRLedgerR.SetParameterValue("address", persister.CompAddress);
            lATRLedgerR.SetParameterValue("compName", persister.CompName);
            lATRLedgerR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            lATRLedgerR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            lATRLedgerR.SetParameterValue("lcID", persister.ILCId);
            lATRLedgerR.SetParameterValue("lcNo", persister.ILCNo);



            CrystalReportViewer1.ReportSource = lATRLedgerR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["LATRLedgerDoc"] = lATRLedgerR;
        }
    }
}