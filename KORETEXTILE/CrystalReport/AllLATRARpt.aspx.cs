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
    public partial class AllLATRARpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                ALLLATRAR report = (ALLLATRAR)Session["LATRADoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            ALLLATRAR LATRA = new ALLLATRAR();

            LATRA.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\ALLLATRAR.rpt");
            LATRA.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["LATRAParam"] as ReportParmPersister;
            List<ALLLATRAReport> data = HttpContext.Current.Session["LATRAData"] as List<ALLLATRAReport>;
            LATRA.SetDataSource(data);

            LATRA.SetParameterValue("compAddress", persister.CompAddress);
            LATRA.SetParameterValue("compName", persister.CompName);
            LATRA.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            LATRA.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = LATRA;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["LATRADoc"] = LATRA;
        }
    }
}