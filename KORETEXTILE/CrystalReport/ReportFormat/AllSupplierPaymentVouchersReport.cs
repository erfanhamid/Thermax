using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class AllSupplierPaymentVouchersReport
    {
        public int SPVNo { get; set; }
        public DateTime Date { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string PaymentAccount { get; set; }
        public string RefNo { get; set; }
        public string Descriptions { get; set; }
        public decimal Amount { get; set; }
    }
}