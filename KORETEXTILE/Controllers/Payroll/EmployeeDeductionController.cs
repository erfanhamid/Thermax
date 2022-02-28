using BEEERP.CrystalReport.ReportFormat;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using BEEERP.Models.Payroll;
using BEEERP.Models.ViewModel.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Payroll
{
    [ShowNotification]
    //[Authorize(Roles = "HraAdmin,HraOperator,HraViewer,HraApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    public class EmployeeDeductionController : Controller
    {
        // GET: EmployeeDeduction
        static List<SalaryDeductionVModel> list = null;
        BEEContext beeContext = new BEEContext();
        public ActionResult EmployeeDeduction()
        {
            string sql = string.Format("exec GetEmployeesForSalaryDeduction");
            ViewBag.list = beeContext.Database.SqlQuery<SalaryDeductionVModel>(sql).ToList();
            return View();
        }
        public ActionResult GetEmployeeSalaryDeductions(int EmployeeID)
        {
            if (EmployeeID == 0)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var employeeSalaryDeduction = beeContext.EmployeeSalaryDeductions.Where(m => m.EmployeeID == EmployeeID).ToList();
                if (employeeSalaryDeduction.Count() != 0)
                {
                    var employeeSalaryDeductions = beeContext.EmployeeSalaryDeductions.FirstOrDefault(m => m.EmployeeID == EmployeeID);
                    employeeSalaryDeductions.EmployeeName = beeContext.Employees.FirstOrDefault(x => x.Id == EmployeeID).Name;
                    var designationid = beeContext.Employees.FirstOrDefault(x => x.Id == EmployeeID).Designation;
                    employeeSalaryDeductions.Designation = beeContext.Designation.FirstOrDefault(x => x.Id == designationid).Name;
                    return Json(new { employeesalarydeduction = employeeSalaryDeductions }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    EmployeeSalaryDeductions employeeSalaryDeductions = new EmployeeSalaryDeductions();
                    employeeSalaryDeductions.EmployeeID = EmployeeID;
                    employeeSalaryDeductions.EmployeeName = beeContext.Employees.FirstOrDefault(x => x.Id == EmployeeID).Name;
                    var designationid = beeContext.Employees.FirstOrDefault(x => x.Id == EmployeeID).Designation;
                    employeeSalaryDeductions.Designation = beeContext.Designation.FirstOrDefault(x => x.Id == designationid).Name;
                    //employeeSalaryDeductions.Designation = (from d in beeContext.Designation
                    //                                        join e in beeContext.Employees on d.Id equals e.Designation
                    //                                        where e.Id == EmployeeID
                    //                                        select d.Name).ToString();
                    employeeSalaryDeductions.IncomeTax = 0;
                    employeeSalaryDeductions.ProvidentFund = 0;
                    employeeSalaryDeductions.MCAdjustment = 0;
                    employeeSalaryDeductions.AdvanceAdjustment = 0;
                    employeeSalaryDeductions.OtherAdjustment = 0;
                    return Json(new { employeesalarydeduction = employeeSalaryDeductions }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        [HttpGet]
        public JsonResult GetAllEmployeeInfo()
        {
            string sql = string.Format("exec GetEmployeesForSalaryDeduction");
            list = beeContext.Database.SqlQuery<SalaryDeductionVModel>(sql).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveEmployeeDeduction(EmployeeSalaryDeductions employeeSalaryDeductions)
        {
            if (employeeSalaryDeductions != null)
            {
                var isexists = beeContext.EmployeeSalaryDeductions.FirstOrDefault(x => x.EmployeeID == employeeSalaryDeductions.EmployeeID);
                if (isexists != null)
                {
                    beeContext.EmployeeSalaryDeductions.Remove(isexists);
                    beeContext.EmployeeSalaryDeductions.Add(employeeSalaryDeductions);
                    beeContext.SaveChanges();
                    //return Json(new { id = EmployeeSalaryDeductions.EmployeeID }, JsonRequestBehavior.AllowGet);
                    return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    beeContext.EmployeeSalaryDeductions.Add(employeeSalaryDeductions);
                    beeContext.SaveChanges();
                    //return Json(new { id = EmployeeSalaryDeductions.EmployeeID }, JsonRequestBehavior.AllowGet);
                    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }
                

        }
        public ActionResult DeleteEmployeeSalarayDeductionById(int EmployeeID)
        {
            var delete = beeContext.EmployeeSalaryDeductions.FirstOrDefault(x => x.EmployeeID == EmployeeID);

            if (delete == null)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                beeContext.EmployeeSalaryDeductions.Remove(delete);
                beeContext.SaveChanges();
                return Json(new { id = EmployeeID }, JsonRequestBehavior.AllowGet);
            }
        }

        //[HttpPost]
        public ActionResult ShowSalaryDeductionReport(SalaryDeductionVModel model)
        {
            Session["ReportName"] = "SalaryDeductionSummaryR";
            ReportParmPersister param = new ReportParmPersister();
            //param.FromDate = model.From;
            //param.ToDate = model.To;

            Session["SalaryDeductionSummaryParam"] = param;

            string sql = string.Format("exec spEmployeesForSalaryDeductionSummary");
            var items = beeContext.Database.SqlQuery<SalarydeductionReport>(sql).ToList();
            if (items.Count == 0)
            {
                SalarydeductionReport report = new SalarydeductionReport();
                items.Add(report);
            }
            Session["SalaryDeductionSummaryReportData"] = items;
            return Redirect("/CrystalReport/SalaryDeductionSummaryReportRpt.aspx");

        }
    }
}