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
    public partial class SingleCustomerSalesAndCollection : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleCustomerSalesAndCollectionR report = (SingleCustomerSalesAndCollectionR)Session["SingleCustomerSalesAndCollectionR"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SingleCustomerSalesAndCollectionR singleCustomerSalesAndCollection = new SingleCustomerSalesAndCollectionR();

            singleCustomerSalesAndCollection.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SingleCustomerSalesAndCollectionR.rpt");
            singleCustomerSalesAndCollection.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SingleCustomerSalesAndCollectionParam"] as ReportParmPersister;
            List<TsoWiseSalesAndCollectionReport> data = HttpContext.Current.Session["SingleCustomerSalesAndCollectionData"] as List<TsoWiseSalesAndCollectionReport>;
            singleCustomerSalesAndCollection.SetDataSource(data);

            singleCustomerSalesAndCollection.SetParameterValue("address", persister.CompName);
            singleCustomerSalesAndCollection.SetParameterValue("compName", persister.CompAddress);
            singleCustomerSalesAndCollection.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            singleCustomerSalesAndCollection.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            singleCustomerSalesAndCollection.SetParameterValue("custName", persister.CustName);
            singleCustomerSalesAndCollection.SetParameterValue("custId", persister.CustomerId);

            CrystalReportViewer1.ReportSource = singleCustomerSalesAndCollection;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SingleCustomerSalesAndCollectionR"] = singleCustomerSalesAndCollection;
        }
    }
}