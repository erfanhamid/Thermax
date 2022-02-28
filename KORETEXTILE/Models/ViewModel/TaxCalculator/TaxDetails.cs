using BEEERP.Models.TaxCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.TaxCalculator
{
    public class TaxDetails
    {
        public string EmployeeName { set; get; }
        public string Corporation { set; get; }
        public string CompanyAddress { set; get; }
        public string AssesmentYear { set; get; }
        public int EmployeeId { set; get; }
        public string Location { set; get; }
        public string FinancialYear { set; get; }
        public decimal TotGross { get; set; }
        public decimal TotIncome { get; set; }
        public decimal LessExempted { set; get; }
        public decimal TaxableIncome { set; get; }
        public decimal IncomeTaxPayableWithoutInvestment { set; get; }
        public decimal InvestmentAmount { set; get; }
        public decimal TotalRebateOnInvestment { get; set; }
        public decimal IncometaxPayableWithInvestment { set; get; }
        public decimal TotChallan { get; set; }
        public decimal NetTaxWithoutInvestment { set; get; }
        public decimal NetTaxWithInvestment { set; get; }
        public string Type { get; set; }
        public List<GrossSalary> GrossSalarie { get; set; }
        public List<ExempCalculation> ExempCalculation { get; set; }
        public List<Slab> Slab { get; set; }
        public List<Investment> Investment { get; set; }
        public List<ChallanList> ChallanList { get; set; }
        
    }
}