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
    public partial class CompanyWiseSPInvRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                CompanyWiseSPInvSummaryR report = (CompanyWiseSPInvSummaryR)Session["CompanyWiseSPInvSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            CompanyWiseSPInvSummaryR twspinvsumm = new CompanyWiseSPInvSummaryR();

            twspinvsumm.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\CompanyWiseSPInvSummaryR.rpt");
            twspinvsumm.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["CompWiseSPInvSummaryparam"] as ReportParmPersister;
            List<CompWiseSPInvSumReport> data = HttpContext.Current.Session["CompWiseSPInvSummaryData"] as List<CompWiseSPInvSumReport>;
            twspinvsumm.SetDataSource(data);

            twspinvsumm.SetParameterValue("compAddress", persister.CompAddress);
            twspinvsumm.SetParameterValue("compName", persister.CompName);
            //dwspinvsumm.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            //dwspinvsumm.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            twspinvsumm.SetParameterValue("asDate", persister.AsOnDate.ToString("dd-MM-yyyy"));




            CrystalReportViewer1.ReportSource = twspinvsumm;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["CompanyWiseSPInvSummaryDoc"] = twspinvsumm;
        }
    }
}