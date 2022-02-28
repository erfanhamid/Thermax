using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class PurchaseOrderPreviewReport
    {
        public int PONo { get; set; }
        public DateTime PODate { get; set; }
        public string ReferenceNo { get; set; }
        public string PoSubject { get; set; }
        public string Others { get; set; }
        public string ContactPerson { get; set; }
        public string Supplier { get; set; }
        public string SupplierAddress { get; set; }
        public string Item { get; set; }
        public string PacSize { get; set; }
        public string Group { get; set; }
        public decimal UnitCost { get; set; }
        public int ItemQty { get; set; }
        public decimal TotalCost { get; set; }
        public string Specification { get; set; }
        public int CartonQty { get; set; }
    }
}