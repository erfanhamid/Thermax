using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SPAdvancePreviewReport
    {
        public int PBID { get; set; }
        public DateTime TDate { get; set; }
        public string CreditAdvance { get; set; }
        public string SGroupName { get; set; }
        public string SupplierName { get; set; }
        public string Name { get; set; }
        public string RefNo { get; set; }
        public string Description { get; set; }
        public decimal PaidAmt { get; set; }
    }
}