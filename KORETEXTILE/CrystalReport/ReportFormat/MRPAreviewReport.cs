using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class MRPAreviewReport
    {
        public int mrpano { get; set; }
        public DateTime mrpadate { get; set; }
        public int empid { get; set; }
        public string empname { get; set; }
        public string designame { get; set; }
        public int suppid { get; set; }
        public string suppname { get; set; }
        public decimal amnt { get; set; }
        public string rfno { get; set; }
        public string descriptionsss { get; set; }
    }
}