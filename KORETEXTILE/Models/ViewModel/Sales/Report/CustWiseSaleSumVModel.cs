using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Sales.Report
{
    public class CustWiseSaleSumVModel
    {
        public string BrnachName { set; get; }
        public int CustomerID { set; get; }
        public string CustomerName { set; get; }
        public decimal InvoiceAmount { set; get; }
        public decimal VatAmount { set; get; }
        public decimal DiscountAmt { set; get; }
        public decimal NetAmount { set; get; }
    }
}