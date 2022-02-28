using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Sales.Report
{
    public class InvoiceVModel
    {
        public int CustomerId { set; get; }
        public string CustomerName { set; get; }
        public string Address { set; get; }
        public int InvoiceNo { set; get; }
        public string OrderBookDate { set; get; }
        public string InvoiceDate { set; get; }
        public string DAssttName { set; get; }
        public string DACode { set; get; }
        public string TSOName { set; get; }
        public string TSOCode { set; get; }
        public decimal NetSale { get; internal set; }
        public string TelePhoneNo { set; get; }
    }
}