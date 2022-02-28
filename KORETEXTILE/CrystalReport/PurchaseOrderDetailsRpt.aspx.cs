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
    public partial class PurchaseOrderDetailsRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                PurchaseOrderDetailsR report = (PurchaseOrderDetailsR)Session["PurchaseOrderDetails"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            PurchaseOrderDetailsR purchaseOrder = new PurchaseOrderDetailsR();
            purchaseOrder.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\PurchaseOrderDetailsR.rpt");
            purchaseOrder.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["PurchaseOrderDetailsParam"] as ReportParmPersister;
            List<PurchaseOrderDetails> data = HttpContext.Current.Session["PurchaseOrderDetailsData"] as List<PurchaseOrderDetails>;
            purchaseOrder.SetDataSource(data);
            
            purchaseOrder.SetParameterValue("SupplierName", persister.SupplierName);
            purchaseOrder.SetParameterValue("DateDuration", persister.Description);
            purchaseOrder.SetParameterValue("address", persister.CompAddress);
            purchaseOrder.SetParameterValue("compName", persister.CompName);
            CrystalReportViewer1.ReportSource = purchaseOrder;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["PurchaseOrderDetails"] = purchaseOrder;
        }
    }
}