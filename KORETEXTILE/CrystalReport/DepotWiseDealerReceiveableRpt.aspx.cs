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
    public partial class DepotWiseDealerReceiveableRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                DepotWiseDealerReceivableR report = (DepotWiseDealerReceivableR)Session["DepotWiseDealerReceivableSS"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            DepotWiseDealerReceivableR depotWiseDealerReceivableR = new DepotWiseDealerReceivableR();

            depotWiseDealerReceivableR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\DepotWiseDealerReceivableR.rpt");
            depotWiseDealerReceivableR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["DepotWiseDealerReceiveableParam"] as ReportParmPersister;
            List<DepotWiseDealerReceiveableReport> data = HttpContext.Current.Session["DepotWiseDealerReceiveableData"] as List<DepotWiseDealerReceiveableReport>;

            depotWiseDealerReceivableR.SetDataSource(data);

            depotWiseDealerReceivableR.SetParameterValue("AsDate", persister.AsOnDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = depotWiseDealerReceivableR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["DepotWiseDealerReceivableSS"] = depotWiseDealerReceivableR;
        }
    }
}