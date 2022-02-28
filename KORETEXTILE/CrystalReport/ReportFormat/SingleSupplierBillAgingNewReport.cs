using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SingleSupplierBillAgingNewReport
    {
        public string SGroupName { get; set; }
        public int SupplierID { get; set; }
        public string Suppname { get; set; }
        public string BillType { get; set; }
        public int BillNo { get; set; }
        public DateTime PDate { get; set; }
        public int BillAge { get; set; }
        public string BillAgeGroup { get; set; }
        public decimal NetBillAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal AdjustedAmount { get; set; }
        public decimal BalanceAmount { get; set; }
    }
}