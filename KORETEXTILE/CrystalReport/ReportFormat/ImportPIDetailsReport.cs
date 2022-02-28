using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class ImportPIDetailsReport
    {
        public int IPIId { get; set; }
        public string IPINO { get; set; }
        public string Supplier { get; set; }
        public int PurchaseOrderNo { get; set; }
        public string LCType { get; set; }
        public string Item { get; set; }
        public string UOM { get; set; }
        public string PacSize { get; set; }
        public decimal QTY { get; set; }
        public decimal Liter { get; set; }
        public decimal Carton { get; set; }
        public decimal Rate { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
    }
}