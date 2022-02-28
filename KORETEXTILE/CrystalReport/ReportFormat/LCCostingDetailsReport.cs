using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class LCCostingDetailsReport
    {
        public string Name { get; set; }
        public string PacSize { get; set; }
        public decimal ItemQty { get; set; }
        public decimal LiterKg { get; set; }
        public decimal Cartoon { get; set; }
        public decimal UnitRate { get; set; }
        public decimal TotalCost { get; set; }
    }
}