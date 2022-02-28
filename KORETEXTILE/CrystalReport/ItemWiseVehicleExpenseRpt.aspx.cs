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
    public partial class ItemWiseVehicleExpenseRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                ItemWiseVehicleExpenseR report = (ItemWiseVehicleExpenseR)Session["VehicleExpenseDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            ItemWiseVehicleExpenseR vExpense = new ItemWiseVehicleExpenseR();
            vExpense.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\ItemWiseVehicleExpenseR.rpt");
            vExpense.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["VehicleExpenseParam"] as ReportParmPersister;
            List<ItemWiseVehicleExpense> data = HttpContext.Current.Session["VehicleExpenseData"] as List<ItemWiseVehicleExpense>;
            vExpense.SetDataSource(data);

            //vExpense.SetParameterValue("accountName", persister.AccountName);
            vExpense.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            vExpense.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = vExpense;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["VehicleExpenseDoc"] = vExpense;
        }
    }
}