using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class RMConsumptionReport
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ParentName { get; set; }
        public int ItemID { get; set; }
        public double Qty { get; set; }
        public string UOM { get; set; }
        public decimal AvgRate { get; set; }
        public double Value { get; set; }
    }
}