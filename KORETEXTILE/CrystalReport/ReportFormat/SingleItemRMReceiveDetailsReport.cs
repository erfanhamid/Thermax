using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SingleItemRMReceiveDetailsReport
    {
        public int GRNNo { get; set; }
        public DateTime TDate { get; set; }
        public string ChallanNo { get; set; }
        public string SupplierName { get; set; }
        public string Ref { get; set; }
        public string Description { get; set; }
        public string ItemGroup { get; set; }
        public string ItemName { get; set; }
        public decimal ReceiveQty { get; set; }
        public string UName { get; set; }
    }
}