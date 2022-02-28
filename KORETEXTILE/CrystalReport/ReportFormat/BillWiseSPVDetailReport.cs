using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class BillWiseSPVDetailReport
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
        public string BillType { get; set; }
        public int BilllNo { get; set; }
        public decimal PaymentAmount { get; set; }
    }
}