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
    public partial class SingleGroupSupplierAgingRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleGroupSupplierAgingR report = (SingleGroupSupplierAgingR)Session["SingleGroupSupplierAgingDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SingleGroupSupplierAgingR sgsas = new SingleGroupSupplierAgingR();

            sgsas.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\BatchitemwiseFGPSummaryR.rpt");
            sgsas.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SGSAparam"] as ReportParmPersister;
            List<SingleGroupSupplierAgingReport> data = HttpContext.Current.Session["SGSAData"] as List<SingleGroupSupplierAgingReport>;
            sgsas.SetDataSource(data);

            //sgsas.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            sgsas.SetParameterValue("asDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = sgsas;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SingleGroupSupplierAgingDoc"] = sgsas;
        }
    }
}