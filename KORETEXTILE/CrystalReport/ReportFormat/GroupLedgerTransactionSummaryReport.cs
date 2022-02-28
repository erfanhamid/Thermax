using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class GroupLedgerTransactionSummaryReport
    {
        public string groupname { get; set; }
        public string accountname { get; set; }
        public decimal OB { get; set; }
        public decimal NetIncrease { get; set; }
        public decimal Netdecrease { get; set; }
        public decimal closing { get; set; }

    }
}