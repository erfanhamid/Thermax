using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class FGPSummaryReport
    {
        public string BatchNo { get; set; }
        public DateTime FGPDate { get; set; }
        public int FGPNo { get; set; }
        public string RefNo { get; set; }
        public string Description { get; set; }
        public decimal TotalQty { get; set; }
        public decimal TotalValue { get; set; }
    }
}