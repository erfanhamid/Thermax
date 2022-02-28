using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class TsoWiseSalesAndCollectionReport
    {
        public string TsoName { get; set; }
        public int InvoiceNo { set; get; }
        public DateTime SalesDate { set; get; }
        public string CustomerName { set; get; }
        public decimal InvoiceAmount { set; get; }
        public decimal DiscountAmt { set; get; }
        public decimal VatAmount { set; get; }
        public decimal BillAmount { set; get; }
        public decimal PaidAmount { set; get; }
        public decimal SalesReturn { set; get; }
        public string PayMode { set; get; }
        public decimal Dues { set; get; }
    }
}