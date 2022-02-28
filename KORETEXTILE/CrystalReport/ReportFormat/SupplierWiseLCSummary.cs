using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SupplierWiseLCSummary
    {
        public int SupplierID { set; get; }
        public string SupplierName { set; get; }
        public decimal ImportTotal { set; get; }
    }
}