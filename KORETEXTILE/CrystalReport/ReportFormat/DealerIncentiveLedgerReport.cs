using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class DealerIncentiveLedgerReport
    {
        public DateTime Date { get; set; }
        public int DealerID { get; set; }
        public string DealerName { get; set; }
        public string AccountHead { get; set; }
        public string DocType { get; set; }
        public int DocNo { get; set; }
        public string Description { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; }
    }
}