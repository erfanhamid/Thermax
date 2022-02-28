using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class BatchSummaryReport
    {
        public string BatchNo { set; get; }
        public DateTime BatchDate { set; get; }
        public double Qty { set; get; }
        public string Name { set; get; }
        public string PacSize { set; get; }
        public string ItemCode { set; get; }
    }
}