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
    public partial class ItemwisegwosummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                ItemwisegwosummaryR report = (ItemwisegwosummaryR)Session["ItemwisegwosummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            ItemwisegwosummaryR itemwisegwosummaryR = new ItemwisegwosummaryR();

            itemwisegwosummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\ItemwisegwosummaryR.rpt");
            itemwisegwosummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["Itemwisegwosummaryparam"] as ReportParmPersister;
            List<ItemwisewosummaryReport> data = HttpContext.Current.Session["ItemwisegwosummaryData"] as List<ItemwisewosummaryReport>;
            itemwisegwosummaryR.SetDataSource(data);

            //supplierwisewosummaryR.SetParameterValue("compAddress", persister.CompAddress);
            //supplierwisewosummaryR.SetParameterValue("compName", persister.CompName);
            itemwisegwosummaryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            itemwisegwosummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            //supplierwisewosummaryR.SetParameterValue("reportName", Session["ReportName"]);




            CrystalReportViewer1.ReportSource = itemwisegwosummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ItemwisegwosummaryDoc"] = itemwisegwosummaryR;
        }
    }
}