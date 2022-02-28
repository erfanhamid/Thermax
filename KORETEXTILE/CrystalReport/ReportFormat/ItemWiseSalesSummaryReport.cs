using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class ItemWiseSalesSummaryReport
    {
        public int ItemID { set; get; }
        public string ItemName { set; get; }
        public string ItemCode { set; get; }
        public double Qty { set; get; }
        public double OfferQty { set; get; }
        public double UnitPrice { set; get; }
        public decimal SalesValue { set; get; }
        public decimal DiscountAmount { set; get; }
        public decimal VatAmount { set; get; }
        public decimal NetAmount { set; get; }
    }
}