using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using BEEERP.Models.Payroll;
using BEEERP.Models.ViewModel.Payroll;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Payroll
{
    [ShowNotification]
    public class OpeningLeaveBalanceController : Controller
    {
        
        BEEContext context = new BEEContext();
        // GET: OpeningLeaveBalance
        public ActionResult OpeningLeaveBalance()
        {
            
            return View();
        }

        [HttpGet]
        public JsonResult GetAllEmployeeInfo()
        {
            string sql = string.Format("exec GetEmployeesForLeaveBalance");
            var result = context.Database.SqlQuery<OpeningLeaveBalanceTableVModel>(sql).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveOpeningLeaveBalance(OpeningLeaveBalance olb)
        {
            if (olb.EmployeeId > 0)
            {
                var leaveBalance = context.OpeningLeaveBalances.FirstOrDefault(x => x.EmployeeId == olb.EmployeeId);
                if (leaveBalance == null)
                {
                    context.OpeningLeaveBalances.Add(olb);
                    UserLog.SaveUserLog(ref context, new UserLog(olb.EmployeeId.ToString(), DateTime.Now, "OpeningLeaveBalance", ScreenAction.Save));
                    context.SaveChanges();
                }
                else
                {
                    context.OpeningLeaveBalances.Remove(leaveBalance);
                    context.SaveChanges();
                    context.OpeningLeaveBalances.Add(olb);
                    UserLog.SaveUserLog(ref context, new UserLog(olb.EmployeeId.ToString(), DateTime.Now, "OpeningLeaveBalance", ScreenAction.Save));
                    context.SaveChanges();                   
                }
                return Json(olb, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}