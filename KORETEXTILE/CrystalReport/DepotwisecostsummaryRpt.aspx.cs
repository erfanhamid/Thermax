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
    public partial class DepotwisecostsummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                DepotwisecostsummaryR report = (DepotwisecostsummaryR)Session["DepotwisecostsummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            DepotwisecostsummaryR dwcs = new DepotwisecostsummaryR();
            dwcs.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\DepotwisecostsummaryR.rpt");
            dwcs.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["DWCSParam"] as ReportParmPersister;
            List<Depotwisecostsummaryreport> data = HttpContext.Current.Session["DWCSData"] as List<Depotwisecostsummaryreport>;
            dwcs.SetDataSource(data);

            //vExpense.SetParameterValue("accountName", persister.AccountName);
            dwcs.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            dwcs.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            //salesDetails.SetParameterValue("BranchName", persister.BranchName);

            CrystalReportViewer1.ReportSource = dwcs;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["DepotwisecostsummaryDoc"] = dwcs;
        }
    }
}