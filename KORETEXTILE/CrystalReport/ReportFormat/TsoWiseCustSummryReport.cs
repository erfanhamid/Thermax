using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class TsoWiseCustSummryReport
    {
        public int CustId { set; get; }
        public string CustomerName { set; get; }
        public double Credit { set; get; }
        public int CreditDays { set; get; }
        public decimal SalesAmount { set; get; }
        public decimal SalesReturnAmount { set; get; }
        public string TsoName { set; get; }
        public decimal OpeningBalance { set; get; }
        public decimal Collection { set; get; }
        public decimal Due { set; get; }
    }
}