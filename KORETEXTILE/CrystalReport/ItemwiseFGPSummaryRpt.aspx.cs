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
    public partial class ItemwiseFGPSummaryRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                ItemwiseFGPSummaryR report = (ItemwiseFGPSummaryR)Session["ItemwiseFGPSummaryDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            ItemwiseFGPSummaryR itemwiseFGPSummaryR = new ItemwiseFGPSummaryR();

            itemwiseFGPSummaryR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\ItemwiseFGPSummaryR.rpt");
            itemwiseFGPSummaryR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["ItemwiseFGPSummaryParam"] as ReportParmPersister;
            List<ItemwiseFGPSummaryReport> data = HttpContext.Current.Session["ItemwiseFGPSummaryData"] as List<ItemwiseFGPSummaryReport>;
            itemwiseFGPSummaryR.SetDataSource(data);

            itemwiseFGPSummaryR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            itemwiseFGPSummaryR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = itemwiseFGPSummaryR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ItemwiseFGPSummaryDoc"] = itemwiseFGPSummaryR;
        }
    }
}