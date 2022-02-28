using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class StoreWiseInventoryValueSummaryReport
    {
        public string Store { get; set; }
        public decimal TotalQty { get; set; }
        public decimal StandardValue { get; set; }
        public decimal RetailValue { get; set; }
    }
}