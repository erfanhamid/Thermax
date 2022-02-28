using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class EmployeeITBalanceSummary
    {
        public int EmployeeID { set; get; }
        public string Name { set; get; }
        public string BrnachName { set; get; }
        public decimal Amount { set; get; }
    }
}