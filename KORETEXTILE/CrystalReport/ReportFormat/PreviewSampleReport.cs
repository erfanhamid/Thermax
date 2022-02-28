using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class PreviewSampleReport
    {
        public DateTime Date { get; set; }
        public string BrnachName { get; set; }
        public int DSIANo { get; set; }
        public int ItemID { get; set; }
        public string Name { set; get; }
        public int AdjQTY { set; get; }
        public decimal SampleAmount { set; get; }
        public double UnitPrice { set; get; }
    }
}