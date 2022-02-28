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
    public partial class RMInentoryQtyBalancDetailRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                RMInentoryQtyBalancDetailR report = (RMInentoryQtyBalancDetailR)Session["RMInentoryQtyBalancDetailDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            RMInentoryQtyBalancDetailR rMInentoryQtyBalancDetailR = new RMInentoryQtyBalancDetailR();

            rMInentoryQtyBalancDetailR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\RMInentoryQtyBalancDetailR.rpt");
            rMInentoryQtyBalancDetailR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["RMInentoryQtyBalancDetailparam"] as ReportParmPersister;
            List<RMInentoryQtyBalancDetailReport> data = HttpContext.Current.Session["RMInentoryQtyBalancDetailData"] as List<RMInentoryQtyBalancDetailReport>;
            rMInentoryQtyBalancDetailR.SetDataSource(data);


            //rMInentoryQtyBalancDetailR.SetParameterValue("address", "");
            //rMInentoryQtyBalancDetailR.SetParameterValue("compName", "");
            rMInentoryQtyBalancDetailR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            rMInentoryQtyBalancDetailR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = rMInentoryQtyBalancDetailR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["RMInentoryQtyBalancDetailDoc"] = rMInentoryQtyBalancDetailR;
        }
    }
}