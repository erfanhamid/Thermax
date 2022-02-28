using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ViewModel.CommercialVM
{
    public class PurchaseOrderVModel
    {
        [Display(Name = "PO No")]
        public int PONo { get; set; }
        [Display(Name = "Date")]
        public DateTime PODate { get; set; }
        [Display(Name = "Supplier")]
        public int SupplierID { get; set; }
        public string RefNo { get; set; }
        [Display(Name = "PO Total")]
        public decimal POTotal { get; set; }
        [Display(Name = "Total Qty")]
        public double TotalQty { get; set; }
        [Display(Name = "Total Vat")]
        public decimal TotalVat { set; get; }
        
        [Display(Name = "Item")]
        public int ItemID { get; set; }
        [Display(Name = "Unit Cost")]
        public decimal UnitCost { get; set; }
        public double Cartoon { get; set; }
        [Display(Name = "Order Qty")]
        public double ItemQty { get; set; }
        [Display(Name = "Liter / KG")]
        public double LiterKg { get; set; }
        [Display(Name = "Value")]
        public decimal TotalCost { get; set; }
        public string Specification { get; set; }
        [Display(Name = "Group")]
        public int GroupID { get; set; }
        [Display(Name = "Vat (%)")]
        public double VatPerc { set; get; }

        [Display(Name = "Total Vat (+)")]
        public decimal VatPlus { get; set; }

        [Display(Name = "Net Of vat")]
        public decimal NetOfVat { get; set; }

        [Display(Name = "Group")]
        public int SupplierGroup { get; set; }

        [Display(Name = "Supplier Code")]
        public int SupplierCode { get; set; }
        public string UOM { get; set; }
        [Display(Name = "Reference No")]
        public string ReferenceNo { get; set; }
        [Display(Name = "Po Subject")]
        public string PoSubject { get; set; }
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }
        public string Others { get; set; }
        [Display(Name = "Rate Per Liter/KG")]
        public decimal RatePerLiter { get; set; }
    }
}