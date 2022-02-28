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
    public partial class StoreWiseFGStatus : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
         {
                ShowReport();
            }
            else
            {
                StoreWiseFGStatusR report = (StoreWiseFGStatusR)Session["SearchstoreWiseFGStatusRDoc"];
                CrystalReportViewer1.ReportSource = report;

            }
        }
        public void ShowReport()
        {
            StoreWiseFGStatusR storeWiseFGStatusR = new StoreWiseFGStatusR();
                //StoreWiseFGStatusR storeWiseFGStatusR = new StoreWiseFGStatusR();

            storeWiseFGStatusR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\StoreWiseFGStatusR.rpt");
            storeWiseFGStatusR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SearchstoreWiseFGStatusR"] as ReportParmPersister;
            List<FGStatusReport> data = HttpContext.Current.Session["storeWiseFGStatusRReportData"] as List<FGStatusReport>;
            storeWiseFGStatusR.SetDataSource(data);


            storeWiseFGStatusR.SetParameterValue("itemName", "");
            storeWiseFGStatusR.SetParameterValue("address", "");
            storeWiseFGStatusR.SetParameterValue("compName", "");
            storeWiseFGStatusR.SetParameterValue("AsOnDate", persister.AsOnDate.ToString("dd-MM-yyyy"));
            storeWiseFGStatusR.SetParameterValue("fDate", persister.FromDate.ToShortDateString());
            storeWiseFGStatusR.SetParameterValue("tDate", persister.ToDate.ToShortDateString());

            CrystalReportViewer1.ReportSource = storeWiseFGStatusR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SearchstoreWiseFGStatusRDoc"] = storeWiseFGStatusR;
           
        }
    }
}