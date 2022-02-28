using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.CommonInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BEEERP.CrystalReport
{
    public partial class EmployeeInfoDetails : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                EmpInfoDetails report = (EmpInfoDetails)Session["ReportDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            EmpInfoDetails empInfoDetails = new EmpInfoDetails();
            ReportParmPersister persister = HttpContext.Current.Session["empParam"] as ReportParmPersister;
            empInfoDetails.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\EmpInfoDetails.rpt");
            empInfoDetails.Load(APPPATH);

            List<EmpInfoReport> data = HttpContext.Current.Session["empInfoDetailsReportData"] as List<EmpInfoReport>;
            empInfoDetails.SetDataSource(data);


            empInfoDetails.SetParameterValue("address", "");
            empInfoDetails.SetParameterValue("compName", "");
            
            empInfoDetails.SetParameterValue("imageUrl", persister.ImageLink);
            
            CrystalReportViewer1.ReportSource = empInfoDetails;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["ReportDoc"] = empInfoDetails;
        }
    }
}