using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class ItemWiseRMCSummaryReport
    {
        public int RMCNo { get; set; }
        public DateTime RMCDate { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public decimal ItemQty { get; set; }
        public decimal ItemRate { get; set; }
        public decimal ItemCostTotal { get; set; }
        public string ItemGroup { get; set; }
        public string UName { get; set; }
    }
}