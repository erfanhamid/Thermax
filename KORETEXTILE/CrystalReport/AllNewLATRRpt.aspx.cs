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
    public partial class AllNewLATRRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                AllNewLATRR report = (AllNewLATRR)Session["LATRNewDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            AllNewLATRR LATR = new AllNewLATRR();

            LATR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\AllNewLATRR.rpt");
            LATR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["LATRNewParam"] as ReportParmPersister;
            List<AllNewLATRReport> data = HttpContext.Current.Session["LATRNewData"] as List<AllNewLATRReport>;
            LATR.SetDataSource(data);

            LATR.SetParameterValue("compAddress", persister.CompAddress);
            LATR.SetParameterValue("compName", persister.CompName);
            LATR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            LATR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = LATR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["LATRNewDoc"] = LATR;
        }
    }
}