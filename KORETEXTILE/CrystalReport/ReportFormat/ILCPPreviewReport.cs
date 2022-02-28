using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class ILCPPreviewReport
    {
        public int ILCID { get; set; }
        public string IALCNO { get; set; }
        public string PaymentAccount { get; set; }
        public string Account { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int ILCPNO { get; set; }
        public string SubLedgerAccount { get; set; }
    }
}