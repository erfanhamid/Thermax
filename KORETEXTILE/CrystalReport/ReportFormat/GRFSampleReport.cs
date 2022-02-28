using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class GRFSampleReport
    {
        public string Name { set; get; }
        public string PacSize { set; get; }
        public int Qty { set; get; }
        public decimal Amount { set; get; }
        public double UnitPrice { set; get; }
    }
}