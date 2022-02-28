using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class LATRPaymentReport
    {
        public int LTRPNo { get; set; }
        public DateTime Date { get; set; }
        public int LTRID { get; set; }
        public string Bank { get; set; }
        public decimal Amount { get; set; }
        public string LATRNo { get; set; }
        public int ILCId { get; set; }
        public string LCNo { get; set; }
    }
}