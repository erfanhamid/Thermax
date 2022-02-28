using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Payroll
{
    [ShowNotification]
    //[Authorize(Roles = "HraAdmin,HraOperator,HraViewer,HraApprover,SysAdmin,SysViewer,SysOperator,SysApprover,DataAdmin,DataOperator,DataViewer,DataApprover")]
    public class EmployeeProfileController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: EmployeeProfile
        public ActionResult EmployeeProfile(int Id)
        {
            if (Id!=0)
            {
                var employee = context.Employees.FirstOrDefault(m => m.Id == Id);
                //if (employee.MarstausID != 0)
                //{
                //    employee.MaritalStatus = context.MarriageStatuse.FirstOrDefault(m => m.slno == employee.MarstausID).mstatus;
                //}
                //else
                //{
                //    employee.MaritalStatus = "";
                //}
                //if (employee.ReligionID != 0)
                //{
                //    employee.ReligionName = context.Religion.FirstOrDefault(m => m.sln == employee.ReligionID).ReligionName;
                //}
                //else
                //{
                //    employee.ReligionName = "";
                //}
                //if (employee.Sex != 0)
                //{
                //    employee.GenderName = context.Gender.FirstOrDefault(m => m.Id == employee.Sex).Name;
                //}
                //else
                //{
                //    employee.GenderName = "";
                //}
                if (employee.DepatmentID != 0)
                {
                    employee.Department = context.Department.FirstOrDefault(m => m.DeptmentID == employee.DepatmentID).DeprtmntName;
                }
                else
                {
                    employee.Department = "";
                }
                if (employee.CompanyId != 0)
                {
                    employee.CompanyName = context.CompanyInfo.FirstOrDefault(m => m.Id == employee.CompanyId).CompanyName;
                }
                else
                {
                    employee.CompanyName = "";
                }
                if (employee.BranchID != 0)
                {
                    employee.WorkStation = context.BranchInformation.FirstOrDefault(m => m.Slno == employee.BranchID).BrnachName;
                }
                else
                {
                    employee.WorkStation = "";
                }
                if (employee.Section != 0)
                {
                    employee.SectionName = context.EmployeeSection.FirstOrDefault(m => m.ID == employee.Section).CGroup;
                }
                else
                {
                    employee.SectionName = "";
                }
                if (employee.Designation != 0)
                {
                    employee.DesignationName = context.Designation.FirstOrDefault(m => m.Id == employee.Designation).Name;
                }
                else
                {
                    employee.DesignationName = "";
                }
                if (employee.HighestEducation != 0)
                {
                    employee.HigherEdu = context.HighestEducation.FirstOrDefault(m => m.ID == employee.HighestEducation).Education;
                }
                else
                {
                    employee.HigherEdu = "";
                }
                return View(employee);
            }
            return View();
        }
    }
}