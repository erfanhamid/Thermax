using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class OrderAndSalesSummaryReport
    {
        public int TsoId { get; set; }
        public string TsoName { get; set; }
        public int OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderVat { get; set; }
        public decimal OrderDiscount { get; set; }
        public decimal OrderAmount { get; set; }
        public decimal OrderNetAmount { get; set; }
        public int SalesNo { get; set; }
        public DateTime SalesDate { get; set; }
        public decimal SalesVat { get; set; }
        public decimal SalesDiscount { get; set; }
        public decimal SalesAmount { get; set; }
        public decimal SalesNetAmount { get; set; }
    }
}