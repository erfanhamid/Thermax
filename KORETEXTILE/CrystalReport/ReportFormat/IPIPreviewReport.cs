using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class IPIPreviewReport
    {
        public int PIId { get; set; }
        public string RefNo { get; set; }
        public DateTime Date { get; set; }
        public string ProductDescription { get; set; }
        public string HSCode { get; set; }
        public decimal Unit { get; set; }
        public decimal Cartoon { get; set; }
        public decimal Quantity { get; set; }
        public decimal CFRPricePerLitreKg { get; set; }
        public decimal TotalCFRPrice { get; set; }
        public string PortOfLoading { get; set; }
        public string PortOfDelivery { get; set; }
        public decimal InvoicePriceIncludingFreight { get; set; }
        public string PaymentTerms { get; set; }
        
    }
}