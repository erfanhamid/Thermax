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
    public partial class SingleSupplierBillAgingRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleSupplierBillAgingR report = (SingleSupplierBillAgingR)Session["SingleSupplierBillAgingDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SingleSupplierBillAgingR ssba = new SingleSupplierBillAgingR();

            ssba.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SingleSupplierBillAgingR.rpt");
            ssba.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SSBAparam"] as ReportParmPersister;
            List<SingleSupplierBillAgingReport> data = HttpContext.Current.Session["SSBAData"] as List<SingleSupplierBillAgingReport>;
            ssba.SetDataSource(data);

            //sgsas.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            ssba.SetParameterValue("asDate", persister.ToDate.ToString("dd-MM-yyyy"));
            ssba.SetParameterValue("suppGroup", persister.SupplierGroup.ToString());

            CrystalReportViewer1.ReportSource = ssba;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SingleSupplierBillAgingDoc"] = ssba;
        }
    }
}