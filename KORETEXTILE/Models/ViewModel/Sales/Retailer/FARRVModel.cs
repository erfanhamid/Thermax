using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Sales.Retailer
{
    public class FARRVModel
    {
        public int FARRNo { get; set; }
        public int DepotId { get; set; }
        public int CustomerID { get; set; }
        public int DealerAcc { get; set; }
        public string DName { get; set; }
        public string Zone { get; set; }
        public DateTime Date { get; set; }
        public string Area { get; set; }
        public int RetailerId { get; set; }
        public string RetailerName { get; set; }
        public double Amount { get; set; }
        public string Descriptions { get; set; }
        public string RType { get; set; }
        public string RefNo { get; set; }
        public string SelectType { get; set; }

        public double Total { get; set; }
    }
}