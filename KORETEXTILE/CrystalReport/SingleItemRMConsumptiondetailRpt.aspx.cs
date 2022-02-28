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
    public partial class SingleItemRMConsumptiondetailRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleItemRMConsumptiondetailR report = (SingleItemRMConsumptiondetailR)Session["SingleItemRMConsumptiondetailDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SingleItemRMConsumptiondetailR singleItemRMConsumptiondetailR = new SingleItemRMConsumptiondetailR();

            singleItemRMConsumptiondetailR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\ItemWiseRMIssueSummaryR.rpt");
            singleItemRMConsumptiondetailR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SingleItemRMConsumptiondetailparam"] as ReportParmPersister;
            List<SingleItemRMConsumptiondetailReport> data = HttpContext.Current.Session["SingleItemRMConsumptiondetailData"] as List<SingleItemRMConsumptiondetailReport>;
            singleItemRMConsumptiondetailR.SetDataSource(data);

            singleItemRMConsumptiondetailR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            singleItemRMConsumptiondetailR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = singleItemRMConsumptiondetailR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SingleItemRMConsumptiondetailDoc"] = singleItemRMConsumptiondetailR;
        }
    }
}