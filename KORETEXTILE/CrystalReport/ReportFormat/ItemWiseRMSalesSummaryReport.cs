using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class ItemWiseRMSalesSummaryReport
    {
        public int RMSalesNo { get; set; }
        public string StoreName { get; set; }
        public string CustomerName { get; set; }
        public DateTime TDate { get; set; }
        public string ItemName { get; set; }
        public decimal ItemQty { get; set; }
        public string UName { get; set; }
        public decimal ItemRate { get; set; }
        public decimal ItemValue { get; set; }
        public decimal CogsRate { get; set; }
        public decimal COGSTotal { get; set; }
    }
}