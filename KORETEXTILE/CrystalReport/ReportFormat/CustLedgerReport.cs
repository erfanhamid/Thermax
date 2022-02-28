using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class CustLedgerReport
    {
        public int CustomerId { set; get; }
        public DateTime TDate { set; get; }
        public string CustomerName { set; get; }
        public string MobileNo { set; get; }
        public string BillTo { set; get; }
        public string DocumentType { set; get; }
        public string AccName { set; get; }
        public string TrDesc { set; get; }
        public string RefNo { set; get; }
        public decimal Amount { set; get; }
        public int DocNo { set; get; } 
        public decimal OpenBal { set; get; }
    }
}