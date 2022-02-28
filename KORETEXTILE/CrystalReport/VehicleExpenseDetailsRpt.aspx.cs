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
    public partial class VehicleExpenseDetailsRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                VehicleExpenseDetailsR report = (VehicleExpenseDetailsR)Session["VehicleExpenseDetailsDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            VehicleExpenseDetailsR vExpense = new VehicleExpenseDetailsR();
            vExpense.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\VehicleExpenseDetailsR.rpt");
            vExpense.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["VehicleExpenseDetailsParam"] as ReportParmPersister;
            List<VehicleExpenseDetailsReport> data = HttpContext.Current.Session["VehicleExpenseDetailsData"] as List<VehicleExpenseDetailsReport>;
            vExpense.SetDataSource(data);

            //vExpense.SetParameterValue("accountName", persister.AccountName);
            vExpense.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            vExpense.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            //vExpense.SetParameterValue("VehicleName", persister.StoreName);

            CrystalReportViewer1.ReportSource = vExpense;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["VehicleExpenseDetailsDoc"] = vExpense;
        }
    }
}