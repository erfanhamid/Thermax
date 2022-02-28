using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class RMInventoryLedgerReport
    {
        public int CIID { get; set; }
        public string ItemName { get; set; }
        public decimal AbsQty { get; set; }
        public DateTime TDate { get; set; }
        public string DoCType { get; set; }
        public int DocID { get; set; }
        public int StoreID { get; set; }
    }
}