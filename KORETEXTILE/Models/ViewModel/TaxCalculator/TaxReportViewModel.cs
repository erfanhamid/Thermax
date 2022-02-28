using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.TaxCalculator
{
    public class TaxReportViewModel
    {
        public int EmployeeID { get; set; }
        public string Location { get; set; }
        public string FinancialYear { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}