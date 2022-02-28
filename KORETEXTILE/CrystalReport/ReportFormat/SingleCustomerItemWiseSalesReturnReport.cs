using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SingleCustomerItemWiseSalesReturnReport
    {
        public int ItemID { set; get; }
        public double Qty { set; get; }
        public double TradePrice { set; get; }       
        public decimal DiscountAmount { set; get; }
        public decimal NetValue { set; get; }
        public decimal VatAmount { set; get; }
        public decimal NetReturnAmount { set; get; }
        public string PacSize { set; get; }
        public string ItemName { set; get; }
        public string ItemCode { set; get; }
    }
}