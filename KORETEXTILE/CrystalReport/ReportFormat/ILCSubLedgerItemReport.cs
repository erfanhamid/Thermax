using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class ILCSubLedgerItemReport
    {
        public DateTime Date { get; set; }
        public String Account { get; set; }
        public int DocNo { get; set; }
        public string DocType { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public decimal Amount { get; set; }
        public string Name { get; set; }    
    }
}