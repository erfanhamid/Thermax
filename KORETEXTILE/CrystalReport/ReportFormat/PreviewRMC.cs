using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class PreviewRMC
    {
        public int RMCNo { get; set; }
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string UOM { get; set; }
        public decimal Qty { get; set; }
        public decimal RmcRate { get; set; }
        public decimal RmcValue { get; set; }
        public decimal RMCTotalValue { get; set; }
        public decimal TotalQty { get; set; }
        public DateTime RMCDate { get; set; }
        public string Description { get; set; }
        public string RefNo { get; set; }
        public string BatchNo { get; set; }
        public string Group { get; set; }

    }
}