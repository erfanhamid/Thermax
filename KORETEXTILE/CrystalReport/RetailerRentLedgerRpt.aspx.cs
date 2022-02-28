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
    public partial class RetailerRentLedgerRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                RetailerRentLedgerR report = (RetailerRentLedgerR)Session["RetailerRentLedgerRDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            RetailerRentLedgerR retailerRentLedgerR = new RetailerRentLedgerR();

            retailerRentLedgerR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\RetailerRentLedgerR.rpt");
            retailerRentLedgerR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["RetailerRentLedgerParam"] as ReportParmPersister;
            List<RetailerRentLedgerReport> data = HttpContext.Current.Session["RetailerRentLedgerData"] as List<RetailerRentLedgerReport>;
            retailerRentLedgerR.SetDataSource(data);

            //retailerRentLedgerR.SetParameterValue("address", persister.CompAddress);
            //retailerRentLedgerR.SetParameterValue("compName", persister.CompName);
            retailerRentLedgerR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            retailerRentLedgerR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            //retailerRentLedgerR.SetParameterValue("supId", persister.ID);
            //retailerRentLedgerR.SetParameterValue("supName", persister.SupplierName);
            //retailerRentLedgerR.SetParameterValue("SupplierGroup", persister.SupplierGroup);


            CrystalReportViewer1.ReportSource = retailerRentLedgerR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["RetailerRentLedgerRDoc"] = retailerRentLedgerR;
        }
    }
}