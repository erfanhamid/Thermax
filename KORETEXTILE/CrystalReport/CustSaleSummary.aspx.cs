using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.ViewModel.Sales.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BEEERP.CrystalReport
{
    public partial class CustSaleSummary : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                CustWiseSaleSumm report = (CustWiseSaleSumm)Session["ReportDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            CustWiseSaleSumm custSaleSum = new CustWiseSaleSumm();


            custSaleSum.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            string repotPath = "";

            List<CustWiseSaleSumVModel> data = HttpContext.Current.Session["CustWiseSaleSummList"] as List<CustWiseSaleSumVModel>;
            ReportParmPersister persister = HttpContext.Current.Session["CustWiseSaleSummParam"] as ReportParmPersister;

            if (HttpContext.Current.Session["CustWiseSaleSummRpt"] != null)
            {
                repotPath = @"\ReportRdlc\" + HttpContext.Current.Session["CustWiseSaleSummRpt"].ToString() + ".rpt";
                custSaleSum.Load(Server.MapPath(repotPath));
            }

            custSaleSum.SetDataSource(data);

            custSaleSum.SetParameterValue("fDate", persister.FromDate.ToString("dd-MMM-yyyy"));
            custSaleSum.SetParameterValue("tDate", persister.ToDate.ToString("dd-MMM-yyyy"));
            custSaleSum.SetParameterValue("BranchName", persister.BranchName);
            custSaleSum.SetParameterValue("@fromDate", persister.FromDate);
            custSaleSum.SetParameterValue("@toDate", persister.ToDate);
            custSaleSum.SetParameterValue("@branchId", persister.CustId);

            CrystalReportViewer1.ReportSource = custSaleSum;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ReportDoc"] = custSaleSum;
        }
    }
}