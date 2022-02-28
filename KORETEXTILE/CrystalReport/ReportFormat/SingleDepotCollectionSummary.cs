using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SingleDepotCollectionSummary
    {
        public string Depot { get; set; }
        public int DID { get; set; }
        public string DealerName { get; set; }
        public string Account { get; set; }
        public decimal Amount { get; set; }

    }
}