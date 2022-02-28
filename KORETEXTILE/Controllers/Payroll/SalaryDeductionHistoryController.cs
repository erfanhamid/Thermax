using BEEERP.Models.CustomAttribute;
using BEEERP.Models.ViewModel.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Payroll
{
    public class SalaryDeductionHistoryController : Controller
    {
        // GET: SalaryDeductionHistory
        [ShowNotification]
        public ActionResult SearchDeductionHistory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SalaryDeductionHistoryReport(SalaryDeductionHistoryVModel historyVModel)
        {
            return Json(false);
        }
    }
}