using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class IFGSPreviewReport
    {
        public string Name { set; get; }
        //public string PacSize { set; get; }
        public decimal IssueQty { set; get; }
        public decimal SampleAmount { set; get; }
        public decimal UnitPrice { set; get; }
    }
}