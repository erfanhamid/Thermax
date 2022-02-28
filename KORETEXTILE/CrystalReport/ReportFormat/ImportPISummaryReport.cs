using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class ImportPISummaryReport
    {
        public int IPIID { get; set; }
        public string IPINO { get; set; }
        public string SupplierName { get; set; }
        public DateTime IPIDate { get; set; }
        public string Type { get; set; }
        public string MoPName { get; set; }
        public string CurrencyName { get; set; }
        public string IncotermName { get; set; }
        public string IssuingBank { get; set; }
        public decimal ItemTotal { get; set; }
        public decimal Freight { get; set; }
        public decimal GrandTotal { get; set; }
    }
}