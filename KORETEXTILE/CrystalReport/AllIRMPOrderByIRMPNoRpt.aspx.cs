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
    public partial class AllIRMPOrderByIRMPNoRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                AllIRMPOrderByIRMPNoR report = (AllIRMPOrderByIRMPNoR)Session["AllIRMPOrderByIRMPNoDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            AllIRMPOrderByIRMPNoR allIRMPOrderByIRMPNoR = new AllIRMPOrderByIRMPNoR();

            allIRMPOrderByIRMPNoR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\allIRMPOrderByIRMPNoR.rpt");
            allIRMPOrderByIRMPNoR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["AllIRMPOrderByIRMPNoParam"] as ReportParmPersister;
            List<ShowAllIRMPPByIRMPNoReport> data = HttpContext.Current.Session["AllIRMPOrderByIRMPNoData"] as List<ShowAllIRMPPByIRMPNoReport>;
            allIRMPOrderByIRMPNoR.SetDataSource(data);

            allIRMPOrderByIRMPNoR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            allIRMPOrderByIRMPNoR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = allIRMPOrderByIRMPNoR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["AllIRMPOrderByIRMPNoDoc"] = allIRMPOrderByIRMPNoR;
        }
    }
}