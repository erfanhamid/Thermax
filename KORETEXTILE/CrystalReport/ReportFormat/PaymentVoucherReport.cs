using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class PaymentVoucherReport
    {
        public int PVNo { get; set; }
        public DateTime PVDate { get; set; }
        public string CostCenter { get; set; }
        public string PayeeName { get; set; }
        public string CreditAccount { get; set; }
        public decimal CrAmount { get; set; }
        public string CostGroup { get; set; }
        public string DebitAccount { get; set; }
        public string Descriptions { get; set; }
        public decimal DrAmount { get; set; }
    }
}