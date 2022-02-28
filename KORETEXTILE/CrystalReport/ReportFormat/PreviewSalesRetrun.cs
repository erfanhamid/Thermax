using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class PreviewSalesRetrun
    {
        public int ItemID { set; get; }
        public string Name { set; get; }
        public string PacSize { set; get; }
        public double ReturnQty { set; get; }
        public decimal VatAmount { set; get; }
        public decimal DiscountAmount { set; get; }
        public decimal NetAmount { set; get; }
    }
}