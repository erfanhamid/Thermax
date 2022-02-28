using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class BalanceSheetReport
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Type { set; get; }
        public int ParentId { set; get; }
        public int Ordering { set; get; }
        public Double Amount { set; get; }
        public string ParentName { set; get; }
        public string ReportOrderName { set; get; }
    }
}