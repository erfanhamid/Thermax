using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Sales.Report
{
    public class DepotAndDealerWiseIncentiveSummaryReport
    {
        public string BrnachName { set; get; }
        public string ZoneName { set; get; }
        public string AreaName { set; get; }
        public int CustomerID { set; get; }
        public string CustomerName { set; get; }
        public DateTime SalesDate { set; get; }
        public int InvoiceNo { set; get; }
        public decimal InvoiceAmount { set; get; }
        public decimal VatAmount { set; get; }
        public decimal DiscountAmt { set; get; }
        public decimal NetInvoice { set; get; }
    }
}