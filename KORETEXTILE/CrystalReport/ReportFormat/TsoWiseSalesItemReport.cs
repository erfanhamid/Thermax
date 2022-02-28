using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class TsoWiseSalesItemReport
    {
        public string TsoName { set; get; }
        public string ItemCode { set; get; }
        public string ItemName { set; get; }
        public string PacSize { set; get; }
        public decimal UnitPrice { set; get; }       
        public double Quantity { set; get; }
        public double OfferQty { set; get; }
        public decimal SalesValue { set; get; }
        public decimal DiscountAmount { set; get; }
        public decimal VatAmount { set; get; }
        public decimal NetAmount { set; get; }
    }
}