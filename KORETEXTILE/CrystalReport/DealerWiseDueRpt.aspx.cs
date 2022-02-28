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
    public partial class DealerWiseDueRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                DealerWiseDueR report = (DealerWiseDueR)Session["DealerWiseDueDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            DealerWiseDueR due = new DealerWiseDueR();
            due.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\DealerWiseDueR.rpt");
            due.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["DueParam"] as ReportParmPersister;
            List<DealerWiseDueReport> data = HttpContext.Current.Session["DueData"] as List<DealerWiseDueReport>;
            due.SetDataSource(data);

            //vExpense.SetParameterValue("accountName", persister.AccountName);
            due.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            due.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));
            //salesDetails.SetParameterValue("BranchName", persister.BranchName);

            CrystalReportViewer1.ReportSource = due;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["DealerWiseDueDoc"] = due;
        }
    }
}