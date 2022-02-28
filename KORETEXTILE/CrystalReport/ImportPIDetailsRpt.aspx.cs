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
    public partial class ImportPIDetailsRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                ImportPIDetailsR report = (ImportPIDetailsR)Session["ImportPIDetailsDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            ImportPIDetailsR ImportPIDetails = new ImportPIDetailsR();
            ImportPIDetails.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\ImportPIDetailsR.rpt");
            ImportPIDetails.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["ImportPIReportparam"] as ReportParmPersister;
            List<ImportPIDetailsReport> data = HttpContext.Current.Session["ImportPIReportData"] as List<ImportPIDetailsReport>;
            ImportPIDetails.SetDataSource(data);

            ImportPIDetails.SetParameterValue("SupplierName", persister.SupplierName);
            ImportPIDetails.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            ImportPIDetails.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            ImportPIDetails.SetParameterValue("compName", persister.CompName);
            ImportPIDetails.SetParameterValue("compAddress", persister.CompAddress);


            CrystalReportViewer1.ReportSource = ImportPIDetails;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ImportPIDetailsDoc"] = ImportPIDetails;
        }
    }
}