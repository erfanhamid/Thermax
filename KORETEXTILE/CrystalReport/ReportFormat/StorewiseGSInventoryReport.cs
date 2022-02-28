using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class StorewiseGSInventoryReport
    {
        public string GroupName { get; set; }
        public int CIID { get; set; }
        public string ItemName { get; set; }
        public string Uname { get; set; }
        public decimal BalanceQty { get; set; }
        public decimal BalanceAmount { get; set; }


    
    }
}