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
    public partial class AllIRMPOrderByIRMPDateRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                AllIRMPOrderByDateR report = (AllIRMPOrderByDateR)Session["AllIRMPOrderByDateDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            AllIRMPOrderByDateR allIRMPOrderByDateR = new AllIRMPOrderByDateR();

            allIRMPOrderByDateR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\allIRMPOrderByIRMPNoR.rpt");
            allIRMPOrderByDateR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["AllIRMPOrderByDateParam"] as ReportParmPersister;
            List<AllIRMPOrderByDateReport> data = HttpContext.Current.Session["AllIRMPOrderByDateData"] as List<AllIRMPOrderByDateReport>;
            allIRMPOrderByDateR.SetDataSource(data);

            allIRMPOrderByDateR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            allIRMPOrderByDateR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = allIRMPOrderByDateR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["AllIRMPOrderByDateDoc"] = allIRMPOrderByDateR;
        }
    }
}