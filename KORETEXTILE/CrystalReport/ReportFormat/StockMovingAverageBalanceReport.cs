using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class StockMovingAverageBalanceReport
    {
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public string UoM { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal Value { get; set; }
        public string GroupName { get; set; }
    }
}