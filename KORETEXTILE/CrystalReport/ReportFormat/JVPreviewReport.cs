using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class JVPreviewReport
    {
        public string CostCenter { set; get; }
        public string CostGroup { set; get; }
        public int JVNo { set; get; }
        public DateTime JVDate { set; get; }
        public string DrAccName { set; get; }
        public decimal DrAmount { set; get; }
        public string CrAccName { set; get; }
        public decimal CrAmount { set; get; }
        public string Description { set; get; }
    }
}