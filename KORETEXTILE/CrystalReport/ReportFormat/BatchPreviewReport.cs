using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class BatchPreviewReport
    {
        public string Name { set; get; }
        public string PacSize { set; get; }
        public double Qty { set; get; }
        public decimal SampleAmount { set; get; }
        public double UnitPrice { set; get; }
    }
}