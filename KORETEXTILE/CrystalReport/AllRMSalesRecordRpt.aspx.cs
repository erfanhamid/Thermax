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
    public partial class AllRMSalesRecordRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                AllRMSalesRecordR report = (AllRMSalesRecordR)Session["AllRMSalesRecordDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            AllRMSalesRecordR allRMSalesRecordR = new AllRMSalesRecordR();

            allRMSalesRecordR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\allRMSalesRecordR.rpt");
            allRMSalesRecordR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["AllRMSalesRecordParam"] as ReportParmPersister;
            List<AllRMSalesRecordReport> data = HttpContext.Current.Session["AllRMSalesRecordData"] as List<AllRMSalesRecordReport>;
            allRMSalesRecordR.SetDataSource(data);

            allRMSalesRecordR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            allRMSalesRecordR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = allRMSalesRecordR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["AllRMSalesRecordDoc"] = allRMSalesRecordR;
        }
    }
}