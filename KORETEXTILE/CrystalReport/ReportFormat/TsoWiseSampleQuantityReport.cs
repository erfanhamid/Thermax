using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class TsoWiseSampleQuantityReport
    {
        public int TSOId { get; set; }
        public string Name { get; set; }
        public int DSIANo { get; set; }
        public DateTime DSIADate { get; set; }
        public decimal AdjQTY { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string PacSize { get; set; }
    }
}