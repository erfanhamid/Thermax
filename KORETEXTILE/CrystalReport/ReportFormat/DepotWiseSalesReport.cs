using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class DepotWiseSalesReport
    {
        public int Dealer { get; set; }
        public string DealerName { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DamageAmount { get; set; }
        public decimal DemagePercentage { get; set; }
        public int DepotID { get; set; }
        public string DepotName { get; set; }
    }
}