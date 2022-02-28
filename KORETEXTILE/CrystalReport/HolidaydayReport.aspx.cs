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
    public partial class HolidaydayReport : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                HolidayDetails report = (HolidayDetails)Session["ReportDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            HolidayDetails holiday = new HolidayDetails();

            holiday.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\HolidayDetails.rpt");
            holiday.Load(APPPATH);

            List<HolidayDetailsR> data = HttpContext.Current.Session["HolidayDetailsData"] as List<HolidayDetailsR>;
            holiday.SetDataSource(data);
            ReportParmPersister persister = HttpContext.Current.Session["HolidayDetailsParam"] as ReportParmPersister;

            holiday.SetParameterValue("mYear",persister.Year );
            holiday.SetParameterValue("mMonth", persister.Month);
            holiday.SetParameterValue("compAddress", persister.CompAddress);
            holiday.SetParameterValue("CompName", persister.CompName);

            CrystalReportViewer1.ReportSource = holiday;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ReportDoc"] = holiday;
        }
    }
}