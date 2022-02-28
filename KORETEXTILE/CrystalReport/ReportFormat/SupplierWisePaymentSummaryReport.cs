using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SupplierWisePaymentSummaryReport
    {
        public string Groups { get; set; }
        public string SupplierName { get; set; }
        public int SPVNo { get; set; }
        public DateTime Date { get; set; }
        public string PaymentAccount { get; set; }
        public decimal Amount { get; set; }
    }
}