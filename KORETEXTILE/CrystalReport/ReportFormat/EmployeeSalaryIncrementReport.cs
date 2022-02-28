using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class EmployeeSalaryIncrementReport
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string CompanyName { get; set; }
        public decimal IncrementAmount { get; set; }
        public decimal Gross { get; set; }
        public DateTime ApplicableDate { get; set; }
    }
}