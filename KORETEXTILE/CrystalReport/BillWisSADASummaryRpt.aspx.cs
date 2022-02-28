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
    public partial class BillWisSADASummaryRpt : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                BillWisSADASummaryR report = (BillWisSADASummaryR)Session["BillWisSADASummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            BillWisSADASummaryR billWisSADASummaryR = new BillWisSADASummaryR();

            billWisSADASummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\BillWisSADASummaryR.rpt");
            billWisSADASummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["BillWisSADASummaryparam"] as ReportParmPersister;
            List<BillWiseSADASummaryReport> data = HttpContext.Current.Session["BillWisSADASummaryData"] as List<BillWiseSADASummaryReport>;
            billWisSADASummaryR.SetDataSource(data);

            billWisSADASummaryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            billWisSADASummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = billWisSADASummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["BillWisSADASummaryDoc"] = billWisSADASummaryR;
        }
    }
}