using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class ILCBPreviewReport
    {
        public int ILCID { get; set; }
        public string IALCNO { get; set; }
        public string Supplier { get; set; }
        public string Account { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int ILCBNO { get; set; }
        public string SubLedgerAccount { get; set; }
    }
}