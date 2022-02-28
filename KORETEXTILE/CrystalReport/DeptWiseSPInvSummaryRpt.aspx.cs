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
    public partial class DeptWiseSPInvSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                DeptWiseSPInvSummaryR report = (DeptWiseSPInvSummaryR)Session["DeptWiseSPInvSummaryRDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            DeptWiseSPInvSummaryR dwspinvsumm = new DeptWiseSPInvSummaryR();

            dwspinvsumm.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\DeptWiseSPInvSummaryR.rpt");
            dwspinvsumm.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["DeptWiseSPInvSummaryparam"] as ReportParmPersister;
            List<TypeWiseSPInvSumReport> data = HttpContext.Current.Session["DeptWiseSPInvSummaryData"] as List<TypeWiseSPInvSumReport>;
            dwspinvsumm.SetDataSource(data);

            dwspinvsumm.SetParameterValue("compAddress", persister.CompAddress);
            dwspinvsumm.SetParameterValue("compName", persister.CompName);
            //dwspinvsumm.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            //dwspinvsumm.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            dwspinvsumm.SetParameterValue("asDate", persister.AsOnDate.ToString("dd-MM-yyyy"));




            CrystalReportViewer1.ReportSource = dwspinvsumm;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["DeptWiseSPInvSummaryRDoc"] = dwspinvsumm;
        }
    }
}