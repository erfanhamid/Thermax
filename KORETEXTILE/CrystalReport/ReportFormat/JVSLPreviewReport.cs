using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class JVSLPreviewReport
    {
        public string CostCenter { set; get; }
        public string CostGroup { set; get; }
        public string JVDescription { set; get; }
        public int JVNO { set; get; }
        public DateTime JVDate { set; get; }
        public string AccountName { set; get; }
        public string DeborCre { set; get; }
        public string CorospondingAccount { set; get; }
        public string SubLedgerName { set; get; }
        public decimal Amount { set; get; }
        public string Note { set; get; }           
    }
}