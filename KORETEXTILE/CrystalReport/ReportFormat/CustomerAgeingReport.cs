using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class CustomerAgeingReport
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public decimal ThirtyDaysAmount { get; set; }
        public decimal SixtyDaysAmount { get; set; }
        public decimal NinetyDaysAmount { get; set; }
        public decimal AboveNinetyDaysAmount { get; set; }
    }
}