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
    public partial class BankWiseLATRABSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                BankWiseLATRABSummaryR report = (BankWiseLATRABSummaryR)Session["BankWiseLATRDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            BankWiseLATRABSummaryR BankWiselATR = new BankWiseLATRABSummaryR();

            BankWiselATR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\BankWiseLATRABSummaryR.rpt");
            BankWiselATR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["BankWiseLatr"] as ReportParmPersister;
            List<BankWiseLATRABSReport> data = HttpContext.Current.Session["BankWiseLATRData"] as List<BankWiseLATRABSReport>;
            BankWiselATR.SetDataSource(data);

            BankWiselATR.SetParameterValue("compAddress", persister.CompAddress);
            BankWiselATR.SetParameterValue("compName", persister.CompName);
            BankWiselATR.SetParameterValue("AsDate", persister.AsOnDate.ToString("dd-MM-yyyy"));
      

            CrystalReportViewer1.ReportSource = BankWiselATR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["BankWiseLATRDoc"] = BankWiselATR;
        }
    }
}