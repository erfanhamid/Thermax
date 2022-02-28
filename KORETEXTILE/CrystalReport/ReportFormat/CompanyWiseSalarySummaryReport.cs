using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class CompanyWiseSalarySummaryReport
    {
        public string CompanyName { get; set; }
        public double GrossSalary { get; set; }
        public double EarnedSalary { get; set; }
        public double TotalAddition { get; set; }
        public double TotalDeduct { get; set; }
        public double PayableSalary { get; set; }
        public double Total{ get; set; }
    }
}