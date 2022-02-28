using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class Allworkorderbywono
    {
        public int WONo { get; set; }
        public DateTime WODate { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string GroupName { get; set; }
        public int WOCode { get; set; }
        public decimal WOTotal { get; set; }
        public decimal VATAmount { get; set; }
        public decimal VDSAmount { get; set; }
        public decimal AITAmount { get; set; }

    }
}