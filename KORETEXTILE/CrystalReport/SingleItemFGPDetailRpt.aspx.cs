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
    public partial class SingleItemFGPDetailRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleItemFGPDetailR report = (SingleItemFGPDetailR)Session["SingleItemFGPDetailDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SingleItemFGPDetailR singleItemFGPDetailR = new SingleItemFGPDetailR();

            singleItemFGPDetailR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SingleItemFGPDetailR.rpt");
            singleItemFGPDetailR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SingleItemFGPDetailparam"] as ReportParmPersister;
            List<SingleItemFGPDetailReport> data = HttpContext.Current.Session["SingleItemFGPDetailData"] as List<SingleItemFGPDetailReport>;
            singleItemFGPDetailR.SetDataSource(data);

            singleItemFGPDetailR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            singleItemFGPDetailR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = singleItemFGPDetailR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SingleItemFGPDetailDoc"] = singleItemFGPDetailR;
        }
    }
}