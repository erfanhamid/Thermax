using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class TSOWiseCollectionDetailsReport
    {
        public int CustomerId { set; get; }
        public string CustomerName { set; get; }
        public string TsoName { set; get; }
        public int TSOId { set; get; }
        public int InvoiceNo { set; get; }
        public int RPID { set; get; }
        public decimal Collections { set; get; }
    }
}