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
    public partial class DealerWiseSalesDetails : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                DealerWiseSalesDetailsR report = (DealerWiseSalesDetailsR)Session["DealerWiseSalesDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            DealerWiseSalesDetailsR salesDetails = new DealerWiseSalesDetailsR();
            salesDetails.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\DealerWiseSalesDetailsR.rpt");
            salesDetails.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SalesDetailsParam"] as ReportParmPersister;
            List<DealerWiseSalesDetailsReport> data = HttpContext.Current.Session["SalesDetailsData"] as List<DealerWiseSalesDetailsReport>;
            salesDetails.SetDataSource(data);

            //vExpense.SetParameterValue("accountName", persister.AccountName);
            salesDetails.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            salesDetails.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            //salesDetails.SetParameterValue("BranchName", persister.BranchName);

            CrystalReportViewer1.ReportSource = salesDetails;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["DealerWiseSalesDoc"] = salesDetails;
        }
    }
}