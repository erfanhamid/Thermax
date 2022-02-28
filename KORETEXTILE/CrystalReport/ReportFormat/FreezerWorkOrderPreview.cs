using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class FreezerWorkOrderPreview
    {
        public int WorkOrderNo { get; set; }
        public DateTime WorkOrderDate { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public DateTime DelivaryDate { get; set; }
        public int PaymentTerms { get; set; }
        public string ReferenceNo { get; set; }
        public int ValidDays { get; set; }
        public decimal WorkOrderTotal { get; set; }
        public int clmCCode { get; set; }
        public int DDtOption { get; set; }
        public int PaymentOption { get; set; }
        public int AddressOption { get; set; }
        public int WorkOrderCode { get; set; }
        public string DdtTerms { get; set; }
        public string PayTerms { get; set; }
        public string AddressTerms { get; set; }
        public decimal VatAmount { get; set; }
        public decimal AitAmount { get; set; }
        public int ItemID { get; set; }
        public string Description { get; set; }
        public decimal ItemRate { get; set; }
        public decimal ItemQty { get; set; }
        public decimal ItemValue { get; set; }
        public string Specification { get; set; }
        public string ContactPerson { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string Address { get; set; }
        public string SupplierCode { get; set; }
        public string TypeName { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string Capacity { get; set; }
        public string MUnit { get; set; }
    }
}