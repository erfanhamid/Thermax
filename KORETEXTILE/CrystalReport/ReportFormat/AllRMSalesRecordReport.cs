using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class AllRMSalesRecordReport
    {
        public int RMSNo { get; set; }
        public string StoreName { get; set; }
        public string CustomerName { get; set; }
        public DateTime TDate { get; set; }
        public string RefNo { get; set; }
        public string TDescription { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal CostTotal { get; set; }
    }
}