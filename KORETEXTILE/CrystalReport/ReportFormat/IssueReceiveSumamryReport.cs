using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class IssueReceiveSumamryReport
    {
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string PacSize { get; set; }
        public decimal Issue { get; set; }
        public decimal Receive { get; set; }
    }
}