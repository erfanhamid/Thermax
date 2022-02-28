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
    public partial class SingleGroupSupplierBillAgingRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleGroupSupplierBillAgingR report = (SingleGroupSupplierBillAgingR)Session["SingleGroupSupplierBillAgingDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SingleGroupSupplierBillAgingR sgsba = new SingleGroupSupplierBillAgingR();

            sgsba.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SingleGroupSupplierBillAgingR.rpt");
            sgsba.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SGSBAparam"] as ReportParmPersister;
            List<SingleGroupSupplierBillAgingReport> data = HttpContext.Current.Session["SGSBAData"] as List<SingleGroupSupplierBillAgingReport>;
            sgsba.SetDataSource(data);

            //sgsas.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            sgsba.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            sgsba.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            //sgsba.SetParameterValue("sgid", persister.SupplierGroupID.ToString());
            sgsba.SetParameterValue("sgid", persister.SupplierGroupID);
            sgsba.SetParameterValue("sgname", persister.SupplierGroup.ToString());


            CrystalReportViewer1.ReportSource = sgsba;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SingleGroupSupplierBillAgingDoc"] = sgsba;
        }
    }
}