using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class ImportLCSummaryReport
    {
        public int ILCID { get; set; }
        public DateTime ILCDate { get; set; }
        public string ILCNo { get; set; }
        public string ALCNo { get; set; }
        public string SupplierName { get; set; }
        public DateTime ShipDate { get; set; }
        public decimal ItemTotal { get; set; }
        public decimal Freight { get; set; }
        public decimal GrandTotal { get; set; }
    }
}