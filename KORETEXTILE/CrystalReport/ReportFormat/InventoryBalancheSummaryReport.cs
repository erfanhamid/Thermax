using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class InventoryBalancheSummaryReport
    {
        public string Store { get; set; }
        public string GroupName { get; set; }
        public string ItemName { get; set; }
        public decimal Qty { get; set; }
        public string UOM { get; set; }
        public decimal UnitCost { get; set; }
        public decimal Amount { get; set; }
    }
}