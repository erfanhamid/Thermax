using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class RetailerRentLedgerReport
    {
        public int RetailerID { get; set; }
        public string RetailerName { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string BrnachName { get; set; }
        public int DocNo { get; set; }
        public string DocType { get; set; }
        public string AccountName { get; set; }
        public DateTime TDate { get; set; }
        public string TrDesc { get; set; }
        public string RefNo { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; }
    }
}