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
    public partial class CategoryWiseSalesAndCollectionRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                CategoryWiseSalesAndCollectionR report = (CategoryWiseSalesAndCollectionR)Session["CategoryWiseSalesAndCollectionDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            CategoryWiseSalesAndCollectionR categoryWiseSalesAndCollectionR = new CategoryWiseSalesAndCollectionR();

            categoryWiseSalesAndCollectionR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\CategoryWiseSalesAndCollectionR.rpt");
            categoryWiseSalesAndCollectionR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["CategoryWiseSalesAndCollectionParam"] as ReportParmPersister;
            List<CategoryWiseSalesAndCollectionReport> data = HttpContext.Current.Session["CategoryWiseSalesAndCollectionData"] as List<CategoryWiseSalesAndCollectionReport>;
            categoryWiseSalesAndCollectionR.SetDataSource(data);


            categoryWiseSalesAndCollectionR.SetParameterValue("address", persister.CompAddress);
            categoryWiseSalesAndCollectionR.SetParameterValue("compName", persister.CompName);
            categoryWiseSalesAndCollectionR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            categoryWiseSalesAndCollectionR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = categoryWiseSalesAndCollectionR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["CategoryWiseSalesAndCollectionDoc"] = categoryWiseSalesAndCollectionR;
        }
    }
}