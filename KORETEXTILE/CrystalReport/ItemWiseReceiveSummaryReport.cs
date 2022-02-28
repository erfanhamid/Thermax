using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport
{
    public class ItemWiseReceiveSummaryReport
    {
        public string ItemGroup { get; set; }
        public string ItemName { get; set; }
        public string UName { get; set; }
        public decimal ReceiveQty { get; set; }
    }
}