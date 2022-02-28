using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class PurchaseOrderDetails
    {
        public DateTime PODate { get; set; }
        public string ItemName { get; set; }
        public int PacSize { get; set; }
        public int ItemQuantity { get; set; }
        public double Litre { get; set; }
        public double UnitCost { get; set; }
        public double TotalFOB { get; set; }
        public int CartonQuantity { get; set; }
    }
}