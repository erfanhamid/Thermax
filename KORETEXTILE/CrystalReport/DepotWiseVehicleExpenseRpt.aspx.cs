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
    public partial class DepotWiseVehicleExpenseRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                DepotWiseVehicleExpenseR report = (DepotWiseVehicleExpenseR)Session["DepotWiseVehicleExpenseDetailsDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            DepotWiseVehicleExpenseR vExpense = new DepotWiseVehicleExpenseR();
            vExpense.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\DepotWiseVehicleExpenseR.rpt");
            vExpense.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["DepotVehicleExpenseParam"] as ReportParmPersister;
            List<DepotWiseVehicleExpense> data = HttpContext.Current.Session["DepotVehicleExpenseData"] as List<DepotWiseVehicleExpense>;
            vExpense.SetDataSource(data);

            //vExpense.SetParameterValue("accountName", persister.AccountName);
            vExpense.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            vExpense.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            vExpense.SetParameterValue("BranchName", persister.BranchName);

            CrystalReportViewer1.ReportSource = vExpense;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["DepotWiseVehicleExpenseDetailsDoc"] = vExpense;
        }
    }
}