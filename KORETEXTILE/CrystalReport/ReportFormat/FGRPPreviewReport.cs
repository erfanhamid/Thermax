using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class FGRPPreviewReport
    {
        public string Name { set; get; }
        public string PacSize { set; get; }
        public double FGRPQty { set; get; }
        public decimal SampleAmount { set; get; }
        public double UnitPrice { set; get; }
    }
}