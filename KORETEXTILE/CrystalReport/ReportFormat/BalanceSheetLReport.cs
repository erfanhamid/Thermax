using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class BalanceSheetLReport
    {
        public string Name { set; get; }
        public decimal Amount { set; get; }
        public int ParentId { set; get; }
    }
}