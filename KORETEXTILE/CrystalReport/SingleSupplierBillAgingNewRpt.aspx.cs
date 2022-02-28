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
    public partial class SingleSupplierBillAgingNewRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleSupplierBillAgingNewR report = (SingleSupplierBillAgingNewR)Session["SingleSupplierBillAgingNewDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SingleSupplierBillAgingNewR ssbanew = new SingleSupplierBillAgingNewR();

            ssbanew.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SingleSupplierBillAgingNewR.rpt");
            ssbanew.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SSBANewparam"] as ReportParmPersister;
            List<SingleSupplierBillAgingNewReport> data = HttpContext.Current.Session["SSBANewData"] as List<SingleSupplierBillAgingNewReport>;
            ssbanew.SetDataSource(data);

            //sgsas.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            ssbanew.SetParameterValue("asDate", persister.AsOnDate.ToString("dd-MM-yyyy"));
            ssbanew.SetParameterValue("suppGroup", persister.SupplierGroup.ToString());

            CrystalReportViewer1.ReportSource = ssbanew;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SingleSupplierBillAgingNewDoc"] = ssbanew;
        }
    }
}