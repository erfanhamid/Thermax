using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SingleGroupSupplierBillAgingReport
    {
        public int SupplierGroupID { get; set; }
        public string SGroupName { get; set; }
        public int SupplierID { get; set; }
        public string Suppname { get; set; }
        public decimal AccountBalance { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal Thirty { get; set; }
        public decimal Sixty { get; set; }
        public decimal Ninety { get; set; }
        public decimal AboveNinety { get; set; }

    }
}