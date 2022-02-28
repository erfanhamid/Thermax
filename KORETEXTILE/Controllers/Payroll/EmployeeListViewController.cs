using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.HRModule;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Payroll
{
    [ShowNotification]
    //[Authorize(Roles = "HraAdmin,HraOperator,HraViewer,HraApprover,SysAdmin,SysViewer,SysOperator,SysApprover,DataAdmin,DataOperator,DataViewer,DataApprover")]
    public class EmployeeListViewController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: EmployeeListView
        public ActionResult EmployeeListView(int? page,string namelike)
        {
            if (namelike!=null)
            {
                List<Employee> employeeList = new List<Employee>();
                employeeList = context.Employees.OrderBy(m=>m.Id).ToList().Where(x => x.Name.ToLower().Contains(namelike.ToLower())).ToList();
                employeeList.ForEach(d =>
                {
                    //if (d.MarstausID != 0)
                    //{
                    //    d.MaritalStatus = context.MarriageStatuse.FirstOrDefault(m => m.slno == d.MarstausID).mstatus;
                    //}
                    //else
                    //{
                    //    d.MaritalStatus = "";
                    //}
                    //if (d.ReligionID != 0)
                    //{
                    //    d.ReligionName = context.Religion.FirstOrDefault(m => m.sln == d.ReligionID).ReligionName;
                    //}
                    //else
                    //{
                    //    d.ReligionName = "";
                    //}
                    //if (d.Sex != 0)
                    //{
                    //    d.GenderName = context.Gender.FirstOrDefault(m => m.Id == d.Sex).Name;
                    //}
                    //else
                    //{
                    //    d.GenderName = "";
                    //}
                    if (d.DepatmentID != 0)
                    {
                        d.Department = context.Department.FirstOrDefault(m => m.DeptmentID == d.DepatmentID).DeprtmntName;
                    }
                    else
                    {
                        d.Department = "";
                    }
                    if (d.CompanyId != 0)
                    {
                        d.CompanyName = context.CompanyInfo.FirstOrDefault(m => m.Id == d.CompanyId).CompanyName;
                    }
                    else
                    {
                        d.CompanyName = "";
                    }
                    if (d.BranchID != 0)
                    {
                        d.WorkStation = context.BranchInformation.FirstOrDefault(m => m.Slno == d.BranchID).BrnachName;
                    }
                    else
                    {
                        d.WorkStation = "";
                    }
                    if (d.Section != 0)
                    {
                        d.SectionName = context.EmployeeSection.FirstOrDefault(m => m.ID == d.Section).CGroup;
                    }
                    else
                    {
                        d.SectionName = "";
                    }
                    if (d.Designation != 0)
                    {
                        d.DesignationName = context.Designation.FirstOrDefault(m => m.Id == d.Designation).Name;
                    }
                    else
                    {
                        d.DesignationName = "";
                    }
                    if (d.HighestEducation != 0)
                    {
                        d.HigherEdu = context.HighestEducation.FirstOrDefault(m => m.ID == d.HighestEducation).Education;
                    }
                    else
                    {
                        d.HigherEdu = "";
                    }
                });

                
                var numberOfEmployee = employeeList.Count();
                ViewBag.count = numberOfEmployee;
                return View(employeeList.ToPagedList(page ?? 1, 8));
            }
            else
            {
                List<Employee> employeeList = new List<Employee>();
                employeeList = context.Employees.OrderBy(x => x.Id).ToList();
                employeeList.ForEach(d =>
                {
                    //if (d.MarstausID != 0)
                    //{
                    //    d.MaritalStatus = context.MarriageStatuse.FirstOrDefault(m => m.slno == d.MarstausID).mstatus;
                    //}
                    //else
                    //{
                    //    d.MaritalStatus = "";
                    //}
                    //if (d.ReligionID != 0)
                    //{
                    //    d.ReligionName = context.Religion.FirstOrDefault(m => m.sln == d.ReligionID).ReligionName;
                    //}
                    //else
                    //{
                    //    d.ReligionName = "";
                    //}
                    //if (d.Sex != 0)
                    //{
                    //    d.GenderName = context.Gender.FirstOrDefault(m => m.Id == d.Sex).Name;
                    //}
                    //else
                    //{
                    //    d.GenderName = "";
                    //}
                    if (d.DepatmentID != 0)
                    {
                        d.Department = context.Department.FirstOrDefault(m => m.DeptmentID == d.DepatmentID).DeprtmntName;
                    }
                    else
                    {
                        d.Department = "";
                    }
                    if (d.CompanyId != 0)
                    {
                        d.CompanyName = context.CompanyInfo.FirstOrDefault(m => m.Id == d.CompanyId).CompanyName;
                    }
                    else
                    {
                        d.CompanyName = "";
                    }
                    if (d.BranchID != 0)
                    {
                        d.WorkStation = context.BranchInformation.FirstOrDefault(m => m.Slno == d.BranchID).BrnachName;
                    }
                    else
                    {
                        d.WorkStation = "";
                    }
                    if (d.Section != 0)
                    {
                        d.SectionName = context.EmployeeSection.FirstOrDefault(m => m.ID == d.Section).CGroup;
                    }
                    else
                    {
                        d.SectionName = "";
                    }
                    if (d.Designation != 0)
                    {
                        d.DesignationName = context.Designation.FirstOrDefault(m => m.Id == d.Designation).Name;
                    }
                    else
                    {
                        d.DesignationName = "";
                    }
                    if (d.HighestEducation != 0)
                    {
                        d.HigherEdu = context.HighestEducation.FirstOrDefault(m => m.ID == d.HighestEducation).Education;
                    }
                    else
                    {
                        d.HigherEdu = "";
                    }
                });
                var numberOfEmployee = employeeList.Count();
                ViewBag.count = numberOfEmployee;
                return View(employeeList.ToPagedList(page ?? 1, 8));
            }
            
        }
    }
}