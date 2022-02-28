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
    public class EnterChallanController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: EnterChallan
        public ActionResult EnterChallan()
        {
            ViewBag.AssesmentYear = LoadComboBox.LoadAssesmentYear();
            ViewBag.DuringYear = LoadComboBox.LoadDuringYear();
            ViewBag.Cororation = LoadComboBox.LoadCorporation();
            ViewBag.LoadLocation = LoadComboBox.LoadLocation();
            return View();
        }

        public ActionResult GetEmployees(string location, int corpuration, string assesmentY, string date)  
        {
            DateTime date1 = Convert.ToDateTime(date);
            //var deduction = (from b in context.EmployeeBalanceCalculationTAX
            //                join m in context.MonthlyAITDeduction on b.DocNO equals m.ID
            //                join ml in context.MonthlyAITDeductionLine on b.EmployeeID equals ml.EmployeeID
            //                where ((m.Location == location) && (b.TBID == corpuration) && (m.FinancialYear == assesmentY) && (b.Date <= date1))
            //                select new { EmployeeID = b.EmployeeID, Amount = b.Amount, WIth_Without = ml.WIth_Without}).ToList().GroupBy(x => new {
            //                    x.EmployeeID,
            //                    x.WIth_Without
            //                }).SelectMany(x=>x.Select(z=>new {z.EmployeeID, Amount = x.Sum(k=>k.Amount),z.WIth_Without })).ToList().Distinct();

            var deduction = (from m in context.MonthlyAITDeduction join
                             ml in context.MonthlyAITDeductionLine on m.ID equals ml.ID
                             where (m.FinancialYear == assesmentY) && (m.Corporation == corpuration) && (m.Location == location)
                             select new { EmployeeID = ml.EmployeeID, WIth_Without = ml.WIth_Without, Amount = (from b in context.EmployeeBalanceCalculationTAX
                                                                                                                where (b.EmployeeID == ml.EmployeeID) && (b.TBID == m.Corporation) && (b.Date <= date1)
                                                                                                                select b.Amount).Sum() }).ToList().Distinct();
            var netPayable = (from t in context.TaxCalculation
                              join tl in context.TaxCalculationLine on t.ID equals tl.ID
                              where (t.Location == location) && (t.Corporation == corpuration) && (t.FinancialYear == assesmentY)
                              select tl).ToList();

            if (deduction != null)
            {
                float percent = context.ChallanPercentageList.FirstOrDefault().Percentage;

                return Json(new { deduction, percent, netPayable }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            } 
        }

        public ActionResult GetRemainTax(string location, int corpuration, string assesmentY, string date)
        {
            DateTime date1 = Convert.ToDateTime(date);
            var netPayable = (from t in context.TaxCalculation
                              join tl in context.TaxCalculationLine on t.ID equals tl.ID
                              where (t.Location == location) && (t.Corporation == corpuration) && (t.FinancialYear == assesmentY)
                              select tl).ToList();
            
            var sumOfTax = (from b in context.TAXBalanceCalculation join 
                            tl in context.TaxCalculationLine on b.EmployeeID equals tl.EmployeeID
                            join t in context.TaxCalculation on tl.ID equals t.ID
                            where (t.Location == location) && (t.Corporation == corpuration) && (t.FinancialYear == assesmentY) && (b.Date <= date1)
                            select new { b.Amount, b.EmployeeID }).GroupBy(x => x.EmployeeID).ToList();


            if (netPayable != null)
            {
                sumOfTax.ForEach(x =>
                {
                    var amount = x.Sum(y => y.Amount);
                    if (amount == 0)
                    {
                        var getEmpl = netPayable.FirstOrDefault(y => y.EmployeeID == x.FirstOrDefault().EmployeeID);
                        getEmpl.NetPayable = amount;

                    }
                });

                return Json(netPayable, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveAitChallan(AITChallan aitChallan, List<AITChallanLine> addedItems)
        {
            using (context)
            {
                aitChallan.ID = GenerateAitChallanId();

                addedItems.ForEach(x =>
                {
                    EmployeeBalanceCalculationTAX emb = new EmployeeBalanceCalculationTAX();
                    emb.EmployeeID = x.EmployeeID;
                    emb.Date = aitChallan.Date;
                    emb.DocumentType = "Challan";
                    emb.AccountID = "Cash";
                    emb.Description = aitChallan.Description;
                    emb.RefNo = aitChallan.ChallanNo;
                    emb.Amount = -x.Amount;
                    emb.DocNO = aitChallan.ID;
                    emb.TBID = aitChallan.Corporation;
                    emb.FundType = 5;

                    x.ID = aitChallan.ID;
                    context.AITChallanLine.Add(x);
                    context.EmployeeBalanceCalculationTAX.Add(emb);

                    TAXBalanceCalculation taxb = new TAXBalanceCalculation();
                    taxb.EmployeeID = x.EmployeeID;
                    taxb.DocNO = aitChallan.ID;
                    taxb.Date = aitChallan.Date;
                    taxb.DocumentType = "Challan";
                    taxb.RefNo = aitChallan.ChallanNo;
                    taxb.Description = aitChallan.Description;
                    taxb.Amount = -x.Amount;
                    taxb.TBID = aitChallan.Corporation;

                    context.TAXBalanceCalculation.Add(taxb);
                });

                context.AITChallan.Add(aitChallan);

                UserLog.SaveUserLog(ref context, new UserLog(aitChallan.ID.ToString(), DateTime.Now, "Challan", ScreenAction.Save));

                context.SaveChanges();
                return Json(new { aitChallan }, JsonRequestBehavior.AllowGet);  
            }
        }

        public ActionResult GetAitChallanById(int id)   
        {

            var aitChallanItem = context.AITChallan.FirstOrDefault(x => x.ID == id);

            if (aitChallanItem != null)
            {
                var aitChallanLineItem = context.AITChallanLine.Where(x => x.ID == id).ToList();
                return Json(new { aitChallanItem, aitChallanLineItem }, JsonRequestBehavior.AllowGet);  
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        public int GenerateAitChallanId()
        {
            var aitChallan = context.AITChallan.ToList().OrderBy(x => x.ID).LastOrDefault();
            if (aitChallan != null)
            {
                return aitChallan.ID + 1;
            }
            else
            {
                return 1;
            }
        }
    }
}