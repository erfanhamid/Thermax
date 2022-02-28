using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SingleSupplierBillAgingReport
    {
        public int BillNo { get; set; }
        public DateTime PDate { get; set; }
        public int Durationn { get; set; }
        public int SuppID { get; set; }
        public string Sname { get; set; }
        public decimal Payable { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal DueAmount { get; set; }
        public string AgeGroup { get; set; }
    }
}