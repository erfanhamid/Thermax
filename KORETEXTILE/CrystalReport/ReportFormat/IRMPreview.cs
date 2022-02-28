using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class IRMPreview
    {
        public int IRMPNo { get; set; }
        public DateTime IRMDate { get; set; }
        public string IRMDescription { get; set; }
        public string RefNo { get; set; }
        public int GRQNo { get; set; }
        public string ItemName { get; set; }
        public string ItemGroupName { get; set; }
        public decimal IRMQty { get; set; }
        public string UoMName { get; set; }

    }
}