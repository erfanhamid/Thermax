using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class CombinedRMInventoryBalanceReport
    {
        public string GroupName { get; set; }
        public string ItemName { get; set; }
        public string UOM { get; set; }
        public decimal Factory_RM { get; set; }
        public decimal Floor_RM { get; set; }
    }
}