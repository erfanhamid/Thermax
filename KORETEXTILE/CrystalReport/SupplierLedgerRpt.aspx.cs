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
    public partial class SupplierLedgerRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SupplierLedgerR report = (SupplierLedgerR)Session["SupplierLedgerRDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SupplierLedgerR supplierLedgerR = new SupplierLedgerR();

            supplierLedgerR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SupplierLedgerR.rpt");
            supplierLedgerR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SupplierLedgerParam"] as ReportParmPersister;
            List<SupplierLedgerReport> data = HttpContext.Current.Session["SupplierLedgerData"] as List<SupplierLedgerReport>;
            supplierLedgerR.SetDataSource(data);

            supplierLedgerR.SetParameterValue("address", persister.CompAddress);
            supplierLedgerR.SetParameterValue("compName", persister.CompName);
            supplierLedgerR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            supplierLedgerR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            supplierLedgerR.SetParameterValue("supId", persister.ID);
            supplierLedgerR.SetParameterValue("supName", persister.SupplierName);
            supplierLedgerR.SetParameterValue("SupplierGroup", persister.SupplierGroup);


            CrystalReportViewer1.ReportSource = supplierLedgerR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SupplierLedgerRDoc"] = supplierLedgerR;
        }
    }
}