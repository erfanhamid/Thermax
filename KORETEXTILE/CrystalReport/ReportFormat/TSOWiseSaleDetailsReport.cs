using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class TSOWiseSaleDetailsReport
    {
        public string TsoName { set; get; }
        public int TSOId { set; get; }
        public string CustomerName { set; get; }
        public int CustomerId { set; get; }
        public int InvoiceNo { set; get; }
        public DateTime InvoiceDate { set; get; }
        public decimal InvoiceAmount { set; get; }
        public decimal TradeVat { set; get; }
        public decimal TradeDiscount { set; get; }
        public decimal NetSale { set; get; }
    }
}