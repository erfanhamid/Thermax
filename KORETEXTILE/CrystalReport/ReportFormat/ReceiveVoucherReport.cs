using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class ReceiveVoucherReport
    {
        public int RVNo { set; get; }
        public DateTime RVDate { set; get; }
        public string ReceiveAccount { set; get; }
        public string CreditAccount { set; get; }
        public string PayeeName { set; get; }
        public string RefNo { set; get; }
        public string Descriptions { set; get; }
        public decimal Amount { set; get; }
    }
}