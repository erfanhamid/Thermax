using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class LCRNSamplePreviewReport
    {
        public int LCRNNo { get; set; }
        public DateTime LCRNDate { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string ChallanNo { get; set; }
        public string LCNO { get; set; }
        public string RefNo { get; set; }
        public string Descriptions { get; set; }
        public string clmType { get; set; }
        public string ItemGroup { get; set; }
        public string ItemName { get; set; }
        public decimal QTY { get; set; }
        public string Unit { get; set; }
    }
}