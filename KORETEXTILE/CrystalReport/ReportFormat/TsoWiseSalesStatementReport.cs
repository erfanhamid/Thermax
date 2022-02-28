using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class TsoWiseSalesStatementReport
    {
        public int TsoId { set; get; }
        public string TsoName { set; get; }
        public decimal OpeingBalance { set; get; }
        public decimal TradePrice { set; get; }
        public decimal BonusAmount { set; get; }
        public decimal SampleMonthly { set; get; }
        public decimal SampleSpecial { set; get; }
        public decimal TotalSample { set; get; }
        public decimal DiscountAmt { set; get; }
        public decimal VatAmount { set; get; }      
        public decimal Collection { set; get; }
        public decimal NetReceivable { set; get; }
        public decimal NetDues { set; get; }
    }
}