using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class DepotZoneWiseCollectionSummary
    {
        public string Depot { get; set; }
        public string ZoneName { get; set; }
        public decimal Amount { get; set; }
    }
}