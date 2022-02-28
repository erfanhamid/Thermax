using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class LATRLedgerReport
    {
        public int LTRID { get; set; }
        public string LTRNo { get; set; }
        public DateTime Date { get; set; }
        public string AccountHead { get; set; }
        public string Doctype { get; set; }
        public int DocNo { get; set; }
        public string Reference  { get; set; }
        public string Descriptions { get; set; }
        public decimal DrAmount { get; set; }
        public decimal CrAmount { get; set; }
        public decimal Balance { get; set; }
        public string Supplier { get; set; }
    }
}