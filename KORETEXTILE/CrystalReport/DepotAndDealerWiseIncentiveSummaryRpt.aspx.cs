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
    public partial class DepotAndDealerWiseIncentiveSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                DepotAndDealerWiseIncentiveSummaryR report = (DepotAndDealerWiseIncentiveSummaryR)Session["IncentiveSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            DepotAndDealerWiseIncentiveSummaryR Inc = new DepotAndDealerWiseIncentiveSummaryR();

            Inc.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\DepotAndDealerWiseIncentiveSummaryR.rpt");
            Inc.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["IncParam"] as ReportParmPersister;
            List<DepotAndDealerWiseIncentiveSummaryReport> data = HttpContext.Current.Session["IncData"] as List<DepotAndDealerWiseIncentiveSummaryReport>;
            Inc.SetDataSource(data);
            Inc.SetParameterValue("AsOnDate", persister.AsOnDate.ToString("dd-MM-yyyy"));
            Inc.SetParameterValue("ReportName", Session["ReportName"]);




            CrystalReportViewer1.ReportSource = Inc;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["IncentiveSummaryDoc"] = Inc;
        }
    }
}