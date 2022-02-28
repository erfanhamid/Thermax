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
    public partial class ILCSubLedgerItemRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                ILCSubLedgerItemR report = (ILCSubLedgerItemR)Session["ILCSubLedgerItemDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            ILCSubLedgerItemR ils = new ILCSubLedgerItemR();
            ils.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\ILCSubLedgerItemR.rpt");
            ils.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["ILCSubLedgerItemParam"] as ReportParmPersister;
            List<ILCSubLedgerItemReport> data = HttpContext.Current.Session["ILCSubLedgerItemData"] as List<ILCSubLedgerItemReport>;
            ils.SetDataSource(data);

            ils.SetParameterValue("CompName", persister.CompName);
            ils.SetParameterValue("CompAddress", persister.CompAddress);
            ils.SetParameterValue("Account", persister.AccountName);
            ils.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            ils.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = ils;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ILCSubLedgerItemDoc"] = ils;
        }
    }
}