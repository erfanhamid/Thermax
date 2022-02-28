using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Dashboard
{
    public class LastThirtyDaySale
    {
        public string SalesMonth { set; get; }
        public int SalesDay { set; get; }
        public int MonthNumb { set; get; }
        public int SalesYear { set; get; }
        public decimal SalesAmount { set; get; }
    }
}