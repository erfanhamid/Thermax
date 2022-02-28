using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.CommercialVM
{
    public class LCPartialCostingVModel
    {
        [Display(Name = "LCPC No")]
        public int LCPCNo { get; set; }
        [Display(Name = "ILC ID")]
        public int ILCID { get; set; }
        [Display(Name = "ILC No")]
        public int ILCNo { get; set; }
        [Display(Name = "Alt ILC No")]
        public int AltILCNo { get; set; }
        [Display(Name = "LCPC Date")]
        public DateTime LCPCDate { get; set; }
        [Display(Name = "ILC Type")]
        public string ILCType { get; set; }
        [Display(Name = "Total Qty of Costing")]
        public decimal TotalCostingQty { get; set; }
        [Display(Name = "ILC Total Cost")]
        public decimal CostingTotal { get; set; }
        [Display(Name = "Total Cost Allocated")]
        public decimal TotalCostAllocated { get; set; }
        [Display(Name = "Cost to be Allocated")]
        public decimal CostToBeAllocated { get; set; }
        [Display(Name = "Group")]
        public string Group { get; set; }
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
        [Display(Name = "LC QTY")]
        public decimal LCQty { get; set; }
        [Display(Name = "Costed QTY")]
        public decimal CostedQty { get; set; }
        [Display(Name = "Available QTY")]
        public decimal AvailableQty { get; set; }
        [Display(Name = "Unit")]
        public decimal Unit { get; set; }
        [Display(Name = "Item Group")]
        public string ItemGroup { get; set; }
        [Display(Name = "Costing Qty")]
        public decimal CostingQty { get; set; }
        [Display(Name = "Rate (Rough)")]
        public decimal RateRough { get; set; }
        [Display(Name = "Value (Rough)")]
        public decimal ValueRough { get; set; }
        [Display(Name = "Rate")]
        public decimal Rate { get; set; }
        [Display(Name = "Value")]
        public decimal Value { get; set; }
        [Display(Name = "Rest Qty")]
        public decimal RestQty { get; set; }
        [Display(Name ="Lc Final Costing and Close")]
        public bool IsClosed { get; set; }
        [Display(Name = "Total Submitted Bill Amount")]
        public decimal TotalSubmittedBillAmount { get; set; }
        public int Carton { get; set; }
        public string LiterKg { get; set; }


    }
}