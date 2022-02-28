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
    public partial class SingleCustomerItemWiseSalesReturnRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleCustomerItemWiseSalesReturnR report = (SingleCustomerItemWiseSalesReturnR)Session["ReportDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SingleCustomerItemWiseSalesReturnR singleCustomerItemWiseSalesReturnR = new SingleCustomerItemWiseSalesReturnR();

            singleCustomerItemWiseSalesReturnR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SingleCustomerItemWiseSalesReturnR.rpt");
            singleCustomerItemWiseSalesReturnR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SingleCustomerItemWiseSalesReturn"] as ReportParmPersister;
            List<SingleCustomerItemWiseSalesReturnReport> data = HttpContext.Current.Session["SingleCustomerItemWiseSalesReturnReportData"] as List<SingleCustomerItemWiseSalesReturnReport>;
            singleCustomerItemWiseSalesReturnR.SetDataSource(data);

            singleCustomerItemWiseSalesReturnR.SetParameterValue("address", "");
            singleCustomerItemWiseSalesReturnR.SetParameterValue("compName", "");
            singleCustomerItemWiseSalesReturnR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            singleCustomerItemWiseSalesReturnR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            singleCustomerItemWiseSalesReturnR.SetParameterValue("custName", persister.CustName);

            CrystalReportViewer1.ReportSource = singleCustomerItemWiseSalesReturnR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ReportDoc"] = singleCustomerItemWiseSalesReturnR;
        }
    }
}