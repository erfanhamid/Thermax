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
    public partial class SingleDepotDealerReceiveableRpt : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleDepotDealerReceiveableR report = (SingleDepotDealerReceiveableR)Session["DealerReceiveableDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SingleDepotDealerReceiveableR due = new SingleDepotDealerReceiveableR();

            due.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\SingleDepotDealerReceiveableR.rpt");
            due.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["RevParam"] as ReportParmPersister;
            List<SingleDepotDealerReceiveable> data = HttpContext.Current.Session["RevData"] as List<SingleDepotDealerReceiveable>;
            due.SetDataSource(data);

            due.SetParameterValue("asOn", persister.AsOnDate.ToString("dd-MM-yyyy"));
            CrystalReportViewer1.ReportSource = due;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["DealerReceiveableDoc"] = due;
        }
    }
}