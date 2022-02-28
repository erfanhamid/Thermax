using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class ItemWiseLCSummary
    {
        public string SupplierName { get; set; }
        public string ItemName { get; set; }
        public decimal ItemQty { get; set; }
        public string UoMName { get; set; }
        public decimal ItemValue { get; set; }
    }
}