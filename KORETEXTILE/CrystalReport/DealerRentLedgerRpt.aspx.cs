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
    public partial class DealerRentLedgerRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                DealerRentLedgerR report = (DealerRentLedgerR)Session["DealerRentLedgerDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            DealerRentLedgerR drl = new DealerRentLedgerR();

            drl.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\DealerRentLedgerR.rpt");
            drl.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["DealerRentLedgerParam"] as ReportParmPersister;
            List<DealerRentLedgerReport> data = HttpContext.Current.Session["DealerRentLedgerData"] as List<DealerRentLedgerReport>;
            drl.SetDataSource(data);

            //drl.SetParameterValue("address", persister.CompAddress);
            //drl.SetParameterValue("compName", persister.CompName);
            drl.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            drl.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            drl.SetParameterValue("did", persister.DID);
            drl.SetParameterValue("cName", persister.CustName);
            drl.SetParameterValue("depotName", persister.DepotName);


            CrystalReportViewer1.ReportSource = drl;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["DealerRentLedgerDoc"] = drl;
        }
    }
}