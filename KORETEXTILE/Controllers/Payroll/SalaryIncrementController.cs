using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Payroll;
using BEEERP.Models.ViewModel.Payroll;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Payroll
{
    [ShowNotification]
    //[Authorize(Roles = "HraAdmin,HraOperator,HraViewer,HraApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    public class SalaryIncrementController : Controller
    {
        BEEContext beeContext = new BEEContext();
        public ActionResult SalaryIncrement()
        {
            TempData["Branch"] = LoadComboBox.LoadBranchInfo();
            TempData["Designation"] = LoadComboBox.LoadAllDesignation();
            TempData["Company"] = LoadComboBox.LoadCompanyInformation();
            TempData["Department"] = LoadComboBox.LoadAllDepartment();
            TempData["FuncDesignation"] = LoadComboBox.LoadAllFuncDesignation();
            TempData["Section"] = LoadComboBox.LoadAllSection();
            return View();
        }
        [HttpGet]
        public ActionResult GetEmployeeInformation()
        {
            string sql = string.Format("select * from ViewEmployeeInfoForSalaryIncrement");
            var result = beeContext.Database.SqlQuery<SalaryIncrementViewModel>(sql).ToList();
            TempData["result"] = result;
            if (result != null)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false);
            }
        }
        [HttpPost]
        public ActionResult SalaryIncrement(SalaryIncrement increment,int update)
        {
            if (increment.ApplicableDate!=null && increment.EmployeeId>0 && increment.IncrementAmount>=0)
            {
                SalaryIncrement checkincrement = null;
                if (increment.IncrementAmount > 0)
                {
                    checkincrement = beeContext.SalaryIncrement.FirstOrDefault(x => x.EmployeeId == increment.EmployeeId && x.ApplicableDate.Month == increment.ApplicableDate.Month && x.ApplicableDate.Year == increment.ApplicableDate.Year);
                }
                var checkpromotion = beeContext.EmployeesPromotionHistory.FirstOrDefault(x => x.EmployeeId == increment.EmployeeId && x.ApplicableDate.Month == increment.ApplicableDate.Month && x.ApplicableDate.Year == increment.ApplicableDate.Year);
                if (update == 0 && (checkincrement!=null || checkpromotion!=null))
                {
                    string fullMonthName = new DateTime(increment.ApplicableDate.Year, increment.ApplicableDate.Month, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("en"));
                    if (checkincrement != null && checkpromotion==null)
                    {
                        return Json(new { id = 1,message="Increment of this employee is already added for the month of "+fullMonthName+"-"+increment.ApplicableDate.Year+",\nDo you want to update ?"}, JsonRequestBehavior.AllowGet);
                    }
                    if (checkincrement == null && checkpromotion != null)
                    {
                        return Json(new { id = 1, message = "New Information of this employee is already added for the month of " + fullMonthName + "-" + increment.ApplicableDate.Year + ",\nDo you want to update ?" }, JsonRequestBehavior.AllowGet);
                    }
                    if (checkincrement != null && checkpromotion != null)
                    {
                        return Json(new { id = 1, message = "Increment and other new Information of this employee is already added for the month of " + fullMonthName + "-" + increment.ApplicableDate.Year + ",\nDo you want to update ?" }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (update == 1 && (checkincrement != null || checkpromotion != null))
                {
                    if (increment.IncrementAmount > 0)
                    {
                        var check = (from si in beeContext.SalaryInformation
                                     join sil in beeContext.SalaryInformationLine on si.SalaryInfoNo equals sil.SalaryInfoNo
                                     where sil.EmployeeId == increment.EmployeeId && si.ProcessDate > increment.ApplicableDate
                                     select new { id = sil.EmployeeId, processdate = si.ProcessDate }).ToList();
                        if (check.Count > 0)
                        {
                            increment.IsArrear = true;
                        }
                        else
                        {
                            increment.IsArrear = false;
                        }
                        //remove the previous
                        if (checkincrement != null)
                        {
                            SalaryIncrement salaryIncrement = beeContext.SalaryIncrement.FirstOrDefault(x => x.EmployeeId == increment.EmployeeId && x.ApplicableDate.Month == increment.ApplicableDate.Month && x.ApplicableDate.Year == increment.ApplicableDate.Year);
                            beeContext.SalaryIncrement.Remove(salaryIncrement);
                        }
                        increment.Gross = beeContext.EmployeeSalaryAdditions.FirstOrDefault(x => x.EmployeeId == increment.EmployeeId).Gross + increment.IncrementAmount;
                        beeContext.SalaryIncrement.Add(increment);
                        //beeContext.SaveChanges();
                    }
                    //remove previous
                    if (checkpromotion != null)
                    {
                        EmployeesPromotionHistory promotionHistory = beeContext.EmployeesPromotionHistory.FirstOrDefault(x => x.EmployeeId == increment.EmployeeId && x.ApplicableDate.Month == increment.ApplicableDate.Month && x.ApplicableDate.Year == increment.ApplicableDate.Year);
                        beeContext.EmployeesPromotionHistory.Remove(promotionHistory);
                        beeContext.SaveChanges();
                    }
                    string sql = string.Format("select * from ViewEmployeeInfoForSalaryIncrement");
                    var oldinfo = beeContext.Database.SqlQuery<SalaryIncrementViewModel>(sql).ToList().FirstOrDefault(x => x.EmployeeId == increment.EmployeeId);
                    EmployeesPromotionHistory promotionhistory = new EmployeesPromotionHistory();
                    promotionhistory.EmployeeId = increment.EmployeeId;
                    promotionhistory.Name = oldinfo.Name;
                    promotionhistory.ApplicableDate = increment.ApplicableDate;

                    if (beeContext.EmployeesPromotionHistory.ToList().Count > 0)
                    {
                        bool notfound = true;
                        EmployeesPromotionHistory emp = null;
                        var pchkL = beeContext.EmployeesPromotionHistory.OrderByDescending(x => x.Id).Where(x => x.EmployeeId == increment.EmployeeId).ToList();
                        if (pchkL.Count >0)
                        {
                            for (int a = 0; a < pchkL.Count; a++)
                            {
                                if (pchkL[a].ApplicableDate >= increment.ApplicableDate)
                                {
                                    emp = pchkL[a];
                                    promotionhistory.PreviousDate = emp.PreviousDate;
                                    emp.PreviousDate = increment.ApplicableDate;
                                    notfound = false;
                                    break;
                                }
                                
                            }
                        }
                        if(notfound && pchkL.Count>0)
                        {
                            emp = pchkL.FirstOrDefault();
                            promotionhistory.PreviousDate = emp.ApplicableDate;
                        }
                        else
                        {
                            DateTime date = beeContext.Employees.FirstOrDefault(x => x.Id == increment.EmployeeId).JoiningDate;
                            promotionhistory.PreviousDate = date;
                        }
                        //EmployeesPromotionHistory pchk = pchkL.LastOrDefault();
                        //if (pchk != null)
                        //{
                        //    if (pchk.ApplicableDate < increment.ApplicableDate)
                        //    {
                        //        promotionhistory.PreviousDate = pchk.ApplicableDate;
                        //    }
                        //    if (pchk.ApplicableDate >= increment.ApplicableDate)
                        //    {
                        //        EmployeesPromotionHistory employeesPromotion = pchk;
                        //        //beeContext.EmployeesPromotionHistory.Remove(pchk);
                        //        employeesPromotion.PreviousDate = increment.ApplicableDate;
                        //        //beeContext.SaveChanges();
                        //        pchkL = beeContext.EmployeesPromotionHistory.Where(x => x.EmployeeId == increment.EmployeeId).ToList();
                        //        EmployeesPromotionHistory pchagain = pchkL.LastOrDefault();
                        //        if (pchagain != null)
                        //        {
                        //            //promotionhistory.PreviousDate = pchagain.ApplicableDate;
                        //            promotionhistory.PreviousDate = pchagain.PreviousDate;
                        //        }
                        //        if (pchagain == null)
                        //        {
                        //            DateTime date = beeContext.Employees.FirstOrDefault(x => x.Id == increment.EmployeeId).JoiningDate;
                        //            promotionhistory.PreviousDate = date;
                        //        }
                        //    }
                        //}
                        //if (pchk == null)
                        //{
                        //    DateTime date = beeContext.Employees.FirstOrDefault(x => x.Id == increment.EmployeeId).JoiningDate;
                        //    promotionhistory.PreviousDate = date;
                        //}
                    }
                    else
                    {
                        DateTime date = beeContext.Employees.FirstOrDefault(x => x.Id == increment.EmployeeId).JoiningDate;
                        promotionhistory.PreviousDate = date;
                    }
                    //if (beeContext.EmployeesPromotionHistory.ToList().Count > 0)
                    //{
                    //    var pchkL = beeContext.EmployeesPromotionHistory.Where(x => x.EmployeeId == increment.EmployeeId).ToList();
                    //    EmployeesPromotionHistory pchk = pchkL.LastOrDefault();
                    //    if (pchk != null)
                    //    {
                    //        promotionhistory.PreviousDate = pchk.ApplicableDate;
                    //    }
                    //    if (pchk == null)
                    //    {
                    //        DateTime date = beeContext.Employees.FirstOrDefault(x => x.Id == increment.EmployeeId).JoiningDate;
                    //        promotionhistory.PreviousDate = date;
                    //    }
                    //}
                    //else
                    //{
                    //    DateTime date = beeContext.Employees.FirstOrDefault(x => x.Id == increment.EmployeeId).JoiningDate;
                    //    promotionhistory.PreviousDate = date;
                    //}

                    //old info
                    promotionhistory.OldBranchID = oldinfo.BranchId;
                    promotionhistory.OldCompanyId = oldinfo.CompanyId;
                    promotionhistory.OldDepatmentID = oldinfo.DepartmentId;
                    promotionhistory.OldDesignation = oldinfo.DesignationId;
                    promotionhistory.OldFunctDesignation = oldinfo.FuncDesignationId;
                    promotionhistory.OldGrossSalary = oldinfo.Gross;
                    promotionhistory.OldSection = oldinfo.SectionId;

                    //new info
                    if (increment.BranchId != 0)
                    {
                        promotionhistory.NewBranchID = increment.BranchId;
                    }
                    if (increment.CompanyId != 0)
                    {
                        promotionhistory.NewCompanyId = increment.CompanyId;
                    }
                    if (increment.DepartmentId != 0)
                    {
                        promotionhistory.NewDepatmentID = increment.DepartmentId;
                    }
                    if (increment.DesignationId != 0)
                    {
                        promotionhistory.NewDesignation = increment.DesignationId;
                    }
                    if (increment.FuncDesignationId != 0)
                    {
                        promotionhistory.NewFunctDesignation = increment.FuncDesignationId;
                    }
                    if (increment.Gross != 0)
                    {
                        promotionhistory.NewGrossSalary = increment.Gross;
                    }
                    if (increment.SectionId != 0)
                    {
                        promotionhistory.NewSection = increment.SectionId;
                    }

                    //Default
                    if (increment.BranchId <= 0)
                    {
                        promotionhistory.NewBranchID = oldinfo.BranchId;
                    }
                    if (increment.CompanyId <= 0)
                    {
                        promotionhistory.NewCompanyId = oldinfo.CompanyId;
                    }
                    if (increment.DepartmentId <= 0)
                    {
                        promotionhistory.NewDepatmentID = oldinfo.DepartmentId;
                    }
                    if (increment.DesignationId <= 0)
                    {
                        promotionhistory.NewDesignation = oldinfo.DesignationId;
                    }
                    if (increment.FuncDesignationId <= 0)
                    {
                        promotionhistory.NewFunctDesignation = oldinfo.FuncDesignationId;
                    }
                    if (increment.Gross <= 0)
                    {
                        promotionhistory.NewGrossSalary = oldinfo.Gross;
                    }
                    if (increment.SectionId <= 0)
                    {
                        promotionhistory.NewSection = oldinfo.SectionId;
                    }
                    beeContext.EmployeesPromotionHistory.Add(promotionhistory);
                    int i = beeContext.SaveChanges();
                    if (i > 0)
                    { return Json(new { id = increment.EmployeeId }, JsonRequestBehavior.AllowGet); }
                    else
                    {
                        return Json(new { id = 0 }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (update==0 && checkincrement == null && checkpromotion == null)
                {
                    if (increment.IncrementAmount > 0)
                    {
                        var check = (from si in beeContext.SalaryInformation
                                     join sil in beeContext.SalaryInformationLine on si.SalaryInfoNo equals sil.SalaryInfoNo
                                     where sil.EmployeeId == increment.EmployeeId && si.ProcessDate > increment.ApplicableDate
                                     select new { id = sil.EmployeeId, processdate = si.ProcessDate }).ToList();
                        if (check.Count > 0)
                        {
                            increment.IsArrear = true;
                        }
                        else
                        {
                            increment.IsArrear = false;
                        }
                        increment.Gross = beeContext.EmployeeSalaryAdditions.FirstOrDefault(x => x.EmployeeId == increment.EmployeeId).Gross + increment.IncrementAmount;
                        beeContext.SalaryIncrement.Add(increment);
                        //beeContext.SaveChanges();
                    }

                    string sql = string.Format("select * from ViewEmployeeInfoForSalaryIncrement");
                    var oldinfo = beeContext.Database.SqlQuery<SalaryIncrementViewModel>(sql).ToList().FirstOrDefault(x => x.EmployeeId == increment.EmployeeId);
                    EmployeesPromotionHistory promotionhistory = new EmployeesPromotionHistory();
                    //if (beeContext.EmployeesPromotionHistory.ToList().Count > 0)
                    //{
                    //    var pchkL = beeContext.EmployeesPromotionHistory.Where(x => x.EmployeeId == increment.EmployeeId).ToList();
                    //    EmployeesPromotionHistory pchk = pchkL.LastOrDefault();
                    //    if (pchk != null)
                    //    {
                    //        promotionhistory.PreviousDate = pchk.ApplicableDate;
                    //    }
                    //    if (pchk == null)
                    //    {
                    //        DateTime date = beeContext.Employees.FirstOrDefault(x => x.Id == increment.EmployeeId).JoiningDate;
                    //        promotionhistory.PreviousDate = date;
                    //    }
                    //}
                    //else
                    //{
                    //    DateTime date = beeContext.Employees.FirstOrDefault(x => x.Id == increment.EmployeeId).JoiningDate;
                    //    promotionhistory.PreviousDate = date;
                    //}
                    if (beeContext.EmployeesPromotionHistory.ToList().Count > 0)
                    {
                        bool notfound = true;
                        EmployeesPromotionHistory emp = null;
                        var pchkL = beeContext.EmployeesPromotionHistory.OrderByDescending(x => x.Id).Where(x => x.EmployeeId == increment.EmployeeId).ToList();
                        if (pchkL.Count > 0)
                        {
                            for (int a = 0; a < pchkL.Count; a++)
                            {
                                if (pchkL[a].ApplicableDate >= increment.ApplicableDate)
                                {
                                    emp = pchkL[a];
                                    promotionhistory.PreviousDate = emp.PreviousDate;
                                    emp.PreviousDate = increment.ApplicableDate;
                                    notfound = false;
                                    break;
                                }

                            }
                        }
                        if (notfound && pchkL.Count > 0)
                        {
                            emp = pchkL.FirstOrDefault();
                            promotionhistory.PreviousDate = emp.ApplicableDate;
                        }
                        else
                        {
                            DateTime date = beeContext.Employees.FirstOrDefault(x => x.Id == increment.EmployeeId).JoiningDate;
                            promotionhistory.PreviousDate = date;
                        }
                        //EmployeesPromotionHistory pchk = pchkL.LastOrDefault();
                        //if (pchk != null)
                        //{
                        //    if (pchk.ApplicableDate < increment.ApplicableDate)
                        //    {
                        //        promotionhistory.PreviousDate = pchk.ApplicableDate;
                        //    }
                        //    if (pchk.ApplicableDate >= increment.ApplicableDate)
                        //    {
                        //        EmployeesPromotionHistory employeesPromotion = pchk;
                        //        //beeContext.EmployeesPromotionHistory.Remove(pchk);
                        //        employeesPromotion.PreviousDate = increment.ApplicableDate;
                        //        //beeContext.SaveChanges();
                        //        pchkL = beeContext.EmployeesPromotionHistory.Where(x => x.EmployeeId == increment.EmployeeId).ToList();
                        //        EmployeesPromotionHistory pchagain = pchkL.LastOrDefault();
                        //        if (pchagain != null)
                        //        {
                        //            //promotionhistory.PreviousDate = pchagain.ApplicableDate;
                        //            promotionhistory.PreviousDate = pchagain.PreviousDate;
                        //        }
                        //        if (pchagain == null)
                        //        {
                        //            DateTime date = beeContext.Employees.FirstOrDefault(x => x.Id == increment.EmployeeId).JoiningDate;
                        //            promotionhistory.PreviousDate = date;
                        //        }
                        //    }
                        //}
                        //if (pchk == null)
                        //{
                        //    DateTime date = beeContext.Employees.FirstOrDefault(x => x.Id == increment.EmployeeId).JoiningDate;
                        //    promotionhistory.PreviousDate = date;
                        //}
                    }
                    else
                    {
                        DateTime date = beeContext.Employees.FirstOrDefault(x => x.Id == increment.EmployeeId).JoiningDate;
                        promotionhistory.PreviousDate = date;
                    }

                    promotionhistory.EmployeeId = increment.EmployeeId;
                    promotionhistory.Name = oldinfo.Name;
                    promotionhistory.ApplicableDate = increment.ApplicableDate;

                    //old info
                    promotionhistory.OldBranchID = oldinfo.BranchId;
                    promotionhistory.OldCompanyId = oldinfo.CompanyId;
                    promotionhistory.OldDepatmentID = oldinfo.DepartmentId;
                    promotionhistory.OldDesignation = oldinfo.DesignationId;
                    promotionhistory.OldFunctDesignation = oldinfo.FuncDesignationId;
                    promotionhistory.OldGrossSalary = oldinfo.Gross;
                    promotionhistory.OldSection = oldinfo.SectionId;

                    //new info
                    if (increment.BranchId != 0)
                    {
                        promotionhistory.NewBranchID = increment.BranchId;
                    }
                    if (increment.CompanyId != 0)
                    {
                        promotionhistory.NewCompanyId = increment.CompanyId;
                    }
                    if (increment.DepartmentId != 0)
                    {
                        promotionhistory.NewDepatmentID = increment.DepartmentId;
                    }
                    if (increment.DesignationId != 0)
                    {
                        promotionhistory.NewDesignation = increment.DesignationId;
                    }
                    if (increment.FuncDesignationId != 0)
                    {
                        promotionhistory.NewFunctDesignation = increment.FuncDesignationId;
                    }
                    if (increment.Gross != 0)
                    {
                        promotionhistory.NewGrossSalary = increment.Gross;
                    }
                    if (increment.SectionId != 0)
                    {
                        promotionhistory.NewSection = increment.SectionId;
                    }

                    //Default
                    if (increment.BranchId <= 0)
                    {
                        promotionhistory.NewBranchID = oldinfo.BranchId;
                    }
                    if (increment.CompanyId <= 0)
                    {
                        promotionhistory.NewCompanyId = oldinfo.CompanyId;
                    }
                    if (increment.DepartmentId <= 0)
                    {
                        promotionhistory.NewDepatmentID = oldinfo.DepartmentId;
                    }
                    if (increment.DesignationId <= 0)
                    {
                        promotionhistory.NewDesignation = oldinfo.DesignationId;
                    }
                    if (increment.FuncDesignationId <= 0)
                    {
                        promotionhistory.NewFunctDesignation = oldinfo.FuncDesignationId;
                    }
                    if (increment.Gross <= 0)
                    {
                        promotionhistory.NewGrossSalary = oldinfo.Gross;
                    }
                    if (increment.SectionId <= 0)
                    {
                        promotionhistory.NewSection = oldinfo.SectionId;
                    }
                    beeContext.EmployeesPromotionHistory.Add(promotionhistory);
                    int i = beeContext.SaveChanges();
                    if (i > 0)
                    { return Json(new { id = increment.EmployeeId }, JsonRequestBehavior.AllowGet); }
                    else
                    {
                        return Json(new { id = 0 }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { id = 0 }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { id = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}