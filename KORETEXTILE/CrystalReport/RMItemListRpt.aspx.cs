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
    public partial class RMItemListRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                RMItemListR report = (RMItemListR)Session["ReportDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            RMItemListR rmItemListR = new RMItemListR();

            rmItemListR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\RMItemListR.rpt");
            rmItemListR.Load(APPPATH);

            List<ItemListReport> data = HttpContext.Current.Session["ItemListReportData"] as List<ItemListReport>;
            rmItemListR.SetDataSource(data);

            rmItemListR.SetParameterValue("address", "");
            rmItemListR.SetParameterValue("compName", "");
            rmItemListR.SetParameterValue("AsOnDate", DateTime.Now.ToString("dd-MM-yyyy"));
            CrystalReportViewer1.ReportSource = rmItemListR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ReportDoc"] = rmItemListR;
        }
    }
}