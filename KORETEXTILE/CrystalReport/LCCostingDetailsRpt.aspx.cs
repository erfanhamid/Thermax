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
    public partial class LCCostingDetailsRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                LCCostingDetailsR report = (LCCostingDetailsR)Session["LCCostingDetailsDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            LCCostingDetailsR lCCostingDetailsR = new LCCostingDetailsR();
            lCCostingDetailsR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\LCCostingDetailsR.rpt");
            lCCostingDetailsR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["LCCostingDetailsParam"] as ReportParmPersister;
            List<LCCostingDetailsReport> data = HttpContext.Current.Session["LCCostingDetailsData"] as List<LCCostingDetailsReport>;
            lCCostingDetailsR.SetDataSource(data);

            lCCostingDetailsR.SetParameterValue("address", persister.CompAddress);
            lCCostingDetailsR.SetParameterValue("compName", persister.CompName);
            lCCostingDetailsR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            lCCostingDetailsR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            lCCostingDetailsR.SetParameterValue("ilcId", persister.ILCId);
            lCCostingDetailsR.SetParameterValue("ilcNo", persister.ILCNo);
            lCCostingDetailsR.SetParameterValue("ialcNo", persister.IALCNo);

            CrystalReportViewer1.ReportSource = lCCostingDetailsR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["LCCostingDetailsDoc"] = lCCostingDetailsR;
        }
    }
}