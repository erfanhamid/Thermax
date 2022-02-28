using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class PreviewGBEReport
    {
        public int GBENo { get; set; }
        public DateTime GBEDate { get; set; }
        public string BranchName { get; set; }
        public string SupplierName { get; set; }
        public string RefNo { get; set; }
        public decimal GBETotal { get; set; }
        public decimal Discountamt { get; set; }
        public decimal VATAmount { get; set; }
        public decimal AITAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal VDSAmount { get; set; }      
        public string AccountName { get; set; }
        public string CostGroup { get; set; }
        public decimal Amount { get; set; }
        public string Descriptions { get; set; }
    }
}