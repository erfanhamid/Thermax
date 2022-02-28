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
    public partial class SalaryProcessVoucherReportRpt : System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowReport();
            }
            else
            {
                SalaryProcessVoucherR report = (SalaryProcessVoucherR)Session["SalaryProcessVoucherReport"];
                CrystalReportViewer1.ReportSource = report;
            }
        }
        public void ShowReport()
        {
            SalaryProcessVoucherR salaryProcessVoucher = new SalaryProcessVoucherR();

            salaryProcessVoucher.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SalaryProcessVoucherReportRpt.rpt");
            salaryProcessVoucher.Load(APPPATH);

            List<SalaryProcessVoucherReport> data = HttpContext.Current.Session["SalaryProcessVoucherData"] as List<SalaryProcessVoucherReport>;

            salaryProcessVoucher.SetDataSource(data);
            ReportParmPersister persister = HttpContext.Current.Session["SalaryProcessVoucherParam"] as ReportParmPersister;

            salaryProcessVoucher.SetParameterValue("CompanyName", persister.CompName);
            salaryProcessVoucher.SetParameterValue("MonthYear", persister.Month);
            salaryProcessVoucher.SetParameterValue("CompanyGroup", persister.CompanyName);
            salaryProcessVoucher.SetParameterValue("address", persister.CompAddress);

            CrystalReportViewer1.ReportSource = salaryProcessVoucher;
            CrystalReportViewer1.EnableParameterPrompt = false;
            Session["SalaryProcessVoucherReport"] = salaryProcessVoucher;
        }
    }
}