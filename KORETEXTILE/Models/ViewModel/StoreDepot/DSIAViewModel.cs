using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ViewModel.StoreDepot
{
    public class DSIAViewModel
    {
        [Display(Name = "DSIA No")]
        public int DSIANo { get; set; }
        [Display(Name = "Date")]
        public DateTime DSIADate { get; set; }
        public int Depot { get; set; }
        [Display(Name = "Store")]
        public int StoreID { get; set; }
        [Display(Name = "Type")]
        public string Type { get; set; }
        public string Ref { get; set; }
        [Display(Name ="Note")]
        public string Description { get; set; } 
        [Display(Name = "Total Qty")]
        public int TotalQty { get; set; }
        [Display(Name = "Total Value")]
        public decimal TotalValue { get; set; }
        [Display(Name = "Group")]
        public int GroupId { get; set; }
        [Display(Name = "Item")]
        public int ItemID { get; set; }
        [Display(Name = "Unit Per Ctn")]
        public int UnitPerCtn { get; set; }
        [Display(Name = "Available Qty(Ctn)")]
        public decimal AvailableCtn { get; set; }
        [Display(Name = "Available Qty(PCs)")]
        public int AvailableQty { get; set; }
        [Display(Name = "Adjust Qty (Ctn)")]
        public int AdjCtn { get; set; }
        [Display(Name = "Adjust Qty (PCs)")]
        public int AdjQTY { get; set; }
        [Display(Name = "Balance Qty")]
        public int BalanceQty { get; set; }
        [Display(Name = "Unit Price")]
        public decimal Rate { get; set; }
        public decimal Value { get; set; }
       // public int TSoId { get; set; }
        //public string Category { get; set; }
        //[Display(Name = "Total Vat")]
       // public decimal TotalVat { get; set; }
        public decimal CogsRate { set; get; }
        public decimal COGSTotal { set; get; }
        public string UOM { get; set; }
    }
}