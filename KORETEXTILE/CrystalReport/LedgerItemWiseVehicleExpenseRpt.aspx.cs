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
    public partial class LedgerItemWiseVehicleExpenseRpt : System.Web.UI.Page
    {
      

            public void Page_Init(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    ShowReport();
                }
                else
                {
                    LedgerItemWiseVehicleExpenseR report = (LedgerItemWiseVehicleExpenseR)Session["LedgerVehicleExpenseDoc"];
                    CrystalReportViewer1.ReportSource = report;
                }
            }
            public void ShowReport()
            {
            LedgerItemWiseVehicleExpenseR vExpense = new LedgerItemWiseVehicleExpenseR();
                vExpense.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
                String APPPATH = Server.MapPath(@"\ReportRdlc\LedgerItemWiseVehicleExpenseR.rpt");
                vExpense.Load(APPPATH);

                ReportParmPersister persister = HttpContext.Current.Session["LedgerVehicleExpenseParam"] as ReportParmPersister;
                List<LedgerItemWiseVehicleExpense> data = HttpContext.Current.Session["LedgerVehicleExpenseData"] as List<LedgerItemWiseVehicleExpense>;
                vExpense.SetDataSource(data);

                //vExpense.SetParameterValue("accountName", persister.AccountName);
                vExpense.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
                vExpense.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

                CrystalReportViewer1.ReportSource = vExpense;
                CrystalReportViewer1.EnableParameterPrompt = false;
                Session["LedgerVehicleExpenseDoc"] = vExpense;
            }

        
    }
}