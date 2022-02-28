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
    public partial class CustomerAgeingRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                CustomerAgeingR report = (CustomerAgeingR)Session["CustomerAgeingSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            CustomerAgeingR customeInfor = new CustomerAgeingR();

            customeInfor.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\CustomerAgeingR.rpt");
            customeInfor.Load(APPPATH);

            List<CustomerAgeingReport> data = HttpContext.Current.Session["CustomerAgeingSummaryData"] as List<CustomerAgeingReport>;

            customeInfor.SetDataSource(data);
            ReportParmPersister persister = HttpContext.Current.Session["CustomerAgeingSummaryParam"] as ReportParmPersister;

            customeInfor.SetParameterValue("address", persister.CompAddress);
            customeInfor.SetParameterValue("compName", persister.CompName);
            customeInfor.SetParameterValue("asOnDates", persister.AsOnDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = customeInfor;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["CustomerAgeingSummaryDoc"] = customeInfor;
        }
    }
}