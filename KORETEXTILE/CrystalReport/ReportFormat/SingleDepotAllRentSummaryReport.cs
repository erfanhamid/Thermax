using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SingleDepotAllRentSummaryReport
    {
        public string Depot { get; set; }
        public string Purpose { get; set; }
        public int DealerID { get; set; }
        public string DealerName { get; set; }
        public string AccountName { get; set; }
        public decimal Amount { get; set; }
    }
}