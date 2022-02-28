using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Procurement
{
    public class FreezerWorkOrderViewModel
    {
        [Display(Name = "FWO No")]
        public int WONo { get; set; }

        [Display(Name = "FWO Date")]
        public DateTime WODate { get; set; }

        [Display(Name = "Group")]
        public int SupplierGroup { get; set; }

        [Display(Name = "Supplier")]
        public int SupplierID { get; set; }

        [Display(Name = "Supplier Code")]
        public int SupplierCode { get; set; }

        [Display(Name = "Delivery Date")]
        public DateTime DeliveryDate { get; set; }

        [Display(Name = "Payment Terms")]
        public int PaymentTerms { get; set; }

        [Display(Name = "Department")]
        public string RefNo { get; set; }

        [Display(Name = "Validity")]
        public int ValidDays { get; set; }

        [Display(Name = "WO Total")]
        public decimal WOTotal { get; set; }

        [Display(Name = "Work Order Code")]
        public int WOCode { get; set; }

        public int DdtOption { get; set; }
        public int PaymentOption { get; set; }
        public int AddressOption { get; set; }
        public string DdtTerms { get; set; }
        public string PayTerms { get; set; }
        public string AddressTerms { get; set; }

        [Display(Name = "Item")]
        public int ItemID { get; set; }

        [Display(Name = "Rate")]
        public decimal UnitCost { get; set; }

        [Display(Name = "Order Qty")]
        public int ItemQty { get; set; }

        public string UOM { get; set; }

        [Display(Name = "Value")]
        public decimal TotalCost { get; set; }
        public string Specification { get; set; }

        [Display(Name = "Group")]
        public int GroupID { get; set; }

        [Display(Name = "VAT (%)")]
        public decimal VatPer { get; set; }

        [Display(Name = "VAT Amount")]
        public decimal VatPlus { get; set; }

        [Display(Name = "Total After VAT")]
        public decimal NetOfVat { get; set; }

        [Display(Name = "VDS (%)")]
        public decimal VdsPer { get; set; }
        [Display(Name = "VDS Amount")]
        public decimal VdsAmount { get; set; }

        [Display(Name = "AIT (%)")]
        public decimal AitPer { get; set; }

        [Display(Name = "AIT Amount")]
        public decimal AitMinus { get; set; }

        [Display(Name = "Net Payable")]
        public decimal NetOfAit { get; set; }

        [NotMapped]
        public int TotalQty { get; set; }
        //new element
        public string Description { get; set; }
    }
}