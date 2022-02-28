using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class StockAdjustmentRMPreviewReport
    {
        public int SANo { get; set; }
        public string Store { get; set; }
        public DateTime Date { get; set; }
        public string ReportName { get; set; }
        public string Description { get; set; }
        public string ItemName { get; set; }
        public string UOM { get; set; }
        public decimal Qty { get; set; }
        public decimal UnitCost { get; set; }
        public decimal Value { get; set; }
    }
}