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
    public partial class AllRMStockAdjustmentRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                AllRMStockAdjustmentR report = (AllRMStockAdjustmentR)Session["AllRMStockAdjustmentDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            AllRMStockAdjustmentR allRMStockAdjustmentR = new AllRMStockAdjustmentR();
            allRMStockAdjustmentR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\AllRMStockAdjustmentR.rpt");
            allRMStockAdjustmentR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["AllRMStockAdjustmentParam"] as ReportParmPersister;
            List<AllRMStockAdjustmentReport> data = HttpContext.Current.Session["AllRMStockAdjustmentData"] as List<AllRMStockAdjustmentReport>;
            allRMStockAdjustmentR.SetDataSource(data);

            //allRMStockAdjustmentR.SetParameterValue("SupplierGroup", persister.SupplierGroup);
            allRMStockAdjustmentR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            allRMStockAdjustmentR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));



            CrystalReportViewer1.ReportSource = allRMStockAdjustmentR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["AllRMStockAdjustmentDoc"] = allRMStockAdjustmentR;
        }
    }
}