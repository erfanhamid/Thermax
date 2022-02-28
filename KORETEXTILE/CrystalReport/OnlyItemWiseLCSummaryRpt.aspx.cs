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
    public partial class OnlyItemWiseLCSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                OnlyItemWiseLCSummaryR report = (OnlyItemWiseLCSummaryR)Session["OnlyItemWiseLCSummaryRDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            OnlyItemWiseLCSummaryR onlyitemWiseLCSummaryR = new OnlyItemWiseLCSummaryR();

            onlyitemWiseLCSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\OnlyItemWiseLCSummaryR.rpt");
            onlyitemWiseLCSummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["OnlyItemWiseLCSummaryParam"] as ReportParmPersister;
            List<OnlyItemWiseLCSummary> data = HttpContext.Current.Session["OnlyItemWiseLCSummaryData"] as List<OnlyItemWiseLCSummary>;
            onlyitemWiseLCSummaryR.SetDataSource(data);

            onlyitemWiseLCSummaryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            onlyitemWiseLCSummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = onlyitemWiseLCSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["OnlyItemWiseLCSummaryRDoc"] = onlyitemWiseLCSummaryR;
        }
    }
}