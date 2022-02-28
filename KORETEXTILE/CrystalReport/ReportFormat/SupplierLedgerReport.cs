using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SupplierLedgerReport
    {
        public int DocNo { get; set; }
        public string DocType { get; set; }
        public string AccountName { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string RefNo { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; }

    }
}