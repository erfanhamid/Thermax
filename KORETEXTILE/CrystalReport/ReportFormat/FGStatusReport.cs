using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class FGStatusReport
    {
        public string StoreName { set; get; }
        public string ItemName { set; get; }
        public string ItemCode { get; set; }
        public string Unit { get; set; }
        public double BalanceQty { set; get; }
        public decimal BalanceAmount { set; get; }
        public double RetailPrice { set; get; }
        public double TotalRetailPrice { set; get; }
        public string PacSize { set; get; }
        public int CartonCapacity { get; set; }

    }
}