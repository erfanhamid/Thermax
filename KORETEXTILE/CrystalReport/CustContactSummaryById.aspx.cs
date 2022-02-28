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
    public partial class CustContactSummaryById : System.Web.UI.Page
    {
      
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                CustConSummaryByIdR report = (CustConSummaryByIdR)Session["CustContactSummaryByIdDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            CustConSummaryByIdR custConSummaryByIdR = new CustConSummaryByIdR();

            custConSummaryByIdR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\CustConSummaryByIdR.rpt");
            custConSummaryByIdR.Load(APPPATH);

            List<CustConSummaryByIdReport> data = HttpContext.Current.Session["CustConSummaryByIdReportData"] as List<CustConSummaryByIdReport>;
            custConSummaryByIdR.SetDataSource(data);


            custConSummaryByIdR.SetParameterValue("address", "");
            custConSummaryByIdR.SetParameterValue("compName", "");

            CrystalReportViewer1.ReportSource = custConSummaryByIdR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["CustContactSummaryByIdDoc"] = custConSummaryByIdR;
        }
    }
}