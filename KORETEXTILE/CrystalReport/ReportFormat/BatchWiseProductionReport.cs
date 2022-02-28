using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class BatchWiseProductionReport
    {
        public int BatchId { set; get; }
        public string BatchNo { set; get; }
        public string ItemCode { set; get; }
        public string ItemName { set; get; }
        public double BatchQty { set; get; }
        public decimal ProdQty { set; get; }
        public string BatchDesc { set; get; }
        public DateTime BatchDate { set; get; }
    }
}