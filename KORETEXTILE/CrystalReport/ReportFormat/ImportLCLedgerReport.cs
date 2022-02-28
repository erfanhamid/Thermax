using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class ImportLCLedgerReport
    {
        public int ILCID { get; set; }
        public string ILCNO { get; set; }
        public string SupplierName { get; set; }
        public int DOCNO { get; set; }
        public string DocType { get; set; }
        public string AccountName { get; set; }
        public DateTime TDate { get; set; }
        public string TDescription { get; set; }
        public string RefNO { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; }
    }
}