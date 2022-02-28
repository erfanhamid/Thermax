using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Sales.Report
{
    public class CustomerReportVModel
    {
        public string Depot { set; get; }
        public string CustomerId { set; get; }
        public string CustomerName { set; get; }
        public DateTime AsOnDate { set; get; }
        public DateTime FromDate { set; get; }
        public DateTime ToDate { set; get; }
        public string Address { set; get; }
        public int TSOId { get; set; }
    }
}