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
    public partial class AllLATROBRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                AllLATROBR report = (AllLATROBR)Session["LATROBDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            AllLATROBR LATROB = new AllLATROBR();

            LATROB.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\ALLLATRAR.rpt");
            LATROB.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["LATROBParam"] as ReportParmPersister;
            List<AllLATROBReport> data = HttpContext.Current.Session["LATROBData"] as List<AllLATROBReport>;
            LATROB.SetDataSource(data);

            LATROB.SetParameterValue("compAddress", persister.CompAddress);
            LATROB.SetParameterValue("compName", persister.CompName);
            LATROB.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            LATROB.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = LATROB;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["LATROBDoc"] = LATROB;
        }
    }
}