using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class DepotWiseCostGroupSummaryReport
    {
        public string DEPOTName { get; set; }
        public string CostGroup { get; set; }
        public decimal TAmount { get; set; }
    }
}