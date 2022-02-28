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
    public partial class ItemWiseSingleDeS : System.Web.UI.Page 
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                ItemWiseSingleDepotSale report = (ItemWiseSingleDepotSale)Session["ReportDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            try
            {
                ItemWiseSingleDepotSale custLedger = new ItemWiseSingleDepotSale();


                custLedger.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);

                string repotPath = "";

                List<ItemWiseSingleDepotSalevModel> data = HttpContext.Current.Session["ItemWiseSingleDepotSaleList"] as List<ItemWiseSingleDepotSalevModel>;

                ReportParmPersister persister = HttpContext.Current.Session["ItemWiseSingleDepotSaleParam"] as ReportParmPersister;

                if (HttpContext.Current.Session["ItemWiseSingleDepotSaleRpt"] != null)
                {
                    repotPath = @"\ReportRdlc\" + HttpContext.Current.Session["ItemWiseSingleDepotSaleRpt"].ToString() + ".rpt";
                    custLedger.Load(Server.MapPath(repotPath));
                }

                custLedger.SetDataSource(data);

                custLedger.SetParameterValue("fDate", persister.FromDate.ToString("dd-MMM-yyyy"));
                custLedger.SetParameterValue("tDate", persister.ToDate.ToString("dd-MMM-yyyy"));
                custLedger.SetParameterValue("BranchName", persister.BranchName);



                CrystalReportViewer1.ReportSource = custLedger;
                CrystalReportViewer1.EnableParameterPrompt = false;
                Session["ReportDoc"] = custLedger;
            }
            catch
            {

            }

        }
    }
}