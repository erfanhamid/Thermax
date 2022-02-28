using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class DealerWiseSalesSummaryReport
    {
        public string Depot { get; set; }
        public int DID { get; set; }
        public string DealerName { get; set; }
        public decimal SalesAmount { get; set; }
        public decimal SalesCommission { get; set; }
        public decimal SalesDiscount { get; set; }
        public decimal DumpingSale { get; set; }
        public decimal DumpingCommission { get; set; }
        public decimal DumpingDiscount { get; set; }
        public decimal TotalSale { get; set; }
        public decimal TotalCommission { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal NetSale { get; set; }

    }
}