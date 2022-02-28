using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class ItemWiseTransactionSummaryReport
    {
        public string ItemCode { set; get; }
        public string Name { set; get; }
        public string PacSize { set; get; }
        public int ItemId { set; get; }
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