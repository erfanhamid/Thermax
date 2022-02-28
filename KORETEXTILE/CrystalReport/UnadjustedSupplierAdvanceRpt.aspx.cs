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
    public partial class UnadjustedSupplierAdvanceRpt : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                UnadjustedSupplierAdvanceR report = (UnadjustedSupplierAdvanceR)Session["UnadjustedSupplierAdvanceDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            UnadjustedSupplierAdvanceR usa = new UnadjustedSupplierAdvanceR();

            usa.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\UnadjustedSupplierAdvanceR.rpt");
            usa.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["usaparam"] as ReportParmPersister;
            List<UnadjustedSupplierAdvanceReport> data = HttpContext.Current.Session["usaData"] as List<UnadjustedSupplierAdvanceReport>;
            usa.SetDataSource(data);

            usa.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            usa.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = usa;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["UnadjustedSupplierAdvanceDoc"] = usa;
        }
    }
}