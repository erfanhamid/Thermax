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
    public partial class StorewiseGSInventoryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                StorewiseGSInventoryBalanceNewR report = (StorewiseGSInventoryBalanceNewR)Session["StorewiseGSInventoryBalanceRDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            StorewiseGSInventoryBalanceNewR swgsibalance = new StorewiseGSInventoryBalanceNewR();

            swgsibalance.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\StorewiseGSInventoryBalanceNewR.rpt");
            swgsibalance.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["StorewiseGSInventoryParam"] as ReportParmPersister;
            List<StorewiseGSInventoryReport> data = HttpContext.Current.Session["StorewiseGSInventoryData"] as List<StorewiseGSInventoryReport>;
            swgsibalance.SetDataSource(data);

            swgsibalance.SetParameterValue("compAddress", persister.CompAddress);
            swgsibalance.SetParameterValue("compName", persister.CompName);
            swgsibalance.SetParameterValue("asDate", persister.AsOnDate.ToString("dd-MM-yyyy"));
            //swgsibalance.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            //supplierwisewosummaryR.SetParameterValue("reportName", Session["ReportName"]);




            CrystalReportViewer1.ReportSource = swgsibalance;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["StorewiseGSInventoryBalanceRDoc"] = swgsibalance;
        }
    }
}