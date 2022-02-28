using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class VehicleExpenseDetailsReport
    {
        public DateTime Date { get; set; }
        public int DocNo { get; set; }
        public string DocType { get; set; }
        public string Account { get; set; }
        public string Branch { get; set; }
        public decimal Amount { get; set; }
    }
}