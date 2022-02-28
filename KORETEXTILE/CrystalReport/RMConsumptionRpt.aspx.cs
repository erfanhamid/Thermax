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
    public partial class RMConsumptionRpt : System.Web.UI.Page
    {

        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                RMConsumptionR report = (RMConsumptionR)Session["RMConsumtionDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            RMConsumptionR RMC = new RMConsumptionR();

            RMC.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\RMConsumptionR.rpt");
            RMC.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["RMConsumptionParam"] as ReportParmPersister;
            List<RMConsumptionReport> data = HttpContext.Current.Session["RMConsumptionData"] as List<RMConsumptionReport>;
            RMC.SetDataSource(data);

            RMC.SetParameterValue("compAddress", persister.CompAddress);
            RMC.SetParameterValue("compName", persister.CompName);
            RMC.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            RMC.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = RMC;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["RMConsumtionDoc"] = RMC;
        }
    }
}