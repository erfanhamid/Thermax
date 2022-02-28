using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.TaxCalculator
{
    public class IndividualEmployeeTaxCalculation
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeDesignation {get;set;}
        public decimal TaxPayable { get; set; }
        public decimal TaxPayWithoutInv { get; set; }   
        public decimal MonthlyAitWithoutInv { get; set; }
        public decimal MonthlyAitWithInv { get; set; }
        public string WIth_Without { get; set; }
        public decimal MonthlyAIT { get; set; }
        public decimal Amount { get; set; }
        public decimal TotTaxWithInv { get; set; }
        public decimal TotTAxWithoutInv { get; set; }
        public string FinancialYear { get; set; }
    }
}