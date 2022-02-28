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
    public partial class YettoreceiveworkorderRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                YettoreceiveworkorderR report = (YettoreceiveworkorderR)Session["YettoreceiveworkorderDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            YettoreceiveworkorderR yettoreceiveworkorderR = new YettoreceiveworkorderR();

            yettoreceiveworkorderR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\YettoreceiveworkorderR.rpt");
            yettoreceiveworkorderR.Load(APPPATH);

            //ReportParmPersister persister = HttpContext.Current.Session["AllGrnSummaryParam"] as ReportParmPersister;
            List<Yettoreceiveworkorderreport> data = HttpContext.Current.Session["YettoreceiveworkorderData"] as List<Yettoreceiveworkorderreport>;
            yettoreceiveworkorderR.SetDataSource(data);

            //yettoreceiveworkorderR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            //yettoreceiveworkorderR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = yettoreceiveworkorderR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["YettoreceiveworkorderDoc"] = yettoreceiveworkorderR;
        }
    }
}