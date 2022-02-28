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
    public partial class ItemListRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                ItemListR report = (ItemListR)Session["ReportDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            ItemListR itemListR = new ItemListR();

            itemListR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\ItemListR.rpt");
            itemListR.Load(APPPATH);

            List<ItemListReport> data = HttpContext.Current.Session["ItemListReportData"] as List<ItemListReport>;
            itemListR.SetDataSource(data);

            itemListR.SetParameterValue("address", "");
            itemListR.SetParameterValue("compName", "");
            itemListR.SetParameterValue("AsOnDate", DateTime.Now.ToString("dd-MM-yyyy"));
            CrystalReportViewer1.ReportSource = itemListR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ReportDoc"] = itemListR;
        }
    }
}