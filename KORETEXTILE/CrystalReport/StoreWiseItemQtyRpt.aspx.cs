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
    public partial class StoreWiseItemQtyRpt : System.Web.UI.Page
    {
      
            public void Page_Init(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    ShowReport();
                }
                else
                {
                    StoreWiseItemQtyR report = (StoreWiseItemQtyR)Session["StoreWiseItemQtyDoc"];
                    CrystalReportViewer1.ReportSource = report;
                }
            }
            public void ShowReport()
            {
            StoreWiseItemQtyR store = new StoreWiseItemQtyR();
            store.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
                String APPPATH = Server.MapPath(@"\ReportRdlc\StoreWiseItemQtyR.rpt");
            store.Load(APPPATH);

                ReportParmPersister persister = HttpContext.Current.Session["storeParam"] as ReportParmPersister;
                List<StoreWiseItemQtyReport> data = HttpContext.Current.Session["storeData"] as List<StoreWiseItemQtyReport>;
            store.SetDataSource(data);


            store.SetParameterValue("asDate", persister.AsOnDate.ToString("dd-MM-yyyy"));
            store.SetParameterValue("AccountType", persister.RootAccountType);
            store.SetParameterValue("Store", persister.StoreName);

            CrystalReportViewer1.ReportSource = store;
                CrystalReportViewer1.EnableParameterPrompt = false;
                Session["StoreWiseItemQtyDoc"] = store;
            }
        
    }
}