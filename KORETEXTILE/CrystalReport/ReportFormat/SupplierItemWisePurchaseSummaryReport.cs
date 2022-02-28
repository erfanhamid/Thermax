using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SupplierItemWisePurchaseSummaryReport
    {
        public int SGroupID { get; set; }
        public string SGroupName { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string groupname { get; set; }
        public string ItemName { get; set; }
        public string MUnit { get; set; }
        public decimal Qty { get; set; }
        public decimal Amount { get; set; }

    }
}