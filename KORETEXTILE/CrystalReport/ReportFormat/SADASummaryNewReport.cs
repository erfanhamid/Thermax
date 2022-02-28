using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SADASummaryNewReport
    {
        public int SPAANo { get; set; }
        public string SGroupName { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public int SPVNo { get; set; }
        public DateTime TDate { get; set; }
        public string TDescription { get; set; }
        public string RefNo { get; set; }
        public decimal SPVAmount { get; set; }
        public decimal AdjustedAmount { get; set; }
    }
}