using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class ImportLCPaymentReport
    {
        public string Description { get; set; }
        public string Remarks { get; set; }
        public string DebitAccount { get; set; }
        public string CreditAccount { get; set; }
        public decimal DrAmount { get; set; }
        public decimal CrAmount { get; set; }
    }
}