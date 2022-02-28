using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BEEERP.CrystalReport
{
    public partial class TsoWiseCustDetailsRpt : System.Web.UI.Page
    {

        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                TsoWiseCustomerDetailR report = (TsoWiseCustomerDetailR)Session["TsoWiseCustomerDetailDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            TsoWiseCustomerDetailR tsoWiseCustomerDetailR = new TsoWiseCustomerDetailR();

            tsoWiseCustomerDetailR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\TsoWiseCustomerDetailR.rpt");
            tsoWiseCustomerDetailR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["TsoWiseCustDetails"] as ReportParmPersister;
            List<TsoWiseCustDetailsReport> data = HttpContext.Current.Session["TsoWiseCustDetailsRptData"] as List<TsoWiseCustDetailsReport>;
            tsoWiseCustomerDetailR.SetDataSource(data);


            tsoWiseCustomerDetailR.SetParameterValue("address", "");
            tsoWiseCustomerDetailR.SetParameterValue("compName", "");
            tsoWiseCustomerDetailR.SetParameterValue("tsoName", persister.TsoName);


            CrystalReportViewer1.ReportSource = tsoWiseCustomerDetailR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["TsoWiseCustomerDetailDoc"] = tsoWiseCustomerDetailR;
        }
    }
}