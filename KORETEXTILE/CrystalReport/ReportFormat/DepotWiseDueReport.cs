using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class DepotWiseDueReport
    {
        public string Depot { get; set; }
        public decimal TotalSales { get; set; }
        public decimal TotalCommission { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal NetSales { get; set; }
        public decimal TotalCollection { get; set; }
        public decimal TotalAdjustment { get; set; }
        public decimal DueAmount { get; set; }
        public string DepotGroup { get; set; }
        public string DepotSub { get; set; }
    }
}