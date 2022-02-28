using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class EmployeeTaxBalanceLedgerReport
    {
        public int DocNO { get; set; }
        public DateTime TDate { get; set; }
        public string DocumentType { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public string RefNo { get; set; }
    }
}