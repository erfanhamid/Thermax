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
    public partial class AllworkorderbywonoRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                AllWorkOrderByWONoR report = (AllWorkOrderByWONoR)Session["AllWorkOrderByWONoRDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            AllWorkOrderByWONoR allWorkOrderByWONoR = new AllWorkOrderByWONoR();

            allWorkOrderByWONoR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\allIRMPOrderByIRMPNoR.rpt");
            allWorkOrderByWONoR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["AllWorkOrderByWONoparam"] as ReportParmPersister;
            List<Allworkorderbywono> data = HttpContext.Current.Session["AllWorkOrderByWONoData"] as List<Allworkorderbywono>;
            allWorkOrderByWONoR.SetDataSource(data);

            allWorkOrderByWONoR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            allWorkOrderByWONoR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = allWorkOrderByWONoR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["AllWorkOrderByWONoRDoc"] = allWorkOrderByWONoR;
        }
    }
}