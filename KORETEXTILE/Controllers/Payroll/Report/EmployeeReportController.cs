using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.ViewModel.Sales.Report;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Payroll.Report
{
    [ShowNotification]
   // [Authorize(Roles = "HraAdmin,HraOperator,HraViewer,HraApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    public class EmployeeReportController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: EmployeeReport
        public ActionResult EmployeeInfoReport()
        {
            ViewBag.Employee = LoadComboBox.LoadAllEmployee();
            return View();
        }

        public ActionResult GetAllEmployeeInfo(int EmployeeId)
        {
           // Session["ReportName"] = "RePaymentSchedule";

            ReportParmPersister param = new ReportParmPersister();



            Session["RePaymentScheduleParam"] = param;

            string sql = string.Format("exec EmployeeInfo '" + EmployeeId + "'");
            var items = context.Database.SqlQuery<EmpInfoReport>(sql).ToList();
            if (items.Count == 0)
            {
                EmpInfoReport report = new EmpInfoReport();
                items.Add(report);
            }
            //var trustee = items.FirstOrDefault().Trustee;

            EmpInfoDetails rd = ShowReport( items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");
        }

        public EmpInfoDetails ShowReport( List<EmpInfoReport> data)
        {
            EmpInfoDetails saR = new EmpInfoDetails();

            saR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\EmpInfoDetails.rpt");
            saR.Load(APPPATH);
            saR.SetDataSource(data);
           int t = data.FirstOrDefault().EmployeeNo;
            var Url = context.EmployeeImageAndAttachment.FirstOrDefault(x => x.EmployeeId == t);
            if (Url != null)
            {
                var fullUrl = ControllerContext.HttpContext.Server.MapPath(@"~/Image/Employees/" + Url.EmployeeImageName + ".png");
                saR.SetParameterValue("imageUrl", fullUrl);
                saR.SetParameterValue("compName", "");
                saR.SetParameterValue("Address", "");

            }
            else
            {
                saR.SetParameterValue("imageUrl", "");
                saR.SetParameterValue("compName", "");
                saR.SetParameterValue("Address", "");
            }
            return saR;
        }

        //public ActionResult GetAllEmployeeInfo(int EmployeeId)
        //{
        //    ReportParmPersister param = new ReportParmPersister();
        //    Session["ReportName"] = "EmpInfoDetails";
        //    //var Url = context.TrusteeBoard.FirstOrDefault(x => x.TrusteeName == t).ImageUrl;
        //    //if (Url != null)
        //    //{

        //    //    saR.SetParameterValue("imageUrl", fullUrl);
        //    //}
           
        //    var image = context.EmployeeImageAndAttachment.FirstOrDefault(x => x.EmployeeId == EmployeeId);
        //    if(image != null)
        //    {
        //        var fullUrl = ControllerContext.HttpContext.Server.MapPath(@"~/Image/Employees/" + image.EmployeeImageName + ".png");
        //        param.ImageLink  = fullUrl;
        //    }
        //    else
        //    {
        //        param.ImageLink = "";
        //    }
           
        //    string sql = string.Format("exec EmployeeInfo '"+EmployeeId+"'");
        //    var items = context.Database.SqlQuery<EmpInfoReport>(sql).ToList();
        //    if (items.Count == 0)
        //    {
        //        EmpInfoReport report = new EmpInfoReport();
        //        items.Add(report);
        //    }
        //    Session["empInfoDetailsReportData"] = items;
        //    Session["empParam"] = param;
        //    return Redirect("/CrystalReport/EmployeeInfoDetails.aspx");
        //}

        public ActionResult GetEmployeeListOrderByName()
        {

            Session["ReportName"] = "EmpListByName";

            string sql = string.Format("exec EmployeeList");
            var items = context.Database.SqlQuery<EmpListByNameReport>(sql).ToList();
            if (items.Count == 0)
            {
                EmpListByNameReport report = new EmpListByNameReport();
                items.Add(report);
            }
            Session["empListByNameReportData"] = items;
            return Redirect("/CrystalReport/EmployeeListOrderByName.aspx");
        }

        public ActionResult GetEmployeePFBalenceSummary(DateTime AsOnDate)
        {

            Session["ReportName"] = "EPFBalanceSummaryR";
            ReportParmPersister param = new ReportParmPersister();
            param.AsOnDate = AsOnDate;
            Session["EPFBalanceSummaryParam"] = param;
            string sql = string.Format("exec spEmployeePFBalanceSummary '" + DateTimeFormat.ConvertToDDMMYYYY(AsOnDate) + "' ");
            var items = context.Database.SqlQuery<EmployeePFBalanceSummary>(sql).ToList();
            if (items.Count == 0)
            {
                EmployeePFBalanceSummary report = new EmployeePFBalanceSummary();
                items.Add(report);
            }
            Session["hello"] = items;
            return Redirect("/CrystalReport/EmployeePFBalanceSummaryRpt.aspx");
        }

        public ActionResult GetEmployeeITBalenceSummary(DateTime AsOnDate)
        {

            Session["ReportName"] = "EITBalanceSummaryR";
            ReportParmPersister param = new ReportParmPersister();
            param.AsOnDate = AsOnDate;
            Session["EITBalanceSummaryParam"] = param;
            string sql = string.Format("exec spEmployeeITBalanceSummary '" + DateTimeFormat.ConvertToDDMMYYYY(AsOnDate) + "' ");
            var items = context.Database.SqlQuery<EmployeeITBalanceSummary>(sql).ToList();
            if (items.Count == 0)
            {
                EmployeeITBalanceSummary report = new EmployeeITBalanceSummary();
                items.Add(report);
            }
            Session["hello"] = items;
            return Redirect("/CrystalReport/EmployeeITBalanceSummaryRpt.aspx");
        }

        public ActionResult GetEmployeeMCBalenceSummary(DateTime AsOnDate)
        {

            Session["ReportName"] = "EMCBalanceSummaryR";
            ReportParmPersister param = new ReportParmPersister();
            param.AsOnDate = AsOnDate;
            Session["EMCBalanceSummaryParam"] = param;
            string sql = string.Format("exec spEmployeeMCBalanceSummary '" + DateTimeFormat.ConvertToDDMMYYYY(AsOnDate) + "' ");
            var items = context.Database.SqlQuery<EmployeeMCBalanceSummary>(sql).ToList();
            if (items.Count == 0)
            {
                EmployeeMCBalanceSummary report = new EmployeeMCBalanceSummary();
                items.Add(report);
            }
            Session["hello"] = items;
            return Redirect("/CrystalReport/EmployeeMCBalanceSummaryRpt.aspx");
        }

        public ActionResult GetEmployeeADBalenceSummary(DateTime AsOnDate)
        {

            Session["ReportName"] = "EADBalanceSummaryR";
            ReportParmPersister param = new ReportParmPersister();
            param.AsOnDate = AsOnDate;
            Session["EADBalanceSummaryParam"] = param;
            string sql = string.Format("exec spEmployeeADBalanceSummary '" + DateTimeFormat.ConvertToDDMMYYYY(AsOnDate) + "' ");
            var items = context.Database.SqlQuery<EmployeeADBalanceSummary>(sql).ToList();
            if (items.Count == 0)
            {
                EmployeeADBalanceSummary report = new EmployeeADBalanceSummary();
                items.Add(report);
            }
            Session["hello"] = items;
            return Redirect("/CrystalReport/EmployeeADBalanceSummaryRpt.aspx");
        }

        public ActionResult GetEmployeeBalanceSummary( DateTime AsOnDate, int EmployeeId=0 )
        {

            Session["ReportName"] = "EmpAccBalanceSummary";

            ReportParmPersister param = new ReportParmPersister();
            param.AsOnDate = AsOnDate;
            Session["EmployeeBalanceSummaryDate"] = param;


            string sql = string.Format("exec EmployeeBalanceSummary '"+ DateTimeFormat.ConvertToDDMMYYYY(AsOnDate) + "', '"+ EmployeeId + "' ");
            var items = context.Database.SqlQuery<EmpBalanceSummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                EmpBalanceSummaryReport report = new EmpBalanceSummaryReport();
                items.Add(report);
            }
            Session["empBalanceSummaryReportData"] = items;
            return Redirect("/CrystalReport/EmpAccountBalanceSummary.aspx");
        }

        public ActionResult ShowEmployeeLeaveDetails(ReportVModel model)
        {
            Session["ReportName"] = "EmployeeLeaveDetailsR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["EmployeeLeaveDetailsParam"] = param;

            string sql = string.Format("exec EmployeeLeaveDetails '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "' ");
            var items = context.Database.SqlQuery<EmployeeLeaveDetailsReport>(sql).ToList();
            if (items.Count == 0)
            {
                EmployeeLeaveDetailsReport report = new EmployeeLeaveDetailsReport();
                items.Add(report);
            }
            Session["EmployeeLeaveDetailsData"] = items;
            return Redirect("/CrystalReport/EmployeeLeaveDetailsRpt.aspx");

        }

        public ActionResult GetLeftEmployeeList(ReportVModel model)
        {
            Session["ReportName"] = "EmployeeLeftDetailsR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["EmployeeLeftDetailsParam"] = param;

            string sql = string.Format("exec AllJobQuitInformation '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "' ");
            var items = context.Database.SqlQuery<AllJobQuitInformationReport>(sql).ToList();
            if (items.Count == 0)
            {
                AllJobQuitInformationReport report = new AllJobQuitInformationReport();
                items.Add(report);
            }
            Session["hello"] = items;
            return Redirect("/CrystalReport/LeftEmployeeDetailsRpt.aspx");

        }
        public ActionResult ShowEmployeeLedger(ReportVModel model)
        {

            Session["ReportName"] = "EmployeeBalanceCalculationDetailsR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            param.EmployeeID = model.EmployeeID;
            var Employee = context.Employees.FirstOrDefault(x => x.Id == model.EmployeeID);
            if (Employee != null)
            {
                param.EmployeeName = Employee.Name;
            }
            else
            {
                param.EmployeeName = "";
            }
            Session["EmployeeBalanceCalculationParam"] = param;

            string sql = string.Format("exec EmployeeBalanceCalculations '" + model.EmployeeID + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "' ");
            var items = context.Database.SqlQuery<EmployeeLedger>(sql).ToList();
            if (items.Count == 0)
            {
                EmployeeLedger report = new EmployeeLedger();
                items.Add(report);
            }
            Session["EmployeeBalanceCalculationData"] = items;
            return Redirect("/CrystalReport/EmployeeLedgerRpt.aspx");

        }
        public ActionResult ShowEmployeeOfficialHistory(ReportVModel model)
        {
            Session["ReportName"] = "EmployeeOfficialHistory";
            ReportParmPersister param = new ReportParmPersister();
            if (model.EmployeeID > 0)
            {
                param.EmployeeID = model.EmployeeID;
                param.EmployeeName = context.Employees.FirstOrDefault(x => x.Id == model.EmployeeID).Name;
            }
            else
            {
                param.EmployeeID = 0;
                param.EmployeeName = "";
            }
            param.Description = "From "+ DateTimeFormat.ConvertToDDMMYYYY(model.From) + " To "+ DateTimeFormat.ConvertToDDMMYYYY(model.To) + "";
            param.CompanyName = "Asian Petroleum Limited";
             Session["EmployeeOfficialHistoryParam"] = param;

            string sql = string.Format("exec EmployeeOfficialHistory '" + model.EmployeeID + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "' ");
            var items = context.Database.SqlQuery<EmployeeOfficialHistory>(sql).ToList();
            if (items.Count == 0)
            {
                EmployeeOfficialHistory report = new EmployeeOfficialHistory();
                items.Add(report);
            }
            Session["EmployeeOfficialHistoryData"] = items;
            return Redirect("/CrystalReport/EmployeeOfficialHistoryRpt.aspx");

        }
    }
}