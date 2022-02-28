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
    public partial class SupplierBalanceDetailsRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SupplierBalanceDetailsR report = (SupplierBalanceDetailsR)Session["SupplierBalanceDetailsDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SupplierBalanceDetailsR supbalance = new SupplierBalanceDetailsR();
            supbalance.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\SupplierBalanceDetailsR.rpt");
            supbalance.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["Supbalanceparam"] as ReportParmPersister;
            List<SupplierBalanceDetailsReport> data = HttpContext.Current.Session["SupbalanceData"] as List<SupplierBalanceDetailsReport>;
            supbalance.SetDataSource(data);

            supbalance.SetParameterValue("SupplierGroup", persister.SupplierGroup);
            supbalance.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            supbalance.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
   


            CrystalReportViewer1.ReportSource = supbalance;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SupplierBalanceDetailsDoc"] = supbalance;
        }
    }
}