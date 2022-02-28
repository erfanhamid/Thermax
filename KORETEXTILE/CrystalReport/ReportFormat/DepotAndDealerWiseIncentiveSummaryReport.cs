using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class DepotAndDealerWiseIncentiveSummaryReport
    {
        public string Depot { get; set; }
        public int DealerID { get; set; }
        public string DealerName { get; set; }
        public decimal Balance { get; set; }
    }
}