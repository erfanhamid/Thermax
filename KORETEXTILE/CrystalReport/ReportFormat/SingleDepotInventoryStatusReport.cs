using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SingleDepotInventoryStatusReport
    {
     
        public string GroupName { get; set; }
        public int ID { get; set; }
        public string ItemName { get; set; }
        public decimal PackSize { get; set; }
        public string Unit { get; set; }
        public decimal Rate { get; set; }
        public decimal Littre { get; set; }
        public decimal OpeningStock { get; set; }
        public decimal ReceiveQty { get; set; }
        public decimal Transfer { get; set; }
        public decimal SalesQty { get; set; }
        public decimal Sales { get; set; }
        public decimal Offer { get; set; }
        public decimal Damage { get; set; }
        public decimal Sample { get; set; }
        public decimal DumpingAdjustment { get; set; }
        public decimal OtherAdjustment { get; set; }
        public decimal Return { get; set; }
    }
}