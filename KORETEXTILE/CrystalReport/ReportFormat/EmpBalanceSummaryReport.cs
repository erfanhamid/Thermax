using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class EmpBalanceSummaryReport
    {
        public int EmployeeId { set; get; }
        public string EmpName { set; get; }
        public string DesigName { set; get; }
        public decimal Amount { set; get; }
    }
}