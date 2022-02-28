using System;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.CommonInformation;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BEEERP.CrystalReport
{
    public partial class DateWisSADASummaryNewRpt : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                DateWisSADASummaryR report = (DateWisSADASummaryR)Session["DateWisSADASummaryRDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            DateWisSADASummaryR dateWisSADASummaryR = new DateWisSADASummaryR();

            dateWisSADASummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\DateWisSADASummaryR.rpt");
            dateWisSADASummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SADASummaryparam"] as ReportParmPersister;
            List<DateWiseSADASummaryReport> data = HttpContext.Current.Session["SADASummaryData"] as List<DateWiseSADASummaryReport>;
            dateWisSADASummaryR.SetDataSource(data);

            dateWisSADASummaryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            dateWisSADASummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = dateWisSADASummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["BillWisSADASummaryDoc"] = dateWisSADASummaryR;
        }
    }
}