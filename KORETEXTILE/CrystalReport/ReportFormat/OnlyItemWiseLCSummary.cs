using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class OnlyItemWiseLCSummary
    {
        public string ItemName { get; set; }
        public decimal ItemQty { get; set; }
        public string UoMName { get; set; }
        public decimal ItemValue { get; set; }
    }
}