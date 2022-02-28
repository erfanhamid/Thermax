using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class Yettoreceiveworkorderreport
    {
        public string SupplierGroup { get; set; }
        public string SupplierName { get; set; }
        public int WorkOrderID { get; set; }
        public int WorkOrderNo { get; set; }
        public DateTime WorkOrderDate { get; set; }
        public string ItemGroup { get; set; }
        public string ItemName { get; set; }
        public decimal OrderQTY { get; set; }
        public decimal GRNQTY { get; set; }
        public decimal HaveToGRN { get; set; }

    }
}