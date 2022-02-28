using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class StoreWiseItemQtyReport
    {
        public string Store { get; set; }
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string UOM { get; set; }
        public decimal AvalilabeQty { get; set; }
        public string GroupName { get; set; }
        public int CartonCapacity { get; set; }
        public decimal Itembal { get; set; }
        public decimal RetailPrice { get; set; }

    }
}