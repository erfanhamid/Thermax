using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.Database;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Payroll;
using BEEERP.Models.ViewModel.Payroll;

namespace BEEERP.Controllers.Payroll
{
    [ShowNotification]
    public class EmployeeCashAllowancesController : Controller
    {
        BEEContext context = new BEEContext();
        public ActionResult EmployeeCashAllowances()
        {
            string sql = string.Format("exec GetEmployeeDetailsforCashAllowances");
            ViewBag.list = context.Database.SqlQuery<EmployeeCashAllowancesVModel>(sql).ToList();
            return View();
        }
        public ActionResult GetEmployeeCashAllowances(int EmployeeID)
        {
            if (EmployeeID == 0)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var cashAllowances = context.EmployeeCashAllowances.FirstOrDefault(m => m.EmployeeID == EmployeeID);
                if (cashAllowances != null)
                {
                    cashAllowances.Name = context.Employees.FirstOrDefault(x => x.Id == EmployeeID).Name;
                    return Json(new { cashAllowances = cashAllowances }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    EmployeeCashAllowances CashAllowances = new EmployeeCashAllowances();
                    CashAllowances.EmployeeID = EmployeeID;
                    CashAllowances.Name = context.Employees.FirstOrDefault(x => x.Id == EmployeeID).Name;
                    CashAllowances.CAAmount = 0;
                    CashAllowances.CARefNO = null;
                    CashAllowances.CADescription = null;
                    return Json(new { cashAllowances = CashAllowances }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult SaveEmployeeCashAllowances(EmployeeCashAllowances CashAllowances)
        {
            var isexists = context.EmployeeCashAllowances.FirstOrDefault(x => x.EmployeeID == CashAllowances.EmployeeID);
            if (isexists != null)
            {
                context.EmployeeCashAllowances.Remove(isexists);
                context.EmployeeCashAllowances.Add(CashAllowances);
                context.SaveChanges();
                return Json(new { id = CashAllowances.EmployeeID }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                context.EmployeeCashAllowances.Add(CashAllowances);
                context.SaveChanges();
                return Json(new { id = CashAllowances.EmployeeID }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}