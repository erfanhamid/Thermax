using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class RSMWiseSalesAndCollectionReport
    {
        public int RSMId { get; set; }
        public string RSMName { get; set; }
        public int TsoId { get; set; }
        public string TsoName { get; set; }
        public decimal TotalSales { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal Collections { get; set; }
        public decimal Due { get; set; }
    }
}