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
    public partial class SingleBranchEmployeePayScaleRpt : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SingleBranchEmployeePayScaleR report = (SingleBranchEmployeePayScaleR)Session["SingleBranchEmployeePayScaleDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SingleBranchEmployeePayScaleR sbeps = new SingleBranchEmployeePayScaleR();

            sbeps.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\ReportRdlc\SingleBranchEmployeePayScaleR.rpt");
            sbeps.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["SingleBranchEmployeePayScaleparam"] as ReportParmPersister;
            List<SingleBranchEPSReport> data = HttpContext.Current.Session["SingleBranchEmployeePayScaleData"] as List<SingleBranchEPSReport>;
            sbeps.SetDataSource(data);

            //sbeps.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            //sbeps.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = sbeps;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SingleBranchEmployeePayScaleDoc"] = sbeps;
        }
    }
}