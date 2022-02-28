using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class InvoiceWiseAgingReport
    {
        public int InvoiceNo { set; get; }
        public DateTime SalesDate { set; get; }
        public int DateCount { set; get; }
        public decimal ThirtyDaysAmount { set; get; }
        public decimal SixtyDaysAmount { set; get; }
        public decimal NinetyDaysAmount { set; get; }
        public decimal AboveNinetyDaysAmount { set; get; }
        
    }
}