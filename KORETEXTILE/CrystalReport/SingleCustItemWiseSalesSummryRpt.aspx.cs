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
    public partial class SingleCustItemWiseSalesSummryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleCustItemWiseSalesSummryR report = (SingleCustItemWiseSalesSummryR)Session["ReportDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SingleCustItemWiseSalesSummryR singleCustItemWiseSalesSummryR = new SingleCustItemWiseSalesSummryR();

            singleCustItemWiseSalesSummryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SingleCustItemWiseSalesSummryR.rpt");
            singleCustItemWiseSalesSummryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SingleCustItemWiseSalesSummry"] as ReportParmPersister;
            List<SingleCustItemWiseSalesSummryReport> data = HttpContext.Current.Session["SingleCustItemWiseSalesSummryReportData"] as List<SingleCustItemWiseSalesSummryReport>;
            singleCustItemWiseSalesSummryR.SetDataSource(data);

            singleCustItemWiseSalesSummryR.SetParameterValue("address", "");
            singleCustItemWiseSalesSummryR.SetParameterValue("compName", "");
            singleCustItemWiseSalesSummryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            singleCustItemWiseSalesSummryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            singleCustItemWiseSalesSummryR.SetParameterValue("custName", persister.CustName);

            CrystalReportViewer1.ReportSource = singleCustItemWiseSalesSummryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ReportDoc"] = singleCustItemWiseSalesSummryR;
        }
    }
}