using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class PreviewFGP
    {
        public string BatchNo { get; set; }
        public DateTime FGPDate { get; set; }
        public int FGPNo { get; set; }
        public string RefNo { get; set; }
        public string Description { get; set; }
        public string ItemName { get; set; }
        public string GroupName { get; set; }
        public decimal Qty { get; set; }
        public decimal CogsRate { get; set; }
        public decimal CogsTotal { get; set; }
    }
}