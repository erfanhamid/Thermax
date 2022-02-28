using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class AllNewLATRReport
    {
        public int LTRID { get; set; }
        public string LTRNo { get; set; }
        public int LCID { get; set; }
        public string Bank { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string LCNo { get; set; }
    }
}