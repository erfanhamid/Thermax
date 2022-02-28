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
    public partial class ImportLCLedgerRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                ImportLCLedgerR report = (ImportLCLedgerR)Session["ImportLCLedgerDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            ImportLCLedgerR importLCLedgerR = new ImportLCLedgerR();

            importLCLedgerR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\ImportLCLedgerR.rpt");
            importLCLedgerR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["ILCLedgerParam"] as ReportParmPersister;
            List<ImportLCLedgerReport> data = HttpContext.Current.Session["ImportLCLedgerData"] as List<ImportLCLedgerReport>;
            importLCLedgerR.SetDataSource(data);

            //importLCLedgerR.SetParameterValue("address", persister.CompAddress);
            //importLCLedgerR.SetParameterValue("compName", persister.CompName);
            importLCLedgerR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            importLCLedgerR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            //importLCLedgerR.SetParameterValue("ilcId", persister.ILCId);
            //importLCLedgerR.SetParameterValue("ilcNo", persister.ILCNo);
            //importLCLedgerR.SetParameterValue("supName", persister.SupplierName);
            //importLCLedgerR.SetParameterValue("lcType", persister.LCType);

            CrystalReportViewer1.ReportSource = importLCLedgerR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ImportLCLedgerDoc"] = importLCLedgerR;
        }
    }
}