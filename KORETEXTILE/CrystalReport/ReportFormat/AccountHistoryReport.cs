using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class AccountHistoryReport
    {
        public int CAID { get; set; }
        public string AccountName { get; set; }
        public string DebitCredit { get; set; }
        public Decimal TAmount { get; set; }
        public DateTime TDate { get; set; }
        public string RefNo { get; set; }
        public string PayeeName { get; set; }
        public string TDescription { get; set; }
        public string TDocType { get; set; }
        public int TDocID { get; set; }
        public string CostCenterName { get; set; }
    }
}