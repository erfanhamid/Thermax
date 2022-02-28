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
    public partial class VehicleExpenseRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                VehicleExpenseR report = (VehicleExpenseR)Session["DepotWiseVehicleExpenseDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            VehicleExpenseR vExpense = new VehicleExpenseR();
            vExpense.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\VehicleExpenseR.rpt");
            vExpense.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["VehicleExpenseParam"] as ReportParmPersister;
            List<VehicleExpenseReport> data = HttpContext.Current.Session["VehicleExpenseData"] as List<VehicleExpenseReport>;
            vExpense.SetDataSource(data);

            //vExpense.SetParameterValue("accountName", persister.AccountName);
            vExpense.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            vExpense.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            vExpense.SetParameterValue("BranchName", persister.BranchName);

            CrystalReportViewer1.ReportSource = vExpense;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["DepotWiseVehicleExpenseDoc"] = vExpense;
        }
    }
}