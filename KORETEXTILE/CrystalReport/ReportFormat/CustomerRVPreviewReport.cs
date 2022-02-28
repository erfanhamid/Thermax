using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class CustomerRVPreviewReport
    {
        public int RPNo { set; get;}
        public DateTime Date { set; get; }
        public string Description { set; get; }
        public string RefNo { set; get; }
        public decimal Amount { set; get; }
        public string ReceiveFrom { set; get; }
        public string ReceiveAccount { set; get; }
    }
}