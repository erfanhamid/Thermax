using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class ItemWiseSingleDepotSaleReport
    {
        public string Depot { get; set; }
        public string Group { get; set; }
        public string Item { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal Value { get; set; }
        public decimal FreeQty { get; set; }
        public decimal Amount { get; set; }
        public decimal Commission { get; set; }

    }
}