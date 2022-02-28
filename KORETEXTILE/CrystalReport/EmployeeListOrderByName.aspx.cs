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
    public partial class EmployeeListOrderByName : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                EmpListByName empListByName = (EmpListByName)Session["empListByNameReportDoc"];
                CrystalReportViewer1.ReportSource = empListByName;
            }
           
        }
        public void ShowReport()
        {
            EmpListByName empListByName = new EmpListByName();

            empListByName.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\EmpListByName.rpt");
            empListByName.Load(APPPATH);

            List<EmpListByNameReport> data = HttpContext.Current.Session["empListByNameReportData"] as List<EmpListByNameReport>;
            empListByName.SetDataSource(data);


            empListByName.SetParameterValue("address", "");
            empListByName.SetParameterValue("compName", "");

            CrystalReportViewer1.ReportSource = empListByName;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["empListByNameReportDoc"] = empListByName;
        }
    }
}