using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class MRBalanceSummaryReport
    {
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public string Empdesig { get; set; }
        public decimal BalAmount { get; set; }
    }
}