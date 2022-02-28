using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class VehicleExpenseReport
    {
        public string Account { get; set; }
        public string Vehicle { get; set; }
        public decimal Amount { get; set; }
        public string Branch { get; set; }
    }
}