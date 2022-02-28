using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Sales.Report
{
    public class SalesReport
    {
        public DateTime FromDate { set; get; }
        public DateTime ToDate { set; get; }
        public string Depot { set; get; }
        public string Group { set; get; }
        public string Item { set; get; }
        public string CustomerId { set; get; }
        public string CustomerName { set; get; }
        public string SalesManId { set; get; }
        public string SalesManName { set; get; }
        public DateTime FromDateR1 { set; get; }
        public DateTime ToDateR1 { get; set; }
        public DateTime FromDateR2 { set; get; }    
        public DateTime ToDateR2 { set; get; }
    }
}