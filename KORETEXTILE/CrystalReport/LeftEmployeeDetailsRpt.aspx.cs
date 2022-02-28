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
    public partial class LeftEmployeeDetailsRpt : System.Web.UI.Page
    {
        //protected void Page_Init(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        ShowReport();
        //    }
        //    else
        //    {
        //        LeftEmployeeReportR report = (LeftEmployeeReportR)Session["leftEmpReportRDoc"];
        //        CrystalReportViewer1.ReportSource = report;
        //    }

        //}
        //public void ShowReport()
        //{
        //    EmployeeLeaveReportR leftemp = new EmployeeLeaveReportR();

        //    leftemp.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
        //    String APPPATH = Server.MapPath(@"\CrystalReport\LeftEmployeeReportR.rpt");
        //    leftemp.Load(APPPATH);
        //    ReportParmPersister persister = HttpContext.Current.Session["EmployeeLeftDetailsParam"] as ReportParmPersister;
        //    //List<LeftEmployeeDetailsReport> data = HttpContext.Current.Session["LeftEmployeeDetailsData"] as List<LeftEmployeeDetailsReport>;
        //    List<BEEERP.CrystalReport.ReportFormat.LeftEmployeeDetailsReport> data = HttpContext.Current.Session["hello"] as List<BEEERP.CrystalReport.ReportFormat.LeftEmployeeDetailsReport>;

        //    leftemp.SetDataSource(data);
        //    //leftemp.SetParameterValue("CompName", persister.CompName);
        //    //leftemp.SetParameterValue("CompAddress", persister.CompAddress);
        //    leftemp.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
        //    leftemp.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

        //    CrystalReportViewer1.ReportSource = leftemp;
        //    CrystalReportViewer1.EnableParameterPrompt = false;
        //    Session["leftEmpReportRDoc"] = leftemp;
        //}

        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                LeftEmployeeReportR report = (LeftEmployeeReportR)Session["leftEmpReportRDoc"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            LeftEmployeeReportR leftEmployeeReportR = new LeftEmployeeReportR();

            leftEmployeeReportR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\LeftEmployeeReportR.rpt");
            leftEmployeeReportR.Load(APPPATH);

            ReportParmPersister persister = HttpContext.Current.Session["EmployeeLeftDetailsParam"] as ReportParmPersister;
            List<BEEERP.CrystalReport.ReportFormat.AllJobQuitInformationReport> data = HttpContext.Current.Session["hello"] as List<BEEERP.CrystalReport.ReportFormat.AllJobQuitInformationReport>;
            leftEmployeeReportR.SetDataSource(data);


            //ePFBalanceSummaryR.SetParameterValue("address", "");
            //ePFBalanceSummaryR.SetParameterValue("compName", "");
            leftEmployeeReportR.SetParameterValue("fDate", persister.FromDate.ToString("dd-MM-yyyy"));
            leftEmployeeReportR.SetParameterValue("tDate", persister.ToDate.ToString("dd-MM-yyyy"));

            CrystalReportViewer1.ReportSource = leftEmployeeReportR;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["leftEmpReportRDoc"] = leftEmployeeReportR;
        }
    }
}