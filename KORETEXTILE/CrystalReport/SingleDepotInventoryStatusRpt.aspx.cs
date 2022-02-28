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
    public partial class SingleDepotInventoryStatusRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleDepotInventoryStatusR report = (SingleDepotInventoryStatusR)Session["SingleDepotInventoryStatusDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SingleDepotInventoryStatusR status = new SingleDepotInventoryStatusR();
            status.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\SingleDepotInventoryStatusR.rpt");
            status.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["InventoryStatusParam"] as ReportParmPersister;
            List<SingleDepotInventoryStatusReport> data = HttpContext.Current.Session["InventoryStatusData"] as List<SingleDepotInventoryStatusReport>;
            status.SetDataSource(data);

            //vExpense.SetParameterValue("accountName", persister.AccountName);
            status.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            status.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            status.SetParameterValue("Depot", persister.DepotName);

            CrystalReportViewer1.ReportSource = status;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SingleDepotInventoryStatusDoc"] = status;
        }
    }
}