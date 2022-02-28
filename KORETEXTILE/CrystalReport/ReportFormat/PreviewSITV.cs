using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class PreviewSITV
    {
        public int SITVNo { get; set; }
        public DateTime Date { get; set; }
        public string ShiftFrom { get; set; }
        public string ShiftTo { get; set; }
        public string DriverName { get; set; }
        public string GroupName { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int CartonCapacity { get; set; }
        public int Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal Value { get; set; }
    }
}