using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Payroll
{
    [ShowNotification]
    public class EmployeePromotionController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: EmployeePromotion
        public ActionResult EmployeePromotion()
        {
            ViewBag.Employee = LoadComboBox.LoadAllEmployee();
            ViewBag.GradeList = LoadComboBox.LoadGradeList();
            ViewBag.DesignationList = LoadComboBox.LoadAllDesignation();
            ViewBag.FunctionalDesignationList = LoadComboBox.LoadAllFuncDesignation();
            return View();
        }
        public ActionResult GetEmployeeDetails(int empId)
        {
            var employee = context.Employees.FirstOrDefault(x => x.Id == empId);

            if (employee != null)
            {
                var ODesignation = context.Designation.FirstOrDefault(x => x.Id == employee.Designation).Name;
                //var OFuncDesignation = context.FunctionalDesignation.FirstOrDefault(x => x.ID == employee.FunctDesignation).FuncDesignation;
                //var EmpGrade = Convert.ToInt32(employee.EmpGrade);
                //var OGrade = context.Grade.FirstOrDefault(x => x.GradeID == EmpGrade).GradeName;

                var isExist = context.EmployeePromotion.FirstOrDefault(x => x.EmployeeID == empId);
                if (isExist != null)
                {
                    return Json(new { message = 1, isExist, ODesignation }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { message = 0, ODesignation }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { message = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveEmployeePromotion(EmployeePromotion Promotion)
        {
            using (context)
            {
                string promotionID = "";
                string yearLastTwoPart = Promotion.EffectiveDate.Year.ToString().Substring(2, 2).ToString();
                var noOfPromotion = context.EmployeePromotion.Count();

                if (noOfPromotion > 0)
                {
                    EmployeePromotion lastPromotion = context.EmployeePromotion.ToList().LastOrDefault(x => x.PromotionID.ToString().Substring(0, 2) == yearLastTwoPart);
                    if (lastPromotion == null)
                    {
                        promotionID = yearLastTwoPart + "00001";
                    }
                    else
                    {
                        promotionID = yearLastTwoPart + (Convert.ToInt32(lastPromotion.PromotionID.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                    }
                }
                else
                {
                    promotionID = yearLastTwoPart + "00001";
                }
                Promotion.PromotionID = Convert.ToInt32(promotionID);
                Promotion.OldGrade = context.Grade.FirstOrDefault(x => x.GradeName == Promotion.OldGradeName).GradeID;
                Promotion.OldDesignationID = context.Designation.FirstOrDefault(x => x.Name == Promotion.OldDesignationName).Id;
                Promotion.OldFuncDesignationID = context.FunctionalDesignation.FirstOrDefault(x => x.FuncDesignation == Promotion.OldFuncDesignationName).ID;
                var isexists = context.EmployeePromotion.FirstOrDefault(x => x.EmployeeID == Promotion.EmployeeID);
                if (Promotion != null)
                {
                    context.EmployeePromotion.Add(Promotion);
                    context.SaveChanges();
                    return Json(new { id = Promotion.EmployeeID, tid = Promotion.PromotionID }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    context.EmployeePromotion.Add(Promotion);
                    context.SaveChanges();
                    return Json(new { id = Promotion.EmployeeID, tid = Promotion.PromotionID }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult UpdateEmployeePromotion(EmployeePromotion Promotion)
        {
            var isExist = context.EmployeePromotion.FirstOrDefault(x => x.EmployeeID == Promotion.EmployeeID);
            Promotion.OldGrade = context.Grade.FirstOrDefault(x => x.GradeName == Promotion.OldGradeName).GradeID;
            Promotion.OldDesignationID = context.Designation.FirstOrDefault(x => x.Name == Promotion.OldDesignationName).Id;
            Promotion.OldFuncDesignationID = context.FunctionalDesignation.FirstOrDefault(x => x.FuncDesignation == Promotion.OldFuncDesignationName).ID;
            if (isExist != null)
            {
                context.EmployeePromotion.Remove(isExist);
                //Transfer.Tdate = itob.Tdate.Add(DateTime.Now.TimeOfDay);
                context.EmployeePromotion.Add(Promotion);
                context.SaveChanges();
                return Json(Promotion, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
    }
}