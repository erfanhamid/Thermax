using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class StockSummaryReport
    {
        public int ItemId { set; get; }
        public string Name { set; get; }
        public int CtnCapacity { set; get; }
        public string GroupName { get; set; }
        public string Uom { get; set; }
        public decimal ReceiveQty { set; get; }
        public decimal Sample { set; get; }
        public decimal SalesR { set; get; }
        public decimal Sales { set; get; }
        public decimal Offer { set; get; }
        public decimal FGOB { set; get; }
        public decimal ClosingStock { set; get; }
        public decimal IssueQty { set; get; }
    }
}