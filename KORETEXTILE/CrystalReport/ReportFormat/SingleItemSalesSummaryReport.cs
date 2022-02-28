using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SingleItemSalesSummaryReport
    {
        public string Depot { get; set; }
        public string ItemGroup { get; set; }
        public string ItemName { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal GrossSale { get; set; }
    }
}