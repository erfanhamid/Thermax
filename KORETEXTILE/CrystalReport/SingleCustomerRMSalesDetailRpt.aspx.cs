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
    public partial class SingleCustomerRMSalesDetailRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleCustomerRMSalesDetailR report = (SingleCustomerRMSalesDetailR)Session["SingleCustomerRMSalesDetailDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SingleCustomerRMSalesDetailR singleCustomerRMSalesDetailR = new SingleCustomerRMSalesDetailR();

            singleCustomerRMSalesDetailR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SingleCustomerRMSalesDetailR.rpt");
            singleCustomerRMSalesDetailR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SingleCustomerRMSalesDetailparam"] as ReportParmPersister;
            List<SingleCustomerRMSalesDetailReport> data = HttpContext.Current.Session["SingleCustomerRMSalesDetailData"] as List<SingleCustomerRMSalesDetailReport>;
            singleCustomerRMSalesDetailR.SetDataSource(data);

            singleCustomerRMSalesDetailR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            singleCustomerRMSalesDetailR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            //rr.SetParameterValue("item", persister.ItemName);


            CrystalReportViewer1.ReportSource = singleCustomerRMSalesDetailR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SingleCustomerRMSalesDetailDoc"] = singleCustomerRMSalesDetailR;
        }
    }
}