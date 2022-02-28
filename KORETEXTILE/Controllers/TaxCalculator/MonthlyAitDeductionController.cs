using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using BEEERP.Models.TaxCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.TaxCalculator
{
    [ShowNotification]
    public class MonthlyAitDeductionController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: MonthlyAitDeduction
        public ActionResult MonthlyAitDeduction()   
        {
            ViewBag.AssesmentYear = LoadComboBox.LoadAssesmentYear();
            ViewBag.DuringYear = LoadComboBox.LoadDuringYear();
            ViewBag.Cororation = LoadComboBox.LoadCorporation();
            ViewBag.LoadLocation = LoadComboBox.LoadLocation();
            ViewBag.Month = LoadComboBox.LoadMonth();

            return View();
        }

        public ActionResult GetEmployees(string location, int corpuration, string assesmentY, string month)
        {
            var forecast = (from f in context.AITForecast
                           join fl in context.AITForecastLine on f.ID equals fl.ID
                           where f.Location == location && f.Corporation == corpuration && f.FinancialYear == assesmentY
                           select fl).ToList();

            var monthExist = (from m in context.MonthlyAITDeduction
                              join ml in context.MonthlyAITDeductionLine on m.ID equals ml.ID
                              where m.Location == location && m.Corporation == corpuration && m.FinancialYear == assesmentY
                              select new { EmployeeID = ml.EmployeeID, Month = m.Month }).ToList();
          
            monthExist.ForEach(x => {
                if(x.Month == month)
                {
                    var empE = forecast.FirstOrDefault(y => y.EmployeeID == x.EmployeeID);
                    forecast.Remove(empE);
                }
            });

            if (forecast != null)
            {
                forecast.ForEach(x =>
                {
                    x.EmployeeDesignation = FindObjectById.GetERMTransection().FirstOrDefault(y => y.xemp == x.EmployeeID).xdesig;
                });

                return Json(forecast, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveMonthlyAit(MonthlyAITDeduction monthlyAit, List<MonthlyAITDeductionLine> addedItems)
        {
            using (context)
            {
                monthlyAit.ID = GenerateMonthylyAitId();

                addedItems.ForEach(x =>
                {
                    EmployeeBalanceCalculationTAX emb = new EmployeeBalanceCalculationTAX();
                    emb.EmployeeID = x.EmployeeID;
                    emb.Date = monthlyAit.Date;
                    emb.DocumentType = "Deduction";
                    emb.AccountID = "Salary";
                    emb.Description = "";
                    emb.RefNo = "";
                    emb.Amount = x.Amount;
                    emb.DocNO = monthlyAit.ID;
                    emb.TBID = monthlyAit.Corporation;
                    emb.FundType = 5;

                    x.ID = monthlyAit.ID;
                    context.MonthlyAITDeductionLine.Add(x);
                    context.EmployeeBalanceCalculationTAX.Add(emb);
                });

                context.MonthlyAITDeduction.Add(monthlyAit);
                
                UserLog.SaveUserLog(ref context, new UserLog(monthlyAit.ID.ToString(), DateTime.Now, "Deduction", ScreenAction.Save));

                context.SaveChanges();
                return Json(new { monthlyAit }, JsonRequestBehavior.AllowGet);  
            }
        }

        public ActionResult GetMonthlyAitDeductionById(int id)  
        {

            var dedudctionItem = context.MonthlyAITDeduction.FirstOrDefault(x => x.ID == id);

            if (dedudctionItem != null)
            {
                var deductiontLineItem = context.MonthlyAITDeductionLine.Where(x => x.ID == id).ToList();
                deductiontLineItem.ForEach(x =>
                {
                    x.ForcastAmount = (from f in context.AITForecast join 
                                       fl in context.AITForecastLine on f.ID equals fl.ID
                                       where (f.FinancialYear == dedudctionItem.FinancialYear) && (fl.EmployeeID == x.EmployeeID)
                                       select fl.MonthlyAIT).FirstOrDefault();
                    x.EmployeeDesignation = FindObjectById.GetERMTransection().FirstOrDefault(y => y.xemp == x.EmployeeID).xdesig;
                });
                return Json(new { dedudctionItem, deductiontLineItem }, JsonRequestBehavior.AllowGet);  
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        public int GenerateMonthylyAitId()  
        {
            var monthlyAIT = context.MonthlyAITDeduction.ToList().OrderBy(x => x.ID).LastOrDefault();   
            if (monthlyAIT != null)
            {
                return monthlyAIT.ID + 1;
            }
            else
            {
                return 1;
            }
        }
    }
}