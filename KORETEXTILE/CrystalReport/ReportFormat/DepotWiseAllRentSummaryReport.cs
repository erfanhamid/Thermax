using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class DepotWiseAllRentSummaryReport
    {
        public string Depot { get; set; }
        public string AccountName { get; set; }
        public decimal TAmount { get; set; }
    }
}