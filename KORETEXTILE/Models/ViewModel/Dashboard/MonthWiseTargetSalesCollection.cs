using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Dashboard
{
    public class MonthWiseTargetSalesCollection
    {
        public string SalesMonth { get; set; }
        public decimal TargetSalesAmount { get; set; }
        public decimal TargetCollectionAmount { get; set; }
    }
}