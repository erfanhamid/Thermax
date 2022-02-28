using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.TaxCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Log;
using BEEERP.Models.ViewModel.TaxCalculator;
using Rotativa;

namespace BEEERP.Controllers.TaxCalculator
{
    [ShowNotification]
    public class TaxCalculatorController : Controller
    {
        List<ERMTransection> erml = FindObjectById.GetERMTransection();
        BEEContext context = new BEEContext();
        decimal lessExempF = 0;
        TaxDetails taxD = new TaxDetails();
        TaxCertificate taxC = new TaxCertificate();


        // GET: TaxCalculator
        public ActionResult TaxCalculator() 
        {
            ViewBag.AssesmentYear = LoadComboBox.LoadAssesmentYear();
            ViewBag.DuringYear = LoadComboBox.LoadDuringYear();
            ViewBag.Cororation = LoadComboBox.LoadCorporation();
            ViewBag.LoadLocation = LoadComboBox.LoadLocation();
            //ViewBag.EmployeeDetails = FindObjectById.GetERMTransection();
            return View();
        }

        public ActionResult GetEmployees(string duringYear, string location, int corpuration, string assesmentY)
        {
            //duringYear = "2018 - 2019";
            string s1 = duringYear.Substring(0, 4);
            string e1 = duringYear.Substring(7, 4);
            DateTime start = Convert.ToDateTime(s1 + "-07-01");
            DateTime end = Convert.ToDateTime(e1 + "-06-30");
            var data = erml.Where(x => x.xprloc == location && x.xpaydate >= start && x.xpaydate <= end && x.xprorg == corpuration).OrderBy(y => y.xemp).ToList();
            var data1 = data.GroupBy(y => y.xemp).Distinct().ToList();
            var tax = (from t in context.TaxCalculation
                       join tl in context.TaxCalculationLine on t.ID equals tl.ID
                       where t.Location == location && t.Corporation == corpuration && t.FinancialYear == assesmentY
                       select tl).ToList();

            List<IndividualEmployeeTaxCalculation> iETCl = new List<IndividualEmployeeTaxCalculation>();
            data1.ForEach(x =>
            {
                IndividualEmployeeTaxCalculation iETC = new IndividualEmployeeTaxCalculation();
                iETC = GetEmployeeTAxDetails(x.FirstOrDefault().xemp, duringYear, assesmentY);
                iETC.EmployeeDesignation = x.FirstOrDefault().xdesig;
                iETC.WIth_Without =  (from f in context.AITForecast join 
                                      fl in context.AITForecastLine on f.ID equals fl.ID
                                      where (f.FinancialYear == duringYear) && (fl.EmployeeID == iETC.EmployeeId)
                                      select fl.WIth_Without).FirstOrDefault();
                if (iETC.TaxPayWithoutInv != 0)
                {
                  var taxExist = tax.FirstOrDefault(y => y.EmployeeID == iETC.EmployeeId);
                    if (taxExist == null)
                    {
                        iETCl.Add(iETC);
                    }
                }
            });
            return Json(iETCl, JsonRequestBehavior.AllowGet);    
        }

        public ActionResult GetEmployeeDetails(int empId, string duringYear, string assesmentYear, string type)
        {
            ViewBag.Type = type;
            GetEmployeeTAxDetails(empId, duringYear, assesmentYear);

            return View();
        }

        public ActionResult SaveTaxStatement(TaxCalculation taxCalculation, List<TaxCalculationLine> newItem)  
        {
            using (context)
            {
                taxCalculation.ID = GenerateTaxSatatementId();

                newItem.ForEach(x =>
                {
                    x.ID = taxCalculation.ID;
                    context.TaxCalculationLine.Add(x);

                    TAXBalanceCalculation taxb = new TAXBalanceCalculation();
                    taxb.EmployeeID = x.EmployeeID;
                    taxb.DocNO = taxCalculation.ID;
                    taxb.Date = taxCalculation.Date;
                    taxb.DocumentType = "Tax";
                    taxb.Description = "";
                    taxb.Amount = x.TaxAmount;
                    taxb.TBID = taxCalculation.Corporation;
                    context.TAXBalanceCalculation.Add(taxb);
                });

                context.TaxCalculation.Add(taxCalculation);
                

                UserLog.SaveUserLog(ref context, new UserLog(taxCalculation.ID.ToString(), DateTime.Now, "TAX", ScreenAction.Save));

                context.SaveChanges();
                return Json(new { taxCalculation }, JsonRequestBehavior.AllowGet);  
            }
        }

        public ActionResult GetTaxStatementById(int id) 
        {

            var taxItem = context.TaxCalculation.FirstOrDefault(x => x.ID == id);

            if (taxItem != null)
            {
                var taxLineItem = context.TaxCalculationLine.Where(x => x.ID == id).ToList();
                taxLineItem.ForEach(x =>
                {
                    x.EmployeeDesignation = erml.FirstOrDefault(y => y.xemp == x.EmployeeID).xdesig;
                });
                return Json(new { taxItem, taxLineItem }, JsonRequestBehavior.AllowGet);    
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        //public ActionResult GetLocationWiseTaxDetails(TaxReportViewModel model) 
        //{
            
        //}

        public ActionResult GetSingleEmployeeTaxPrintPreview(TaxReportViewModel model)
        {
            if(model.EmployeeID!=0)
            {
                string s1 = model.FinancialYear.Substring(0, 4);
                string e1 = model.FinancialYear.Substring(7, 4);
                DateTime start = Convert.ToDateTime(s1 + "-07-01");
                DateTime end = Convert.ToDateTime(e1 + "-06-30");
                var data = erml.Where(x => x.xprloc == model.Location && x.xpaydate >= start && x.xpaydate <= end&&x.xemp==model.EmployeeID).OrderBy(y => y.xemp).ToList();

                if(data.Count() <= 0)
                {
                    ViewBag.Message = "No Employee Found";
                    return new ViewAsPdf(new TaxRepotVModel());
                }
                else
                {
                    var data1 = data.GroupBy(y => y.xemp).Distinct().ToList();

                    List<TaxReportEmployee> tREL = new List<TaxReportEmployee>();
                    data1.ForEach(x =>
                    {
                        model.EmployeeID = x.FirstOrDefault().xemp;
                        TaxReportEmployee tRE = CalculateSingleEmployeeTaxPrintPreview(model);
                        if (tRE.TaxDetails.TaxableIncome != 0)
                        {
                            tREL.Add(tRE);
                        }
                    });

                    TaxRepotVModel vModel = new TaxRepotVModel();
                    vModel.TaxReportEmployee = tREL;

                    return new ViewAsPdf(vModel);
                }
                
            }
            else
            {
                string s1 = model.FinancialYear.Substring(0, 4);
                string e1 = model.FinancialYear.Substring(7, 4);
                DateTime start = Convert.ToDateTime(s1 + "-07-01");
                DateTime end = Convert.ToDateTime(e1 + "-06-30");
                var data = erml.Where(x => x.xprloc == model.Location && x.xpaydate >= start && x.xpaydate <= end).OrderBy(y => y.xemp).ToList();
                var data1 = data.GroupBy(y => y.xemp).Distinct().ToList();

                List<TaxReportEmployee> tREL = new List<TaxReportEmployee>();
                data1.ForEach(x =>
                {
                    model.EmployeeID = x.FirstOrDefault().xemp;
                    TaxReportEmployee tRE = CalculateSingleEmployeeTaxPrintPreview(model);
                    if (tRE.TaxDetails.TaxableIncome != 0)
                    {
                        tREL.Add(tRE);
                    }
                });

                TaxRepotVModel vModel = new TaxRepotVModel();
                vModel.TaxReportEmployee = tREL;

                return new ViewAsPdf(vModel);
            }
            
        }

        public TaxReportEmployee CalculateSingleEmployeeTaxPrintPreview(TaxReportViewModel model)
        {
            int empId = model.EmployeeID;
            string duringYear = model.FinancialYear;

            string s1 = duringYear.Substring(0, 4);
            string e1 = duringYear.Substring(7, 4);
            DateTime start = Convert.ToDateTime(s1 + "-07-01");
            DateTime end = Convert.ToDateTime(e1 + "-06-30");

            var tax = (from t in context.TaxCalculation join 
                             tl in context.TaxCalculationLine on t.ID equals tl.ID
                             where (t.FinancialYear == model.FinancialYear) && (t.Location == model.Location) && (tl.EmployeeID == model.EmployeeID)
                             select new { Amount = tl.TaxAmount, Type = tl.WIth_Without }).FirstOrDefault();

            var empDetails = erml.Where(x => x.xemp == empId && x.xpaydate >= start && x.xpaydate <= end).OrderBy(y => y.xpaydate).ToList();

            if(empDetails.Count() <= 0)
            {
                ViewBag.Message = "No Employee Found";
                return new TaxReportEmployee();
            }
            else
            {
                ViewBag.Message = "";
                TaxDetails td = new TaxDetails();

                int trustyId = empDetails.FirstOrDefault().xprorg;

                td.EmployeeName = "";
                td.Corporation = context.TrusteeBoard.FirstOrDefault(x => x.TrusteeNo == trustyId).TrusteeName;
                td.CompanyAddress = "";
                td.AssesmentYear = Convert.ToInt32(s1 + 1) + " - " + Convert.ToInt32(e1 + 1);
                td.EmployeeId = empId;
                td.FinancialYear = duringYear;
                td.Location = model.Location;
                if (tax == null)
                {
                    td.Type = "";
                }
                else
                {
                    td.Type = tax.Type;
                }

                td.GrossSalarie = CalculateGrossSalary(empDetails);
                decimal totGross = 0;
                td.GrossSalarie.ForEach(x => {
                    totGross += x.TotalGross;
                });
                td.TotGross = totGross;

                var ecl = CalculateExemptedAmount(empDetails);

                td.ExempCalculation = ecl;

                decimal totIncome = 0;
                ecl.ForEach(x =>
                {
                    totIncome += x.TaxableAmount;
                });
                td.TotIncome = totIncome;

                td.TaxableIncome = CalculateLessExempted(totIncome);
                td.LessExempted = lessExempF;

                td.Slab = CalculateSlabList(td.TaxableIncome);

                decimal taxWithoutInvest = 0;
                td.Slab.ForEach(x =>
                {
                    taxWithoutInvest += x.TaxableAmount;
                });
                td.IncomeTaxPayableWithoutInvestment = taxWithoutInvest;

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
                td.InvestmentAmount = ((totIncome - pfAmount) * Convert.ToDecimal(invPercentage)) / 100;

                td.Investment = CalculateInvestment(td.InvestmentAmount);

                decimal totRebate = 0;
                td.Investment.ForEach(x =>
                {
                    totRebate += x.RebateAmount;
                });

                td.TotalRebateOnInvestment = totRebate;

                td.IncometaxPayableWithInvestment = taxWithoutInvest - totRebate;

                td.ChallanList = GetTotalChallan(empId, duringYear);

                decimal TotChallanAmount = 0;
                td.ChallanList.ForEach(x =>
                {
                    TotChallanAmount += x.Amount;
                });

                td.TotChallan = TotChallanAmount;

                td.NetTaxWithoutInvestment = taxWithoutInvest - TotChallanAmount;
                td.NetTaxWithInvestment = td.NetTaxWithoutInvestment - totRebate;

                //taxD = td;

                //for Tax Certificate

                TaxCertificate tc = new TaxCertificate();
                tc.Name = "";
                tc.FinanacialYear = model.FinancialYear;
                tc.DescriptionOne = "This is Certify that Mr./Ms. " + tc.Name + " is a Faculty of Scholastica Limited. He/She has drawn his/her salary for the Financial Year " + tc.FinanacialYear + " as follows.";
                tc.BasicSalary = ecl.FirstOrDefault(x => x.BreakeDown == "Basic Salary(2/3 of Gross)").TaxableAmount;

                var exemption = context.ExemtionList.ToList();
                var exempHouseRent = exemption.FirstOrDefault(x => x.Item.ToLower().Trim() == "House Rent".ToLower().Trim());
                var houseRent = ecl.FirstOrDefault(x => x.BreakeDown == exempHouseRent.Item + "(" + exempHouseRent.Percentage + "%" + ")");
                tc.HouseRent = houseRent.NetOfDeduction;
                tc.ExemptedHouseRent = houseRent.Exempted;

                var exempConveyanceAllowance = exemption.FirstOrDefault(x => x.Item.ToLower().Trim() == "Conveyance Allowance".ToLower().Trim());
                var conveyanceAllowance = ecl.FirstOrDefault(x => x.BreakeDown == exempConveyanceAllowance.Item + "(" + exempConveyanceAllowance.Percentage + "%" + ")");
                tc.ConveyanceAllowance = conveyanceAllowance.NetOfDeduction;
                tc.ExemptedCA = conveyanceAllowance.Exempted;

                var exempMedicalAllowance = exemption.FirstOrDefault(x => x.Item.ToLower().Trim() == "Medical Allowance".ToLower().Trim());
                var medicalAllowence = ecl.FirstOrDefault(x => x.BreakeDown == exempMedicalAllowance.Item + "(" + exempMedicalAllowance.Percentage + "%" + ")");
                tc.MedicalAllowance = medicalAllowence.NetOfDeduction;
                tc.ExemptedMA = medicalAllowence.Exempted;


                tc.Bonus = ecl.FirstOrDefault(x => x.BreakeDown == "Bonus").TaxableAmount;
                tc.SpecialAllowance = 0;
                tc.PFContribution = ecl.FirstOrDefault(x => x.BreakeDown == "PF").TaxableAmount;

                if (tax == null)
                {
                    tc.AmountOne = 0;
                    tc.TaxType = "";
                }
                else
                {
                    tc.AmountOne = tax.Amount;
                    tc.TaxType = tax.Type;
                }

                var challan = (from c in context.AITChallan
                               join cl in context.AITChallanLine on c.ID equals cl.ID
                               where (c.FinancialYear == model.FinancialYear) && (c.Location == model.Location) && (cl.EmployeeID == model.EmployeeID)
                               select new { ChalanNo = c.ChallanNo, DescriptionOrBank = c.Description, Date = c.Date, Amount = cl.Amount }).ToList();

                List<ChallanInfo> ch = new List<ChallanInfo>();
                challan.ForEach(x =>
                {
                    ChallanInfo ci = new ChallanInfo();
                    ci.Amount = x.Amount;
                    ci.ChalanNo = x.ChalanNo;
                    ci.DescriptionOrBank = x.DescriptionOrBank;
                    ci.Date = x.Date.ToString("dd-MM-yyyy");
                    ch.Add(ci);
                });

                tc.Challans = ch;
                tc.DescriptionTwo = "Tax is Tk. " + String.Format("{0:0.00}", tc.AmountOne) + " /- on the basic of salaries, the following amount has been deducted at source and deposited to the ";
                tc.Challans.ForEach(x =>
                {
                    tc.DescriptionTwo += x.DescriptionOrBank + " by, Challan No " + x.ChalanNo + ", dated " + x.Date + ", Tk. " + String.Format("{0:0.00}", x.Amount) + ".";

                });

                //ViewBag.TaxCertificate = tc;

                //taxC = tc;

                TaxReportEmployee tre = new TaxReportEmployee();
                tre.TaxDetails = td;
                tre.TaxCertificates = tc;

                return tre;
            }
        }


        public IndividualEmployeeTaxCalculation GetEmployeeTAxDetails(int empId, string duringYear, string assesmentYear)
        {
            string s1 = duringYear.Substring(0, 4);
            string e1 = duringYear.Substring(7, 4);
            DateTime start = Convert.ToDateTime(s1 + "-07-01");
            DateTime end = Convert.ToDateTime(e1 + "-06-30");


            var empDetails = erml.Where(x => x.xemp == empId && x.xpaydate >= start && x.xpaydate <= end).OrderBy(y => y.xpaydate).ToList();

            CalculateGrossSalary(empDetails);


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

            var challanList = GetTotalChallan(empId, duringYear);
            ViewBag.ChallanList = challanList;

            decimal TotChallanAmount = 0; 
            challanList.ForEach(x =>
            {
                TotChallanAmount += x.Amount;
            });

            ViewBag.TotChallan = TotChallanAmount;
            decimal taxPayableWithoutInv = taxWithoutInvest - TotChallanAmount;
            decimal taxPayWithInv = taxPayableWithoutInv - totRebate;

            ViewBag.TaxWithoutInv = taxPayableWithoutInv;
            ViewBag.TaxWithInv = taxPayWithInv;

            IndividualEmployeeTaxCalculation iETC = new IndividualEmployeeTaxCalculation();
            iETC.EmployeeId = empId;
            iETC.TaxPayWithoutInv = taxPayableWithoutInv;
            iETC.TaxPayable = taxPayWithInv;
            iETC.TotTaxWithInv = taxWithoutInvest - totRebate;
            iETC.TotTAxWithoutInv = taxWithoutInvest;

            return iETC;

        }

        public List<GrossSalary> CalculateGrossSalary(List<ERMTransection> empDetails)
        {
            //calculate Gross Salary
            empDetails = empDetails.Where(x => x.xpaycode == 1000).ToList();
            List<GrossSalary> gsL = new List<GrossSalary>();
            GrossSalary gs2 = new GrossSalary();
            List<MonthRange> mrl = new List<MonthRange>();
            MonthRange mr2 = new MonthRange();
            int count = 0;
            if(empDetails.Count == 0)
            {
                return new List<GrossSalary>();
            }
            else
            {
                DateTime startDate = empDetails.First().xpaydate;
                decimal firstBasic = empDetails.First().xbasic;

                for (int i = 0; i < empDetails.Count(); i++)
                {
                    MonthRange mr = new MonthRange();
                    GrossSalary gs = new GrossSalary();

                    if (firstBasic != empDetails[i].xbasic)
                    {
                        gs.MonthlyGross = firstBasic * Convert.ToDecimal(1.5);
                        gs.NoOfMonths = count;
                        gs.TotalGross = gs.MonthlyGross * count;
                        gsL.Add(gs);
                        count = 1;
                        firstBasic = empDetails[i].xbasic;

                        mr.StartDate = startDate;
                        mr.EndDate = empDetails[i - 1].xpaydate;
                        mr.MonthlyGross = gs.MonthlyGross;
                        mrl.Add(mr);
                        startDate = empDetails[i].xpaydate;
                    }
                    else
                    {
                        count++;
                        gs2.MonthlyGross = empDetails[i].xbasic * Convert.ToDecimal(1.5);
                        gs2.NoOfMonths = count;
                        gs2.TotalGross = gs2.MonthlyGross * count;
                        firstBasic = empDetails[i].xbasic;

                        mr2.StartDate = startDate;
                        mr2.EndDate = empDetails[i].xpaydate;
                        mr2.MonthlyGross = gs2.MonthlyGross;
                        continue;
                    }
                }

                mrl.Add(mr2);
                gsL.Add(gs2);
                ViewBag.GrossSalary = gsL;
                decimal totGross = 0;
                gsL.ForEach(x =>
                {
                    var mr = mrl.FirstOrDefault(y => y.MonthlyGross == x.MonthlyGross);
                    string sMonth = mr.StartDate.ToString("MMM");
                    string sYear = mr.StartDate.Year.ToString();
                    string eMonth = mr.EndDate.ToString("MMM");
                    string eYear = mr.EndDate.Year.ToString();
                    x.Month = sMonth + " " + sYear + " - " + eMonth + " " + eYear;
                    totGross += x.TotalGross;
                });
                ViewBag.TotGross = totGross;
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
            var empDetailsForBasic = empDetails.Where(x => x.xpaycode == 1000).ToList();
            empDetailsForBasic.ForEach(x =>
            {
                totalBasic += x.xbasic;
            });
            ec.Amount = totalBasic;

            decimal basicDeduction = 0;
            var empDetailsForBasicD = empDetails.Where(x => x.xpaycode == 2100 || x.xpaycode == 2250).ToList();
            empDetailsForBasicD.ForEach(x =>
            {
                basicDeduction += x.xamount;
            });
            ec.Deduction = basicDeduction;
            ec.NetOfDeduction = totalBasic - ec.Deduction;
            ec.TaxableAmount = ec.NetOfDeduction;
            ecl.Add(ec);

            //for House Rent
            var exempHouseRent = exemption.FirstOrDefault(x => x.Item.ToLower().Trim() == "House Rent".ToLower().Trim());
            ExempCalculation ec1 = new ExempCalculation();

            ec1.BreakeDown = exempHouseRent.Item + "(" + exempHouseRent.Percentage + "%" + ")";
            ec1.Amount = (Convert.ToDecimal(exempHouseRent.Percentage) / 100) * totalBasic;

            decimal hrDeduction = 0;
            var empDetailsForHRD = empDetails.Where(x => x.xpaycode == 2270).ToList();
            empDetailsForHRD.ForEach(x =>
            {
                hrDeduction += x.xamount;
            });
            ec1.Deduction = hrDeduction;
            ec1.NetOfDeduction = ec1.Amount - hrDeduction;  
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

            //ConveyanceAllowance
            var exempConveyanceAllowance = exemption.FirstOrDefault(x => x.Item.ToLower().Trim() == "Conveyance Allowance".ToLower().Trim());
            ExempCalculation ec2 = new ExempCalculation();

            ec2.BreakeDown = exempConveyanceAllowance.Item + "(" + exempConveyanceAllowance.Percentage + "%" + ")";
            ec2.Amount = (Convert.ToDecimal(exempConveyanceAllowance.Percentage) / 100) * totalBasic;

            decimal caDeduction = 0;
            var empDetailsForCAD = empDetails.Where(x => x.xpaycode == 2280).ToList();
            empDetailsForCAD.ForEach(x =>
            {
                caDeduction += x.xamount;
            });
            ec2.Deduction = caDeduction;
            ec2.NetOfDeduction = ec2.Amount - caDeduction;

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

            //Medical Allowance
            var exempMedicalAllowance = exemption.FirstOrDefault(x => x.Item.ToLower().Trim() == "Medical Allowance".ToLower().Trim());
            ExempCalculation ec3 = new ExempCalculation();

            ec3.BreakeDown = exempMedicalAllowance.Item + "(" + exempMedicalAllowance.Percentage + "%" + ")";
            ec3.Amount = (Convert.ToDecimal(exempMedicalAllowance.Percentage) / 100) * totalBasic;

            decimal maDeduction = 0;
            var empDetailsForMAD = empDetails.Where(x => x.xpaycode == 2291).ToList();
            empDetailsForMAD.ForEach(x =>
            {
                maDeduction += x.xamount;
            });
            ec3.Deduction = maDeduction;
            ec3.NetOfDeduction = ec3.Amount - maDeduction;

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

            //Bonus
            ExempCalculation ec4 = new ExempCalculation();

            ec4.BreakeDown = "Bonus";
            ec4.Amount = 0;
            var empDetailsForBonus = empDetails.Where(x => x.xpaycode == 1250).ToList();
            empDetailsForBonus.ForEach(x =>
            {
                ec4.Amount += x.xamount;
            });
            ec4.Deduction = 0;
            ec4.NetOfDeduction = 0;

            ec4.Exempted = 0;
            ec4.TaxableAmount = ec4.Amount;
            ecl.Add(ec4);

            //PF calculation
            ExempCalculation ec5 = new ExempCalculation();

            ec5.BreakeDown = "PF";
            var empDetailsForPF = empDetails.Where(x => x.xpaycode == 2300).ToList();

            if (empDetailsForPF.Count == 0)
            {
                
            }
            else
            {
                empDetailsForPF.ForEach(x =>
                {
                    ec5.Amount += x.xamount;
                });
                ec5.Deduction = 0;
                ec5.NetOfDeduction = 0;

                ec5.Exempted = 0;
                DateTime confDate = empDetailsForPF.First().xconfirmationDate;
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
            lessExempF = lessExemp;
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
        public List<ChallanList> GetTotalChallan(int empId, string duringYear)
        {
            var challan = (from c in context.AITChallan
                           join cl in context.AITChallanLine on c.ID equals cl.ID
                           where (c.FinancialYear == duringYear) && (cl.EmployeeID == empId)
                           select new { ChallanNo = c.ChallanNo, Date = c.Date, Amount = cl.Amount }).ToList();

            List<ChallanList> challanLists = new List<ChallanList>();
            challan.ForEach(x =>
            {
                ChallanList cl = new ChallanList();
                cl.ChallanNo = x.ChallanNo;
                cl.Date = x.Date.ToString("dd-MM-yyyy");
                cl.Amount = x.Amount;
                challanLists.Add(cl);
            });

            return challanLists;
        }

        public int GenerateTaxSatatementId()    
        {
            var tax = context.TaxCalculation.ToList().OrderBy(x => x.ID).LastOrDefault();
            if (tax != null)
            {
                return tax.ID + 1;
            }
            else
            {
                return 1;
            }
        }

    }
}