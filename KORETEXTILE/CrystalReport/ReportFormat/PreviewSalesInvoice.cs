using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class PreviewSalesInvoice
    {
        public string Depot { get; set; }
        public string ZoneName { get; set; }
        public int SalesManID { get; set; }
        public string SalesMan { get; set; }
        public string Store { get; set; }
        public int InvoiceNo { get; set; }
        public DateTime SalesDate { get; set; }
        public string DealerName { get; set; }
        public int DealerID { get; set; }
        public decimal PreviousDue { get; set; }
        public string ItemGroup { get; set; }
        public string ItemName { get; set; }
        public int Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal Price { get; set; }
        public int OfferQty { get; set; }
        public decimal Total { get; set; }
        public decimal Commission { get; set; }
        public decimal Discount { get; set; }
        public decimal NetInvoiceTotal { get; set; }
    }
}