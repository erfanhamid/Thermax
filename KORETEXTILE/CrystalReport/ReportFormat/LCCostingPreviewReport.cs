using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class LCCostingPreviewReport
    {
        public string ItemName { get; set; }
        public string UOM { get; set; }
        public string PacSize { get; set; }
        public int Cartoncapacity { get; set; }
        public decimal LCQty { get; set; }
        public decimal TotalLtr { get; set; }
        public decimal TotalValue { get; set; }
        public decimal TotalCost { get; set; }
        public decimal PerCartonCost { get; set; }
        public decimal PerUnitCost { get; set; }
        public decimal PerLtrCost { get; set; }
    }
}