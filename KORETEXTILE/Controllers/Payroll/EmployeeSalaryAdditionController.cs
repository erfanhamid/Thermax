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
    public class EmployeeSalaryAdditionController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: EmployeeSalaryAddition
        public ActionResult EmployeeSalaryAddition()
        {
            ViewBag.Depot = LoadComboBox.LoadAllDepot();
            var empSalaryList = new List<EmployeeSalaryAddition>();
            empSalaryList = context.EmployeeSalaryAdditions.ToList();
            var empSalaryListWithNmae = new List<EmployeeSalaryAdditionVModel>();
            empSalaryList.ForEach(d => {
                EmployeeSalaryAdditionVModel employeeSalaryAdditionVModel = new EmployeeSalaryAdditionVModel();
                employeeSalaryAdditionVModel.EmployeeId = d.EmployeeId;
                if(employeeSalaryAdditionVModel.EmployeeName!=null)
                {
                    employeeSalaryAdditionVModel.EmployeeName = context.Employees.FirstOrDefault(m => m.Id == d.EmployeeId).Name;
                }
                else
                {
                    employeeSalaryAdditionVModel.EmployeeName = "";
                }
                employeeSalaryAdditionVModel.Basic = d.Basic;
                employeeSalaryAdditionVModel.MA = d.MA;
                employeeSalaryAdditionVModel.CA = d.CA;
                employeeSalaryAdditionVModel.HRA = d.HRA;
                employeeSalaryAdditionVModel.Gross = d.Gross;
                employeeSalaryAdditionVModel.OtherAdj = d.OtherAdj;
                //employeeSalaryAdditionVModel.TA = d.TA;
                empSalaryListWithNmae.Add(employeeSalaryAdditionVModel);
            });
            ViewBag.employeeSalarys = empSalaryListWithNmae;
            return View();
        }
        [HttpGet]
        public JsonResult GetAllEmployeeInfo()
        {
            string sql = string.Format("select ISNULL(Employee.id,0.0) as EmployeeId,Employee.Name as EmployeeName,ISNULL(ESA.Basic,0.0) as Basic ,ISNULL(ESA.CA,0.0) as CA, ISNULL(ESA.HRA,0.0) as HRA,ISNULL(ESA.MA,0.0) as MA,ISNULL(ESA.Gross,0.0) as Gross,ISNULL(ESA.OtherAdj,0.0) as OtherAdj from EmployeeSalaryAdditions ESA FULL OUTER JOIN Employee ON Employee.id=ESA.EmployeeId");
            var result = context.Database.SqlQuery<EmployeeSalaryAdditionForSpTableVModel>(sql).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetEmployeeById(int empId)
        {
            var employee = context.Employees.FirstOrDefault(m=>m.Id== empId);
            if (employee != null)
            {
                var empDesignation = context.Designation.FirstOrDefault(m=>m.Id==employee.Designation).Name;
                var employeeName = context.Employees.FirstOrDefault(x => x.Id == employee.Id).Name;
                var branchName = context.BranchInformation.FirstOrDefault(m => m.Slno == employee.BranchID).BrnachName;
                //var empGrade = context.Grade.FirstOrDefault(x => x.GradeID == employee.EmpGrade).GradeName;
                //var empGrade = context.Grade.FirstOrDefault(m => m.GradeID == employee.EmpGrade).GradeName;
                //var EmpGrade = Convert.ToInt32(employee.EmpGrade);
                //employee.EmpGrade = context.Grade.FirstOrDefault(x => x.GradeID == EmpGrade).GradeName;
                return Json(new {  BranchName= branchName, EmpDesignation = empDesignation, EmpName= employeeName }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveSalaryAddition(EmployeeSalaryAddition employeeSalaryAddition)
        {
            if (employeeSalaryAddition!=null)
            {
                var preEmpSalary = context.EmployeeSalaryAdditions.FirstOrDefault(m => m.EmployeeId == employeeSalaryAddition.EmployeeId);
                if (preEmpSalary == null)
                {
                    EmployeeSalaryAdditionHistory employeeSalaryAdditionHistory = new EmployeeSalaryAdditionHistory();
                    employeeSalaryAdditionHistory.EmployeeId = employeeSalaryAddition.EmployeeId;
                    employeeSalaryAdditionHistory.Basic = employeeSalaryAddition.Basic;
                    employeeSalaryAdditionHistory.HRA = employeeSalaryAddition.HRA;
                    employeeSalaryAdditionHistory.MA = employeeSalaryAddition.MA;
                    employeeSalaryAdditionHistory.CA = employeeSalaryAddition.CA;
                    employeeSalaryAdditionHistory.Gross = employeeSalaryAddition.Gross;
                    employeeSalaryAdditionHistory.OtherAdj = employeeSalaryAddition.OtherAdj;
                    employeeSalaryAdditionHistory.Date = DateTime.Now;
                    context.EmployeeSalaryAdditionHistories.Add(employeeSalaryAdditionHistory);
                    context.EmployeeSalaryAdditions.Add(employeeSalaryAddition);
                    UserLog.SaveUserLog(ref context, new UserLog(employeeSalaryAddition.EmployeeId.ToString(), DateTime.Now, "Payroll", ScreenAction.Save));
                    context.SaveChanges();
                    return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    EmployeeSalaryAdditionHistory employeeSalaryAdditionHistory = new EmployeeSalaryAdditionHistory();
                    employeeSalaryAdditionHistory.EmployeeId = employeeSalaryAddition.EmployeeId;
                    employeeSalaryAdditionHistory.Basic = employeeSalaryAddition.Basic;
                    employeeSalaryAdditionHistory.HRA = employeeSalaryAddition.HRA;
                    employeeSalaryAdditionHistory.MA = employeeSalaryAddition.MA;
                    employeeSalaryAdditionHistory.CA = employeeSalaryAddition.CA;
                    employeeSalaryAdditionHistory.Gross = employeeSalaryAddition.Gross;
                    employeeSalaryAdditionHistory.OtherAdj = employeeSalaryAddition.OtherAdj;
                    employeeSalaryAdditionHistory.Date = DateTime.Now;
                    context.EmployeeSalaryAdditionHistories.Add(employeeSalaryAdditionHistory);
                    context.EmployeeSalaryAdditions.Remove(preEmpSalary);
                    context.EmployeeSalaryAdditions.Add(employeeSalaryAddition);
                    UserLog.SaveUserLog(ref context, new UserLog(employeeSalaryAddition.EmployeeId.ToString(), DateTime.Now, "Payroll", ScreenAction.Update));
                    context.SaveChanges();
                    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }
            
        }
        public ActionResult UpdateSalaryAddition(EmployeeSalaryAddition employeeSalaryAddition)
        {
            if (employeeSalaryAddition != null)
            {
                var preEmpSalary = context.EmployeeSalaryAdditions.FirstOrDefault(m => m.EmployeeId == employeeSalaryAddition.EmployeeId);
                if (preEmpSalary != null)
                {
                    EmployeeSalaryAdditionHistory employeeSalaryAdditionHistory = new EmployeeSalaryAdditionHistory();
                    employeeSalaryAdditionHistory.EmployeeId = employeeSalaryAddition.EmployeeId;
                    employeeSalaryAdditionHistory.Basic = employeeSalaryAddition.Basic;
                    employeeSalaryAdditionHistory.HRA = employeeSalaryAddition.HRA;
                    employeeSalaryAdditionHistory.MA = employeeSalaryAddition.MA;
                    employeeSalaryAdditionHistory.CA = employeeSalaryAddition.CA;
                    employeeSalaryAdditionHistory.Gross = employeeSalaryAddition.Gross;
                    employeeSalaryAdditionHistory.OtherAdj = employeeSalaryAddition.OtherAdj;
                    employeeSalaryAdditionHistory.Date = DateTime.Now;
                    context.EmployeeSalaryAdditionHistories.Add(employeeSalaryAdditionHistory);
                    context.EmployeeSalaryAdditions.Remove(preEmpSalary);
                    context.EmployeeSalaryAdditions.Add(employeeSalaryAddition);
                    UserLog.SaveUserLog(ref context, new UserLog(employeeSalaryAddition.EmployeeId.ToString(), DateTime.Now, "Payroll", ScreenAction.Update));
                    context.SaveChanges();
                    return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetOtherAmountByGross(int grossAmount,string gradeName)
       {
            if (grossAmount!=0)
            {
                var gradeid = context.Grade.FirstOrDefault(x => x.GradeName == gradeName).GradeID;
                var salaryStructure = context.SalaryStructure.FirstOrDefault(x=>x.GradeID== gradeid);
                if (salaryStructure != null) 
                {
                    var basic = ( grossAmount) *0.5;
                    var hra = (basic) *0.5;
                    var ma = salaryStructure.MedicalAllowance ;
                    var ca = salaryStructure.ConveyanceAllowance ;
                    var oa = (Convert.ToDecimal( grossAmount)) -(Convert.ToDecimal( basic) + Convert.ToDecimal(hra) + ma + ca);
                    return Json(new { Message = 1, basic = basic, hra = hra, ma = ma, ca = ca,oa=oa }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}