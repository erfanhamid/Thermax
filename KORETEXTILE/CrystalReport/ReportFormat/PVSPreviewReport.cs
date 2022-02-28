using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class PVSPreviewReport
    {
        public int PVNo { set; get; }
        public string ReceiverName { set; get; }
        public string CostCenter { set; get; }
        public string RefNo { set; get; }
        public DateTime Date { set; get; }
        public int Code { set; get; }
        public string AccountHead { set; get; }
        public string SubLedger { set; get; }
        public decimal Amount { set; get; }
        public string Note { set; get; }
    }
}