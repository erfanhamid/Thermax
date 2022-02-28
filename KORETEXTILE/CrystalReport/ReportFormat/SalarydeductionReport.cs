using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SalarydeductionReport
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public decimal AdvanceAdjustment { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal ProvidentFund { get; set; }
        public decimal OtherAdjustment { get; set; }
        public decimal MCAdjustment { get; set; }
        public string BranchName { get; set; }
    }
}