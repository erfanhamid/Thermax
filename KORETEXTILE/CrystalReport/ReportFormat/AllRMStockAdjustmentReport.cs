using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class AllRMStockAdjustmentReport
    {
        public string GroupName { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public decimal ItemQty { get; set; }
        public decimal ItemRate { get; set; }
        public decimal ItemValue { get; set; }
        public string TrType { get; set; }
        public DateTime TDate { get; set; }
        public int StoreID { get; set; }
        public string StoreName { get; set; }
        public string DocType { get; set; }
        public int DocID { get; set; }
        public string Uname { get; set; }
    }
}