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
    public partial class MRLedger : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                MRLedgerR report = (MRLedgerR)Session["MRLedgerDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            MRLedgerR mrLedger = new MRLedgerR();
            mrLedger.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\MRLedgerR.rpt");
            mrLedger.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SearchMRLedger"] as ReportParmPersister;
            List<MRLedgerReport> data = HttpContext.Current.Session["MRLedgerReportData"] as List<MRLedgerReport>;
            mrLedger.SetDataSource(data);


            mrLedger.SetParameterValue("empId", persister.EmployeeID);
            mrLedger.SetParameterValue("empName", persister.EmployeeName);
            mrLedger.SetParameterValue("empDesignation", persister.EmpDesignation);
            mrLedger.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            mrLedger.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = mrLedger;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["MRLedgerDoc"] = mrLedger;
        }
    }
}