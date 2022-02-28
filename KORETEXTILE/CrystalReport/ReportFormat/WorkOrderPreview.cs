using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class WorkOrderPreview
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
        public int WorkOrderCode { get; set; }
        public string DdtTerms { get; set; }
        public string PayTerms { get; set; }
        public string AddressTerms { get; set; }
        public decimal VatAmount { get; set; }
        public decimal VdsAmount { get; set; }
        public decimal AitAmount { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string GroupName { get; set; }
        public decimal ItemRate { get; set; }
        public decimal ItemQuantity { get; set; }
        public decimal ItemValue { get; set; }
        public string ItemDescriptions { get; set; }
        public string MUName { get; set; }
    }
}