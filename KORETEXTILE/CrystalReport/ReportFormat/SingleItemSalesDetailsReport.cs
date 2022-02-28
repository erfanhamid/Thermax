using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SingleItemSalesDetailsReport
    {
        public string Depot { get; set; }
        public string Item { get; set; }
        public DateTime SalesDate { get; set; }
        public int InvoiceNo { get; set; }
        public decimal Qty { get; set; }
        public decimal Price { get; set; }
        public decimal SalesValue { get; set; }
        public decimal Commsission { get; set; }
        public decimal Discount { get; set; }
        public decimal NetSale { get; set; }
    }
}