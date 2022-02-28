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
    public partial class AllGRNRecordRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                AllGRNRecordR report = (AllGRNRecordR)Session["AllGRNRecordDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            AllGRNRecordR allGRNRecordR = new AllGRNRecordR();

            allGRNRecordR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\AllGRNRecordR.rpt");
            allGRNRecordR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["AllGrnSummaryParam"] as ReportParmPersister;
            List<AllGRNRecordReport> data = HttpContext.Current.Session["AllGRNRecordReportData"] as List<AllGRNRecordReport>;
            allGRNRecordR.SetDataSource(data);

            allGRNRecordR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            allGRNRecordR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = allGRNRecordR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["AllGRNRecordDoc"] = allGRNRecordR;
        }
    }
}