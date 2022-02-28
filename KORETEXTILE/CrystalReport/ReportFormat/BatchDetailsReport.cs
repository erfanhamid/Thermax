using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class BatchDetailsReport
    {
        public int BatchId { set; get; }
        public string BatchNo { set; get; }
        public DateTime BatchDate { set; get; }
        public string ItemCode { set; get; }
        public string ItemName { set; get; }
        public double ProdQty { set; get; }
        public string BatchDesc { set; get; }
    }
}