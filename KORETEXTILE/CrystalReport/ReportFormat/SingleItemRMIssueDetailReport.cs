using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SingleItemRMIssueDetailReport
    {
        public int IRMPNo { get; set; }
        public DateTime TDate { get; set; }
        public string RefNo { get; set; }
        public string TDescription { get; set; }
        public int GRQNo { get; set; }
        public string ItemName { get; set; }
        public string ItemGroup { get; set; }
        public decimal Qty { get; set; }
        public string UName { get; set; }
    }
}