using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.TaxCalculator
{
    public class GrossSalary
    {
        public string Month { get;set;}
        public decimal MonthlyGross { get; set; }
        public int NoOfMonths { get; set; }
        public decimal TotalGross { get; set; }
    }

    public class MonthRange
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }  
        public decimal MonthlyGross { get; set; }
    }
}