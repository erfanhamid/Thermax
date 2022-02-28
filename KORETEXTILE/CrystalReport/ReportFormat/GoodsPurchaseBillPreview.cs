using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class GoodsPurchaseBillPreview
    {
        public string SupplierGroup { get; set; }
        public string SupplierName { get; set; }
        public int BillNo { get; set; } 
        public string PEDate { get; set; }
        public int GRNNo { get; set; }
        public string ItemName { get; set; }
        public string UOM { get; set; }
        //public string PacSize { get; set; }
        public double UnitCost { get; set; }
        public decimal Quantity { get; set; }
        public double VAT { get; set; }
        public double AIT { get; set; }
        public double CastValue { get; set; }
        public int WorkOrderNo { get; set; }
        public string WorkOrderCode { get; set; }
        public string BillDate { get; set; }
        public string RefNo { get; set; }
        public string Description { get; set; }
        public int CartonCapacity { get; set; }
        public double Discount { get; set; }
        public double VDAAmount { get; set; }
        public int SupplierID { get; set; }
        public string ItemGroup { get; set; }
        public decimal TotalBill { get; set; }
    }
}