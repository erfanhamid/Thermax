using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class RMCSummaryReport
    {
        public DateTime Date { get; set; }
        public int RMCNo { get; set; }
        public string Description { get; set; }
        public string RefNo { get; set; }
        public decimal TotalQty { get; set; }
        public decimal TotalValue { get; set; }
        public string BatchNo { get; set; }
    }
}