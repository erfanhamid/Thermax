using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class IssueReceiveDetailsReport
    {
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string PacSize { get; set; }
        public decimal Issue { get; set; }
        public decimal Receive { get; set; }
        public int DocID { get; set; }
        public string ChallanNo { get; set; }
        public DateTime Tdate { get; set; }
    }
}