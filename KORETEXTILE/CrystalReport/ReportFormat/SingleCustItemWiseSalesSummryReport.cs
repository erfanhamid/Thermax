using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SingleCustItemWiseSalesSummryReport
    {
        public int InvoiceNo { set; get; }
        public DateTime InvoiceDate { set; get; }
        public int ItemID { set; get; }
        public decimal Price { set; get; }
        public decimal Qty { set; get; }
        public decimal OfferQty { set; get; }
        public decimal TradeValue { set; get; }
        public double Discount { set; get; }
        public double Vat { set; get; }
        public decimal DiscountAmount { set; get; }
        public decimal VatAmount { set; get; }
        public string PacSize { set; get; }
        public string ItemName { set; get; }
        public decimal NetAmount { set; get; }
        public string ItemCode { set; get; }    
        public string Remarks { set; get; }
    }
}