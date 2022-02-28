using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class DPRPreviewReport
    {
        public int DPRNo { get; set; }
        public int DealerID { get; set; }
        public string DealerName { get; set; }
        public string PaymentAccount { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string RefNo { get; set; }
        public string Description { get; set; }
        public string Depot { get; set; }
        public string Zone { get; set; }
        public string Area { get; set; }
    }
}