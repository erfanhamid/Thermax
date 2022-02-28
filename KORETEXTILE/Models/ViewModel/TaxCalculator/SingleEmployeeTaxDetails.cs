using BEEERP.Models.TaxCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.TaxCalculator
{
    public class SingleEmployeeTaxDetails
    {
        public string EmployeeName { get; set; }
        public string Corporation { get; set; }
        public string CompanyAddress { get; set; }
        public string AssesmentYear { get; set; }
        public int EmployeeId { get; set; }
        public string FinancialYear { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
    }
}