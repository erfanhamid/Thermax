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
    public partial class SingleSupplierGRNSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleSupplierGRNSummaryR report = (SingleSupplierGRNSummaryR)Session["SingleSupplierGRNSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SingleSupplierGRNSummaryR singleSupplierGRNSummaryR = new SingleSupplierGRNSummaryR();

            singleSupplierGRNSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SingleSupplierGRNSummaryR.rpt");
            singleSupplierGRNSummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SingleSupplierGRNSummaryparam"] as ReportParmPersister;
            List<SingleSupplierGRNSummaryReport> data = HttpContext.Current.Session["SingleSupplierGRNSummaryData"] as List<SingleSupplierGRNSummaryReport>;
            singleSupplierGRNSummaryR.SetDataSource(data);

            singleSupplierGRNSummaryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            singleSupplierGRNSummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            //rr.SetParameterValue("item", persister.ItemName);


            CrystalReportViewer1.ReportSource = singleSupplierGRNSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SingleSupplierGRNSummaryDoc"] = singleSupplierGRNSummaryR;
        }
    }
}