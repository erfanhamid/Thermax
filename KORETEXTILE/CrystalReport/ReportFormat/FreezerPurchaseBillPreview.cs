using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class FreezerPurchaseBillPreview
    {
        public int BillNo { get; set; }
        public DateTime  BillDate { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string SupplierMobileNo { get; set; }
        public string Address { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal AitAmount { get; set; }
        public decimal VDSAmount { get; set; }
        public string Reference { get; set; }
        public int ItemID { get; set; }
        public decimal Rate { get; set; }
        public string Description { get; set; }
        public decimal Qty { get; set; }
        public decimal Value { get; set; }
        public string SupplierCode { get; set; }
        public string TypeName { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string Capacity { get; set; }
        public string MUnit { get; set; }
        public int FRNNo { get; set; }
    }
}