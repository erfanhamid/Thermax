using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Sales.Report
{
    public class ItemWiseSingleDepotSalevModel
    {
        public string ItemName { set; get; }
        public double Qty { set; get; }
        public decimal Rate { set; get; }
        public decimal SalesValue { set; get; }
        public decimal Vat { set; get; }
        public decimal Discount { set; get; }
        public decimal NetSale { set; get; }
        public double FreeQty { set; get; }
    }
}