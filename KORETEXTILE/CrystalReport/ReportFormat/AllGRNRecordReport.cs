using System;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class AllGRNRecordReport
    {
        public int GRNNo { get; set; }
        public DateTime GrnDate { get; set; }
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