using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class AccountsLedgerReport
    {
        public string Name { set; get; }
        public DateTime TrDate { set; get; }
        public string DocType { set; get; }
        public int DocID { set; get; }
        public string SubLedger { set; get; }
        public string Particulars { set; get; }
        public decimal Amount { set; get; }
        public int TrType { set; get;}
        public string rootAccountType { set; get; }
    }
}