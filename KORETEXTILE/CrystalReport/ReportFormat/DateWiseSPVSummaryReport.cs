using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class DateWiseSPVSummaryReport
    {
        public DateTime TDate { get; set; }
        public int PBID { get; set; }
        public string CreditAdvance { get; set; }
        public string SGroupName { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public int PaymentAccID { get; set; }
        public string PayAccount { get; set; }
        public decimal SPVAmount { get; set; }
    }
}