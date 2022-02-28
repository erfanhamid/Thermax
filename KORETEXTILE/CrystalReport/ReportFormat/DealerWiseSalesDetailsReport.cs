using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class DealerWiseSalesDetailsReport
    {
        public string Depot { get; set; }
        public string Zone { get; set; }
        public int DID { get; set; }
        public string DealerName { get; set; }
        public int InvoiceNo { get; set; }
        public DateTime SalesDate { get; set; }
        public decimal Amount { get; set; }
        public decimal Commission { get; set; }
        public decimal Discount { get; set; }
        public decimal NetSales { get; set; }
    }
}