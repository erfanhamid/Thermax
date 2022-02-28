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
    public partial class TSOWiseCollectionDetailsRpt : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                TSOWiseCollectionDetailsR report = (TSOWiseCollectionDetailsR)Session["TSOWiseCollectionDetailsDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            TSOWiseCollectionDetailsR tsoWiseCollectionDetailsR = new TSOWiseCollectionDetailsR();

            tsoWiseCollectionDetailsR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\TSOWiseCollectionDetailsR.rpt");
            tsoWiseCollectionDetailsR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["TsoWiseCollectionDetails"] as ReportParmPersister;
            List<TSOWiseCollectionDetailsReport> data = HttpContext.Current.Session["TSOWiseCollectionDetailsReportData"] as List<TSOWiseCollectionDetailsReport>;
            tsoWiseCollectionDetailsR.SetDataSource(data);


            tsoWiseCollectionDetailsR.SetParameterValue("address", "");
            tsoWiseCollectionDetailsR.SetParameterValue("compName", "");
            tsoWiseCollectionDetailsR.SetParameterValue("tsoName", persister.TsoName);
            tsoWiseCollectionDetailsR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            tsoWiseCollectionDetailsR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));


            CrystalReportViewer1.ReportSource = tsoWiseCollectionDetailsR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["TSOWiseCollectionDetailsDoc"] = tsoWiseCollectionDetailsR;
        }
    }
}