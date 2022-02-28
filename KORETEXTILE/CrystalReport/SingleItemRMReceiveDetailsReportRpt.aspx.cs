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
    public partial class SingleItemRMReceiveDetailsReportRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleItemRMReceiveDetailsRR report = (SingleItemRMReceiveDetailsRR)Session["SingleItemRMReceiveDetailsRRDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SingleItemRMReceiveDetailsRR singleItemRMReceiveDetailsRR = new SingleItemRMReceiveDetailsRR();

            singleItemRMReceiveDetailsRR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SingleItemRMReceiveDetailsRR.rpt");
            singleItemRMReceiveDetailsRR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SingleItemRMReceiveDetail2param"] as ReportParmPersister;
            List<SingleItemRMReceiveDetailsReport> data = HttpContext.Current.Session["SingleItemRMReceiveDetailsReportData"] as List<SingleItemRMReceiveDetailsReport>;
            singleItemRMReceiveDetailsRR.SetDataSource(data);

            singleItemRMReceiveDetailsRR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            singleItemRMReceiveDetailsRR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            singleItemRMReceiveDetailsRR.SetParameterValue("itmgroup", persister.ItemGroup);
            singleItemRMReceiveDetailsRR.SetParameterValue("itmname", persister.ItemName);


            CrystalReportViewer1.ReportSource = singleItemRMReceiveDetailsRR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SingleItemRMReceiveDetailsRRDoc"] = singleItemRMReceiveDetailsRR;
        }
    }
}