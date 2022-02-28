using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class DealerRentLedgerReport
    {
        public int Retailerid { get; set; }
        public string Retailername { get; set; }
        public int Cid { get; set; }
        public string DealerName { get; set; }
        public string Depotname { get; set; }
        public int DocNo { get; set; }
        public string Doctype { get; set; }
        public string Accname { get; set; }
        public DateTime TDate { get; set; }
        public string Trdescription { get; set; }
        public string RefNo { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; }
    }
}