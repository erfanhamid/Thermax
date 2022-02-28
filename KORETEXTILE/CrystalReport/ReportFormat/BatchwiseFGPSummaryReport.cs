using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class BatchwiseFGPSummaryReport
    {
        public int FGPNo { get; set; }
        public DateTime TDate { get; set; }
        public string BatchNo { get; set; }
        public string StoreName { get; set; }
        public string RefNo { get; set; }
        public string TDescription { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public decimal LitrePerUnit { get; set; }
        public decimal TradePrice { get; set; }
        public decimal Qty { get; set; }
        public decimal CostRate { get; set; }
        public decimal CostTotal { get; set; }
        public string GroupName { get; set; }
        public string UName { get; set; }
    }
}