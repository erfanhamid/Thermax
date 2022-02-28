using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SingleSupplierGRNSummaryReport
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public DateTime TDate { get; set; }
        public int GRNNo { get; set; }
        public string ChallanNo { get; set; }
        public string RefNo { get; set; }
        public string ItemType { get; set; }
        public string GroupName { get; set; }
        public string ItemName { get; set; }
        public decimal ItemQty { get; set; }
        public string UName { get; set; }
    }
}