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
    public partial class StoreWiseSPInvRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                StoreWiseSPInvSummaryR report = (StoreWiseSPInvSummaryR)Session["StoreWiseSPInvSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            StoreWiseSPInvSummaryR storewspinvsumm = new StoreWiseSPInvSummaryR();

            storewspinvsumm.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\StoreWiseSPInvSummaryR.rpt");
            storewspinvsumm.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["StoreWiseSPInvSummaryparam"] as ReportParmPersister;
            List<StoreWiseSPInvSumReport> data = HttpContext.Current.Session["StoreWiseSPInvSummaryData"] as List<StoreWiseSPInvSumReport>;
            storewspinvsumm.SetDataSource(data);

            storewspinvsumm.SetParameterValue("compAddress", persister.CompAddress);
            storewspinvsumm.SetParameterValue("compName", persister.CompName);
            //dwspinvsumm.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            //dwspinvsumm.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            storewspinvsumm.SetParameterValue("asDate", persister.AsOnDate.ToString("dd-MM-yyyy"));




            CrystalReportViewer1.ReportSource = storewspinvsumm;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["CompanyWiseSPInvSummaryDoc"] = storewspinvsumm;
        }
    }
}