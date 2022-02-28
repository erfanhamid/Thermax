
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
    public partial class CustomerLedger : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                DealerLedger report = (DealerLedger)Session["DealerLedgerDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            DealerLedger custLedger = new DealerLedger();
            custLedger.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\DealerLedger.rpt");
            custLedger.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SearchCustLedger"] as ReportParmPersister;
            List<CustLedgerReport> data = HttpContext.Current.Session["CustLedgerReportData"] as List<CustLedgerReport>;
            custLedger.SetDataSource(data);


            custLedger.SetParameterValue("custName", persister.CustName);
            custLedger.SetParameterValue("address", persister.Address);
            custLedger.SetParameterValue("customerId", persister.CustId);
            custLedger.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            custLedger.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            custLedger.SetParameterValue("DepotName", persister.DepotName);

            CrystalReportViewer1.ReportSource = custLedger;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["DealerLedgerDoc"] = custLedger;
        }
    }
}