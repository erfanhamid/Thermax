using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class StoreWiseSPInvSumReport
    {


        public string StoreName { get; set; }
        public string ParentName { get; set; }
        public string ItemName { get; set; }
        public int CIID { get; set; }
        public decimal BalanceQty { get; set; }
        public decimal BalanceAmnt { get; set; }
    }
}