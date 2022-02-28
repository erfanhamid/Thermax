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
    public partial class SingleItemRMReceiveDetailRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleItemRMReceiveDetailR report = (SingleItemRMReceiveDetailR)Session["SingleItemRMReceiveDetailsDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SingleItemRMReceiveDetailR rr = new SingleItemRMReceiveDetailR();

            rr.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SingleItemRMReceiveDetailR.rpt");
            rr.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SingleItemRMReceiveParam"] as ReportParmPersister;
            List<SingleItemRMReceiveDetail> data = HttpContext.Current.Session["SingleItemRMReceiveData"] as List<SingleItemRMReceiveDetail>;
            rr.SetDataSource(data);

            rr.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            rr.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            rr.SetParameterValue("item", persister.ItemName);


            CrystalReportViewer1.ReportSource = rr;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SingleItemRMReceiveDetailsDoc"] = rr;
        }
    }
}