using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.StoreDepot
{
    public class StockAdjustmentVModel
    {
        [Display(Name = "SA No")]
        public int SANo { get; set; }
        public int Depot { get; set; }
        public int Store { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Transection Type")]
        public string TransectionType { get; set; }
        public string Description { get; set; }
        [Display(Name ="Reference No")]
        public string RefNo { get; set; }
        [Display(Name = "Item")]
        public int ItemId { get; set; }
        public decimal Qty { get; set; }
        [Display(Name = "Group")]
        public int GroupId { get; set; }
        public string UOM { get; set; }

        [Display(Name = "Available Qty (PCs)")]
        public decimal AvailableQty { get; set; }
        [Display(Name = "PCs Per Carton")]
        public int PcsPerCtn { get; set; }
        [Display(Name = "Available Qty(Ctn)")]
        public decimal AvailableQtyCtn { get; set; }
        [Display(Name = "Issue Qty(Ctn)")]
        public decimal QtyCtn { get; set; }
        [Display(Name = "Balance Qty(PCs)")]
        public int BalanceQty { get; set; }
        [Display(Name = "Balance Qty(ctn)")]
        public int BalanceQtyCtn { get; set; }
        [Display(Name = "Rate")]
        public decimal UnitCost { get; set; }
        [Display(Name ="Value")]
        public decimal Value { get; set; }
        public decimal Total { get; set; }
    }
}