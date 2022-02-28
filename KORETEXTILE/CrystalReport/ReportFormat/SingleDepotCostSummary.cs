using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SingleDepotCostSummary
    {
        public string AccountHead { get; set; }
        public decimal TAmount { get; set; }
        public string DepotName { get; set; }
        public DateTime TDate { get; set; }
        public string GroupName { get; set; }
    }
}