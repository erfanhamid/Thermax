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
    public class ForecastCalculatorController : Controller
    {
        List<ERMTransection> erml = FindObjectById.GetERMTransection();
        BEEContext context = new BEEContext();
        bool isUpdate = false;
        string Type = "";
        string financialY = "";
        // GET: ForecastCalculator
        public ActionResult ForecastCalculator()    
        {
            ViewBag.AssesmentYear = LoadComboBox.LoadAssesmentYear();
            ViewBag.DuringYear = LoadComboBox.LoadDuringYear();
            ViewBag.Cororation = LoadComboBox.LoadCorporation();
            ViewBag.LoadLocation = LoadComboBox.LoadLocation();
            return View();
        }

        public ActionResult GetEmployees(string location, int corpuration, string assesmentY)
        {
            string s1 = assesmentY.Substring(0, 4);
            DateTime start = Convert.ToDateTime(s1 + "-07-01");
            DateTime end = Convert.ToDateTime(s1 + "-07-31");
            var data = erml.Where(x => x.xprloc == location && x.xpaydate >= start && x.xpaydate <= end && x.xprorg == corpuration).OrderBy(y => y.xemp).ToList();
            var data1 = data.GroupBy(y => y.xemp).Distinct().ToList();
            var forecast = (from f in context.AITForecast
                            join fl in context.AITForecastLine on f.ID equals fl.ID
                            where f.Location == location && f.Corporation == corpuration && f.FinancialYear == assesmentY
                            select fl).ToList();
            List<IndividualEmployeeTaxCalculation> iETCl = new List<IndividualEmployeeTaxCalculation>();
            data1.ForEach(x =>
            {
                IndividualEmployeeTaxCalculation iETC = new IndividualEmployeeTaxCalculation();
                iETC = GetEmployeeForecastDetails(x.FirstOrDefault().xemp, assesmentY);
                //iETC.EmployeeId = x.FirstOrDefault().xemp;
                iETC.EmployeeDesignation = x.FirstOrDefault().xdesig;
                //if (iETC.MonthlyAitWithoutInv != 0)
                //{
                    var forecastExist = forecast.FirstOrDefault(y => y.EmployeeID == iETC.EmployeeId);
                    if (forecastExist == null)
                    {
                        iETCl.Add(iETC);
                    }
                //}
            });
            return Json(iETCl, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEmployeeForcastDetails(int empId, string assesmentYear, string type = "")   
        {
            if (assesmentYear == "1")
            {
                isUpdate = true;
                ViewBag.IsUpdate = true;
                assesmentYear = GetFinancialYear();
                ViewBag.AssesmentYear = assesmentYear;
                GetEmployeeForecastDetails(empId, assesmentYear);
                ViewBag.Type = type;
            }
            else
            {
                GetEmployeeForecastDetails(empId, assesmentYear);
            }

            return View();
        }

        public IndividualEmployeeTaxCalculation GetEmployeeForecastDetails(int empId, string assesmentYear)  
        {
            string e1 = assesmentYear.Substring(0, 4);
            DateTime start = Convert.ToDateTime(e1 + "-07-01");
            DateTime end = Convert.ToDateTime(e1 + "-07-31");

            if(isUpdate == true)
            {
                string s1 = assesmentYear.Substring(7, 4);
                start = Convert.ToDateTime(e1 + "-07-01");
                end = Convert.ToDateTime(s1 + "-06-30");
            }


            var empDetails = erml.Where(x => x.xemp == empId && x.xpaydate >= start && x.xpaydate <= end).OrderBy(y => y.xpaydate).ToList();

            CalculateGrossSalary(empDetails, Convert.ToInt32(e1));


            var ecl = CalculateExemptedAmount(empDetails);

            decimal totIncome = 0;
            ecl.ForEach(x =>
            {
                totIncome += x.TaxableAmount;
            });
            ViewBag.TotIncome = totIncome;

            decimal taxabledIncome = CalculateLessExempted(totIncome);
            ViewBag.TaxableAmount = taxabledIncome;

            var sl = CalculateSlabList(taxabledIncome);
            ViewBag.SlaBList = sl;
            decimal taxWithoutInvest = 0;
            sl.ForEach(x =>
            {
                taxWithoutInvest += x.TaxableAmount;
            });
            ViewBag.Tax = taxWithoutInvest;

            var pfAmountDetails = ecl.FirstOrDefault(x => x.BreakeDown == "PF");
            decimal pfAmount = 0;
            if (pfAmountDetails == null)
            {
                pfAmount = 0;
            }
            else
            {
                pfAmount = pfAmountDetails.Amount;
            }
            float invPercentage = context.RebatePercntgList.First().Percentage;
            decimal investmentAmount = ((totIncome - pfAmount) * Convert.ToDecimal(invPercentage)) / 100;
            ViewBag.InvestmentAmount = investmentAmount;

            var invL = CalculateInvestment(investmentAmount);
            ViewBag.Investment = invL;

            decimal totRebate = 0;
            invL.ForEach(x =>
            {
                totRebate += x.RebateAmount;
            });
            ViewBag.TotalRebateOnInvestment = totRebate;
            ViewBag.TotalIncomeTaxPayableWithInvestment = taxWithoutInvest - totRebate;

            IndividualEmployeeTaxCalculation iETC = new IndividualEmployeeTaxCalculation();
            iETC.EmployeeId = empId;
            iETC.TaxPayWithoutInv = taxWithoutInvest;
            iETC.MonthlyAitWithoutInv = iETC.TaxPayWithoutInv / 12;
            iETC.TaxPayable = taxWithoutInvest - totRebate;
            iETC.MonthlyAitWithInv = iETC.TaxPayable / 12;

            if (isUpdate == true)
            {
                if (Type == "With Investment")
                {
                    iETC.FinancialYear = financialY;
                    iETC.EmployeeId = empId;
                    iETC.EmployeeDesignation = FindObjectById.GetERMTransection().FirstOrDefault(x => x.xemp == iETC.EmployeeId).xdesig;
                    iETC.WIth_Without = Type;
                    iETC.MonthlyAIT = iETC.MonthlyAitWithInv;

                }
                else
                {
                    iETC.FinancialYear = financialY;
                    iETC.EmployeeId = empId;
                    iETC.EmployeeDesignation = FindObjectById.GetERMTransection().FirstOrDefault(x => x.xemp == iETC.EmployeeId).xdesig;
                    iETC.WIth_Without = Type;
                    iETC.MonthlyAIT = iETC.MonthlyAitWithoutInv;
                }
            }

            return iETC;

        }

        public List<GrossSalary> CalculateGrossSalary(List<ERMTransection> empDetails, int e1)
        {
            //calculate Gross Salary
            empDetails = empDetails.Where(x => x.xpaycode == 1000).ToList();
            List<GrossSalary> gsL = new List<GrossSalary>();
            if (empDetails.Count == 0)
            {
                return new List<GrossSalary>();
            }
            else
            {
                DateTime startDate = empDetails.First().xpaydate;
                decimal firstBasic = empDetails.First().xbasic;
                if (isUpdate == true)
                {
                    firstBasic = empDetails.Max(x => x.xbasic);
                    //startDate = empDetails.Select(x => x.xpaydate).Max();
                }

                GrossSalary gs = new GrossSalary();

                gs.MonthlyGross = firstBasic * Convert.ToDecimal(1.5);
                gs.NoOfMonths = 12;
                gs.TotalGross = gs.MonthlyGross * gs.NoOfMonths;
                int e2 = e1 + 1;
                gs.Month = "July "+ e1 +" - "+ "June "+ e2;
                gsL.Add(gs);
                
                ViewBag.GrossSalary = gsL;
                ViewBag.TotGross = gs.TotalGross;
                return gsL;
            }

        }
        public List<ExempCalculation> CalculateExemptedAmount(List<ERMTransection> empDetails)
        {
            //calculate Exempted Amount
            
            var exemption = context.ExemtionList.ToList();
            List<ExempCalculation> ecl = new List<ExempCalculation>();

            //for basic Salary
            ExempCalculation ec = new ExempCalculation();
            ec.BreakeDown = "Basic Salary(2/3 of Gross)";
            ec.Exempted = 0;
            decimal totalBasic = 0;
            decimal basic = 0;
            var empDetailsForBasic = empDetails.Where(x => x.xpaycode == 1000).FirstOrDefault();
            
            if (empDetailsForBasic == null)
            {
                
            }
            else
            {
                if (isUpdate == true)
                {
                    basic = empDetails.Where(x => x.xpaycode == 1000).Max(y => y.xbasic);
                }
                else
                {
                    basic = empDetailsForBasic.xbasic;
                }

                totalBasic = basic * Convert.ToDecimal(12);
                //totalBasic = empDetailsForBasic.xbasic * Convert.ToDecimal(12);
                ec.Amount = totalBasic;

                var empDetailsForBasicD = empDetails.Where(x => x.xpaycode == 2100 || x.xpaycode == 2250).ToList();
                decimal basicDeduction = 0;
                empDetailsForBasicD.ForEach(x =>
                {
                    basicDeduction += x.xamount;
                });
                if(isUpdate == true)
                {
                    ec.Deduction = basicDeduction;
                }
                else
                {
                    ec.Deduction = basicDeduction * Convert.ToDecimal(12);
                }
                ec.NetOfDeduction = totalBasic - ec.Deduction;
                ec.TaxableAmount = ec.NetOfDeduction;
                ecl.Add(ec);
            }
            

            //for House Rent
            var exempHouseRent = exemption.FirstOrDefault(x => x.Item.ToLower().Trim() == "House Rent".ToLower().Trim());
            ExempCalculation ec1 = new ExempCalculation();

            ec1.BreakeDown = exempHouseRent.Item + "(" + exempHouseRent.Percentage + "%" + ")";
            ec1.Amount = (Convert.ToDecimal(exempHouseRent.Percentage) / 100) * totalBasic;
            
            var empDetailsForHRD = empDetails.Where(x => x.xpaycode == 2270).FirstOrDefault();
            if(empDetailsForHRD == null)
            {

            }
            else
            {
                ec1.Deduction = empDetailsForHRD.xamount * Convert.ToDecimal(12);
                ec1.NetOfDeduction = ec1.Amount - ec1.Deduction;
                ec1.Exempted = exempHouseRent.Limit;

                if (ec1.NetOfDeduction >= ec1.Exempted)
                {
                    ec1.TaxableAmount = ec1.NetOfDeduction - ec1.Exempted;
                }
                else
                {
                    ec1.Exempted = ec1.NetOfDeduction;
                    ec1.TaxableAmount = 0;
                }

                ecl.Add(ec1);
            }
            

            //ConveyanceAllowance
            var exempConveyanceAllowance = exemption.FirstOrDefault(x => x.Item.ToLower().Trim() == "Conveyance Allowance".ToLower().Trim());
            ExempCalculation ec2 = new ExempCalculation();

            ec2.BreakeDown = exempConveyanceAllowance.Item + "(" + exempConveyanceAllowance.Percentage + "%" + ")";
            ec2.Amount = (Convert.ToDecimal(exempConveyanceAllowance.Percentage) / 100) * totalBasic;
            
            var empDetailsForCAD = empDetails.Where(x => x.xpaycode == 2280).FirstOrDefault();

            if (empDetailsForCAD == null)
            {

            }
            else
            {
                ec2.Deduction = empDetailsForCAD.xamount * Convert.ToDecimal(12);
                ec2.NetOfDeduction = ec2.Amount - ec2.Deduction;

                ec2.Exempted = exempConveyanceAllowance.Limit;
                if (ec2.NetOfDeduction >= ec2.Exempted)
                {
                    ec2.TaxableAmount = ec2.NetOfDeduction - ec2.Exempted;
                }
                else
                {
                    ec2.Exempted = ec2.NetOfDeduction;
                    ec2.TaxableAmount = 0;
                }

                ecl.Add(ec2);
            }
            
            //Medical Allowance
            var exempMedicalAllowance = exemption.FirstOrDefault(x => x.Item.ToLower().Trim() == "Medical Allowance".ToLower().Trim());
            ExempCalculation ec3 = new ExempCalculation();

            ec3.BreakeDown = exempMedicalAllowance.Item + "(" + exempMedicalAllowance.Percentage + "%" + ")";
            ec3.Amount = (Convert.ToDecimal(exempMedicalAllowance.Percentage) / 100) * totalBasic;
            
            var empDetailsForMAD = empDetails.Where(x => x.xpaycode == 2291).FirstOrDefault();

            if (empDetailsForMAD == null)
            {

            }
            else
            {
                ec3.Deduction = empDetailsForMAD.xamount * Convert.ToDecimal(12);
                ec3.NetOfDeduction = ec3.Amount - ec3.Deduction;

                ec3.Exempted = exempMedicalAllowance.Limit;
                if (ec3.NetOfDeduction >= ec3.Exempted)
                {
                    ec3.TaxableAmount = ec3.NetOfDeduction - ec3.Exempted;
                }
                else
                {
                    ec3.Exempted = ec3.NetOfDeduction;
                    ec3.TaxableAmount = 0;
                }

                ecl.Add(ec3);
            }
            
            //Bonus
            ExempCalculation ec4 = new ExempCalculation();

            ec4.BreakeDown = "Bonus";
            ec4.Amount = 0;
            var empDetailsForBonus = empDetails.Where(x => x.xpaycode == 1250).FirstOrDefault();

            if (empDetailsForBonus == null)
            {

            }
            else
            {
                ec4.Amount = empDetailsForBonus.xamount * Convert.ToDecimal(12);
                ec4.Deduction = 0;
                ec4.NetOfDeduction = 0;

                ec4.Exempted = 0;
                ec4.TaxableAmount = ec4.Amount;
                ecl.Add(ec4);
            }
            
            //PF calculation
            ExempCalculation ec5 = new ExempCalculation();

            ec5.BreakeDown = "PF";
            var empDetailsForPF = empDetails.Where(x => x.xpaycode == 2300).FirstOrDefault();

            if (empDetailsForPF == null)
            {

            }
            else
            {
                ec5.Amount = empDetailsForPF.xamount * Convert.ToDecimal(12);
                ec5.Deduction = 0;
                ec5.NetOfDeduction = 0;

                ec5.Exempted = 0;
                DateTime confDate = empDetailsForPF.xconfirmationDate;
                int year = ((DateTime.Now.Month - confDate.Month) + 12 * (DateTime.Now.Year - confDate.Year)) / 12;
                if (year < 3)
                {
                    ec5.Amount = 0;
                }
                else if (year >= 3 && year < 5)
                {
                    ec5.Amount = ec5.Amount * Convert.ToDecimal(0.50);
                }
                else if (year >= 5 && year < 7)
                {
                    ec5.Amount = ec5.Amount * Convert.ToDecimal(0.75);
                }
                else
                {
                    ec5.Amount = ec5.Amount;
                }
                ec5.TaxableAmount = ec5.Amount;

                ecl.Add(ec5);
            }

            ViewBag.Exemption = ecl;
            return ecl;
        }
        public decimal CalculateLessExempted(decimal totIncome)
        {
            //ExemptionOnTotalCalculation

            //Get Employee Gener By Id

            var exempOnTotal = context.ExemtionOnTtl.FirstOrDefault();

            decimal lessExemp = totIncome;
            if (lessExemp >= exempOnTotal.Limit)
            {
                lessExemp = exempOnTotal.Limit;
            }
            ViewBag.LessExemp = lessExemp;
            decimal taxabledAmount = totIncome - lessExemp;

            return taxabledAmount;
        }
        public List<Slab> CalculateSlabList(decimal taxabledAmount)
        {
            //SlabCalculation
            List<Slab> sl = new List<Slab>();

            var slabList = context.SLabList.ToList();
            decimal rtnSlab = taxabledAmount;
            slabList.ForEach(x =>
            {
                if (rtnSlab == 0)
                {
                    return;
                }
                var slab = slabList.FirstOrDefault(y => y.Slab.ToLower().Trim() == x.Slab.ToLower().Trim());
                if (slab.MaxLimit >= rtnSlab)
                {
                    Slab s = new Slab();
                    s.SlabName = slab.Slab;
                    if (s.SlabName.ToLower().Trim() == "On Balance".ToLower().Trim())
                    {
                        s.Limit = " ";
                    }
                    else
                    {
                        s.Limit = slab.MaxLimit.ToString();
                    }
                    s.Amount = rtnSlab;
                    s.Percentage = slab.Percentage.ToString();
                    s.TaxableAmount = (Convert.ToDecimal(slab.Percentage) / 100) * rtnSlab;
                    sl.Add(s);
                    rtnSlab = 0;
                }
                else
                {
                    Slab s = new Slab();
                    s.SlabName = slab.Slab;
                    s.Limit = slab.MaxLimit.ToString();
                    s.Amount = slab.MaxLimit;
                    s.Percentage = slab.Percentage.ToString();
                    s.TaxableAmount = (Convert.ToDecimal(slab.Percentage) / 100) * Convert.ToDecimal(s.Limit);
                    sl.Add(s);
                    rtnSlab = rtnSlab - slab.MaxLimit;
                }
            });

            return sl;
        }
        public List<Investment> CalculateInvestment(decimal investmentAmount)
        {
            var rebateConditionLine = context.RebateConditionLine.ToList();
            var condition = context.RebateCondition.FirstOrDefault(x => x.Limit >= investmentAmount);
            var conditionLine = rebateConditionLine.Where(x => x.ConditionID == condition.ID).ToList();

            List<Investment> invL = new List<Investment>();
            decimal rtnSlab = investmentAmount;

            conditionLine.ForEach(x =>
            {
                if (rtnSlab == 0)
                {
                    return;
                }
                var slab = rebateConditionLine.FirstOrDefault(y => y.SlabID == x.SlabID && y.ConditionID == x.ConditionID);
                if (slab.MaxLimit >= rtnSlab)
                {
                    Investment inv = new Investment();
                    inv.InvestmentAmount = rtnSlab;
                    inv.RebatePercent = slab.Percentage;
                    inv.MaxLimit = slab.MaxLimit;
                    inv.RebateAmount = (Convert.ToDecimal(slab.Percentage) / 100) * rtnSlab;
                    invL.Add(inv);
                    rtnSlab = 0;
                }
                else
                {
                    Investment inv = new Investment();
                    inv.InvestmentAmount = slab.MaxLimit;
                    inv.RebatePercent = slab.Percentage;
                    inv.MaxLimit = slab.MaxLimit;
                    inv.RebateAmount = (Convert.ToDecimal(slab.Percentage) / 100) * inv.MaxLimit;
                    invL.Add(inv);
                    rtnSlab = rtnSlab - slab.MaxLimit;
                }
            });

            return invL;
        }

        public ActionResult SaveAitForecast(AITForecast aitForecast, List<AITForecastLine> addedItems)  
        {
            using (context)
            {
                aitForecast.ID = GenerateAitForcastId();
                string s1 = aitForecast.FinancialYear.Substring(0, 4);
                string e1 = aitForecast.FinancialYear.Substring(7, 4);
                DateTime start = Convert.ToDateTime(s1 + "-07-01");
                DateTime end = Convert.ToDateTime(e1 + "-06-30");
                aitForecast.StartDate = start;
                aitForecast.EndDate = end;

                addedItems.ForEach(x =>
                {
                    x.ID = aitForecast.ID;
                    x.EmployeeID = x.EmployeeID;
                    context.AITForecastLine.Add(x);
                });

                context.AITForecast.Add(aitForecast);

                UserLog.SaveUserLog(ref context, new UserLog(aitForecast.ID.ToString(), DateTime.Now, "AITF", ScreenAction.Save));
                //InventoryTransactionBridge.InsertFromSampleEntry(ref context, dsia, addedItems);

                context.SaveChanges();
                return Json(new { aitForecast }, JsonRequestBehavior.AllowGet); 
            }
        }

        public ActionResult GetAITForecastById(int id)  
        {

            var forcastItem = context.AITForecast.FirstOrDefault(x => x.ID == id);

            if (forcastItem != null)
            {
                var forcastLineItem = context.AITForecastLine.Where(x => x.ID == id).ToList();
                forcastLineItem.ForEach(x =>
                {
                    x.EmployeeDesignation = erml.FirstOrDefault(y => y.xemp == x.EmployeeID).xdesig;
                });
                return Json(new { forcastItem, forcastLineItem }, JsonRequestBehavior.AllowGet);    
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateForecast()
        {
            ViewBag.AssesmentYear = LoadComboBox.LoadAssesmentYear();
            ViewBag.DuringYear = LoadComboBox.LoadDuringYear();
            ViewBag.Cororation = LoadComboBox.LoadCorporation();
            ViewBag.LoadLocation = LoadComboBox.LoadLocation();

            return View();
        }

        public ActionResult GetExistingForcast(int id)
        {
            isUpdate = true;

            GetFinancialYear();

            var existForcast = (from f in context.AITForecast 
                                join fl in context.AITForecastLine on f.ID equals fl.ID
                                where (f.FinancialYear == financialY) && (fl.EmployeeID == id)
                                select fl).FirstOrDefault();

            if (existForcast != null)
            {
                IndividualEmployeeTaxCalculation iETC = new IndividualEmployeeTaxCalculation();
                iETC.FinancialYear = financialY;
                iETC.EmployeeId = existForcast.EmployeeID;
                iETC.EmployeeDesignation = FindObjectById.GetERMTransection().FirstOrDefault(x => x.xemp == iETC.EmployeeId).xdesig;
                iETC.WIth_Without = existForcast.WIth_Without;
                iETC.MonthlyAIT = existForcast.MonthlyAIT;

                Type = iETC.WIth_Without;

                return Json(new { iETC = iETC, iETC2 = GetEmployeeForecastDetails(id, financialY) }, JsonRequestBehavior.AllowGet); 
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateForecastLine(IndividualEmployeeTaxCalculation addedItems1)
        {
            var forcastExist = (from f in context.AITForecast
                                join fl in context.AITForecastLine on f.ID equals fl.ID
                                where (f.FinancialYear == addedItems1.FinancialYear) && (fl.EmployeeID == addedItems1.EmployeeId)
                                select fl).FirstOrDefault();

            if (forcastExist != null)
            {
                context.AITForecastLine.Remove(forcastExist);

                AITForecastLine fl = new AITForecastLine();
                fl.ID = forcastExist.ID;
                fl.EmployeeID = addedItems1.EmployeeId;
                fl.WIth_Without = addedItems1.WIth_Without;
                fl.MonthlyAIT = addedItems1.MonthlyAIT;

                context.AITForecastLine.Add(fl);
                UserLog.SaveUserLog(ref context, new UserLog(fl.ID.ToString(), DateTime.Now, "AITF Update", ScreenAction.Save));
                context.SaveChanges();
                return Json(fl.ID, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public string GetFinancialYear()
        {
            int year = DateTime.Now.Year - 1;
            int month = DateTime.Now.Month;
            int year2 = 0;

            if (month > 6)
            {
                year2 = year + 1;
                financialY = year + " - " + year2;
            }
            else
            {
                year2 = year - 1;
                financialY = year2 + " - " + year;
            }

            return financialY;
        }


        public int GenerateAitForcastId()   
        {
            var aITForecast = context.AITForecast.ToList().OrderBy(x => x.ID).LastOrDefault();
            if (aITForecast != null)
            {
                return aITForecast.ID + 1;
            }
            else
            {
                return 1;
            }
        }
    }
}