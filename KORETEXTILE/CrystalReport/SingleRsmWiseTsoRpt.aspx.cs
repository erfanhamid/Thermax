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
    public partial class SingleRsmWiseTsoRpt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleRsmWiseTsoR report = (SingleRsmWiseTsoR)Session["SingleRsmWiseTsoDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SingleRsmWiseTsoR singleRsmWiseTsoR = new SingleRsmWiseTsoR();

            singleRsmWiseTsoR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\SingleRsmWiseTsoR.rpt");
            singleRsmWiseTsoR.Load(APPPATH);
            List<SingleRsmWiseTsoReport> data = HttpContext.Current.Session["SingleRsmWiseTsoData"] as List<SingleRsmWiseTsoReport>;
            singleRsmWiseTsoR.SetDataSource(data);
            ReportParmPersister persister = new ReportParmPersister();
            singleRsmWiseTsoR.SetParameterValue("address",persister.CompAddress);
            singleRsmWiseTsoR.SetParameterValue("compName", persister.CompName);
            CrystalReportViewer1.ReportSource = singleRsmWiseTsoR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SingleRsmWiseTsoDoc"] = singleRsmWiseTsoR;
        }
    }
}