using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class InventorySummaryByAllStoreReport
    {
        public string Store { get; set; }
        public string GroupName { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Qty { get; set; }
        public string UOM { get; set; }
        public decimal StandardRate { get; set; }
        public decimal TotalValue { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal RetailValue { get; set; }
    }
}