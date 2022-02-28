using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class CategoryWiseSalesAndCollectionReport
    {
        public string Category { get; set; }
        public decimal Sales { get; set; }
        public decimal Collections { get; set; }
    }
}