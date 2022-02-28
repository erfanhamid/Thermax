using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class ItemWiseAllDepotSalesSummaryReport
    {
        public string Group { get; set; }
        public string Item { get; set; }
        public double SaleQty { get; set; }
        public decimal Rate { get; set; }
        public decimal Value { get; set; }
        public double FreeQty { get; set; }
    }
}