using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SalarySheetSummaryReport
    {
        public string CompanyName { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int Months { get; set; }
        public decimal Amount { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal EarnedSalary { get; set; }
        public decimal TotalAddition { get; set; }
        public decimal TotalDeduction { get; set; }
        public decimal NetPayable { get; set; }
        public string MonthName { get; set; }

    }
}