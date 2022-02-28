using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BEEERP.CrystalReport
{
    public partial class BalanceSheet : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                BalanceSheetR report = (BalanceSheetR)Session["BalanceSheetDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            ReportParmPersister persister = HttpContext.Current.Session["BalanceSheetParam"] as ReportParmPersister;
            string sql = "exec BalanceSheetL '"+DateTimeFormat.ConvertToDDMMYYYY(persister.AsOnDate) +"'";
            BEEContext context = new BEEContext();
            var lData = context.Database.SqlQuery<BalanceSheetLReport>(sql).ToList();
            BalanceSheetR balaceSheet = new BalanceSheetR();
            balaceSheet.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\BalanceSheetR.rpt");
            balaceSheet.Load(APPPATH);

           
            List<BalanceSheetReport> data = HttpContext.Current.Session["BalanceSheetData"] as List<BalanceSheetReport>;
            balaceSheet.SetDataSource(data);
            balaceSheet.Subreports[0].DataSourceConnections.Clear();
            balaceSheet.Subreports[0].SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            balaceSheet.Subreports[0].SetDataSource(lData);
            balaceSheet.SetParameterValue("asOnDatef", persister.AsOnDate.ToString("dd-MM-yyyy"));
            balaceSheet.SetParameterValue("asOnDateForSubReport", persister.AsOnDate);
            balaceSheet.SetParameterValue("address", persister.CompAddress);
            balaceSheet.SetParameterValue("compName", persister.CompName);



            //balaceSheet.SetParameterValue("ParentName", "",balaceSheet.Subreports[0].Name.ToString());
            CrystalReportViewer1.ReportSource = balaceSheet;
            CrystalReportViewer1.EnableParameterPrompt = false;
            
            Session["BalanceSheetDoc"] = balaceSheet;
        }
    }
}