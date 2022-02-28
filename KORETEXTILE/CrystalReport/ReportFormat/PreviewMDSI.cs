using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class PreviewMDSI
    {
        public DateTime MDSIDate { get; set; }
        public string Account { get; set; }
        public string BrnachName { get; set; }
        public int MDSINO { get; set; }
        public int DealerID { get; set; }
        public string DealerName { get; set; }
        public string RefNo { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}