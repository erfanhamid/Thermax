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
    public partial class SingleItemRMSalesDetailRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleItemRMSalesDetailR report = (SingleItemRMSalesDetailR)Session["SingleItemRMSalesDetailDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SingleItemRMSalesDetailR singleItemRMSalesDetailR = new SingleItemRMSalesDetailR();

            singleItemRMSalesDetailR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SingleItemRMIssueDetailR.rpt");
            singleItemRMSalesDetailR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SingleItemRMSalesDetailparam"] as ReportParmPersister;
            List<SingleItemRMSalesDetailReport> data = HttpContext.Current.Session["SingleItemRMSalesDetailData"] as List<SingleItemRMSalesDetailReport>;
            singleItemRMSalesDetailR.SetDataSource(data);

            singleItemRMSalesDetailR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            singleItemRMSalesDetailR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            singleItemRMSalesDetailR.SetParameterValue("item", persister.ItemName);


            CrystalReportViewer1.ReportSource = singleItemRMSalesDetailR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SingleItemRMSalesDetailDoc"] = singleItemRMSalesDetailR;
        }
    }
}