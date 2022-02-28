using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SingleDepotAccGroupSummaryReport
    {
        public string DepotName { get; set; }
        public string LedgerGroup { get; set; }
        public decimal TAmount { get; set; }
    }
}