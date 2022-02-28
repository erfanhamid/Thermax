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
    public partial class TsoWiseSampleQuantityRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                TsoWiseSampleQuantityR report = (TsoWiseSampleQuantityR)Session["TsoWiseSampleQuantityDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            TsoWiseSampleQuantityR tsoWiseSampleQuantityR = new TsoWiseSampleQuantityR();

            tsoWiseSampleQuantityR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\TsoWiseSampleQuantityR.rpt");
            tsoWiseSampleQuantityR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["TsoWiseSampleQuantityParam"] as ReportParmPersister;
            List<TsoWiseSampleQuantityReport> data = HttpContext.Current.Session["TsoWiseSampleQuantityData"] as List<TsoWiseSampleQuantityReport>;
            tsoWiseSampleQuantityR.SetDataSource(data);

            tsoWiseSampleQuantityR.SetParameterValue("address", persister.CompAddress);
            tsoWiseSampleQuantityR.SetParameterValue("compName", persister.CompName);
            tsoWiseSampleQuantityR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            tsoWiseSampleQuantityR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = tsoWiseSampleQuantityR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["TsoWiseSampleQuantityDoc"] = tsoWiseSampleQuantityR;
        }
    }
}