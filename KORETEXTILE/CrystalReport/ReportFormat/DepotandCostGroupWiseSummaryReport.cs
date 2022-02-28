using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class DepotandCostGroupWiseSummaryReport
    {
        public string DEPOTName { get; set; }
        public string CostGroup { get; set; }
        public string LedgerName { get; set; }
        public decimal TAmount { get; set; }
    }
}