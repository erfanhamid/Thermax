using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class UnadjustedSupplierAdvanceReport
    {
        public int PBID { get; set; }
        public DateTime TDate { get; set; }
        public string CreditAdvance { get; set; }
        public string GroupName { get; set; }
        public int SuppID { get; set; }
        public string SuppName { get; set; }
        public int PaymentAccID { get; set; }
        public string AccName { get; set; }
        public decimal SPVAmount { get; set; }
        public decimal AdjustedAmount { get; set; }
        public decimal UnAdjustedAmount { get; set; }
    }
}