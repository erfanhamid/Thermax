using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.CommonInformation;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BEEERP.CrystalReport
{
    public partial class SingleItemSalesDetailsRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleItemSalesDetailsR report = (SingleItemSalesDetailsR)Session["SingleItemSalesDetailDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }

        public void ShowReport()
        {
            SingleItemSalesDetailsR itemS = new SingleItemSalesDetailsR();

            itemS.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\SingleItemSalesDetailsR.rpt");
            itemS.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SingleItemParam"] as ReportParmPersister;
            List<SingleItemSalesDetailsReport> data = HttpContext.Current.Session["SingleItemData"] as List<SingleItemSalesDetailsReport>;
            itemS.SetDataSource(data);

            itemS.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            itemS.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            itemS.SetParameterValue("ItemGroup", persister.ItemGroup);

            CrystalReportViewer1.ReportSource = itemS;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SingleItemSalesDetailDoc"] = itemS;
        }
    }
}