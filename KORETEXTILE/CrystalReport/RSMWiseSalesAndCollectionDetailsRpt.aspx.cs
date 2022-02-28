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
    public partial class RSMWiseSalesAndCollectionDetailsRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                RSMWiseSalesAndCollectionDetailsR report = (RSMWiseSalesAndCollectionDetailsR)Session["RSMWiseSalesAndCollectionDetailsDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            RSMWiseSalesAndCollectionDetailsR rSMWiseSalesAndCollectionDetailsR = new RSMWiseSalesAndCollectionDetailsR();
            rSMWiseSalesAndCollectionDetailsR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\RSMWiseSalesAndCollectionDetailsR.rpt");
            rSMWiseSalesAndCollectionDetailsR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["RSMWiseSalesAndCollectionDetailsParam"] as ReportParmPersister;
            List<RSMWiseSalesAndCollectionDetailsReport> data = HttpContext.Current.Session["RSMWiseSalesAndCollectionDetailsData"] as List<RSMWiseSalesAndCollectionDetailsReport>;
            rSMWiseSalesAndCollectionDetailsR.SetDataSource(data);

            rSMWiseSalesAndCollectionDetailsR.SetParameterValue("address", persister.CompAddress);
            rSMWiseSalesAndCollectionDetailsR.SetParameterValue("compName", persister.CompName);
            //rSMWiseSalesAndCollectionDetailsR.SetParameterValue("rsmName", persister.RSMName);
            rSMWiseSalesAndCollectionDetailsR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            rSMWiseSalesAndCollectionDetailsR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = rSMWiseSalesAndCollectionDetailsR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["RSMWiseSalesAndCollectionDetailsDoc"] = rSMWiseSalesAndCollectionDetailsR;
        }
    }
}