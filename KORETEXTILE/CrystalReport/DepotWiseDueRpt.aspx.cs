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
    public partial class DepotWiseDueRpt : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                DepotWiseDueR report = (DepotWiseDueR)Session["DepotWiseDueDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            DepotWiseDueR due = new DepotWiseDueR();

            due.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\DepotWiseDueR.rpt");
            due.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["DueParam"] as ReportParmPersister;
            List<DepotWiseDueReport> data = HttpContext.Current.Session["DueData"] as List<DepotWiseDueReport>;
            due.SetDataSource(data);

            due.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            due.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = due;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["DepotWiseDueDoc"] = due;
        }
    }
}