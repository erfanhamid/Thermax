using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class DealerWiseDueReport
    {
        public string Depot { get; set; }
        public int DID { get; set; }
        public string Dealer { get; set; }
        public string Zone { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal TotalSales { get; set; }
        public decimal TotalCommission { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal NetSales { get; set; }
        public decimal TotalCollection { get; set; }
        public decimal TotalAdjustment { get; set; }
        public decimal DueAmount { get; set; }
    }
}