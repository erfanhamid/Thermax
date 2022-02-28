using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class ItemLedgerReport
    {
        public int DocNo { set; get; }
        public string ItemName { set; get; }
        public DateTime TDate { set; get; }
        public string DocType { set; get; }
        public double RcvQty { set; get; }
        public double IssQty { set; get; }
        public decimal Rate { get; set; }
    }
}