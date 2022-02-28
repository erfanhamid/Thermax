using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class PreviewLcEntry
    {
        public int ItemId { set; get; }
        public decimal Qty { set; get; }
        public decimal LcCost { set; get; }
        public decimal MiscellaneousCost { set; get; }
        public string ItemName { set; get; }
        public string Name { set; get; }
        public string PacSize { set; get; }
        public decimal ActualUnitCost { set; get; }
        public decimal Rate { set; get; }
    }
}