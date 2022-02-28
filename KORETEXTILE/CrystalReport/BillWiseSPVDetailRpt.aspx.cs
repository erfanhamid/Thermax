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
    public partial class BillWiseSPVDetailRpt : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                BillWiseSPVDetailR report = (BillWiseSPVDetailR)Session["BillWiseSPVDetailDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            BillWiseSPVDetailR bwspvd = new BillWiseSPVDetailR();

            bwspvd.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\BillWiseSPVDetailR.rpt");
            bwspvd.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["bwspvdparam"] as ReportParmPersister;
            List<BillWiseSPVDetailReport> data = HttpContext.Current.Session["bwspvdData"] as List<BillWiseSPVDetailReport>;
            bwspvd.SetDataSource(data);

            bwspvd.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            bwspvd.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = bwspvd;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["BillWiseSPVDetailDoc"] = bwspvd;
        }
    }
}