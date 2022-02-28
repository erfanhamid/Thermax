using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ProductionModule
{
    public class IssueFGStoreVModel
    {
        [Display(Name = "IFGS No")]
        public int clmIFGSID { get; set; }
        [Display(Name = "Issue Date")]
        public DateTime clmIFGSDate { get; set; }
        [Display(Name = "Total")]
        public decimal clmIFGSTotal { get; set; }
        public decimal clmCostTotal { get; set; }
        [Display(Name = "Description")]
        public string clmDescription { get; set; }
        [Display(Name = "Reference No")]
        public string clmRefno { get; set; }
        [Display(Name = "GRQ No")]
        public string clmGRQNO { get; set; }
        [Display(Name = "Item Group")]
        public int clmItemGrpID { get; set; }
        [Display(Name = "Item Name")]
        public int clmItemID { get; set; }
        [Display(Name = "Issue Qty (Pcs)")]
        public decimal clmQty { get; set; }
        [Display(Name = "Issue Qty (Ctn)")]
        public decimal QtyCtn { get; set; }
        public decimal clmCostRt { get; set; }
        public decimal clmCostVal { get; set; }
        [Display(Name = "Issue From")]
        public int IssueFrom { get; set; }
        [Display(Name = "Issue To")]
        public int IssueTo { get; set; }
        [Display(Name = "Available Qty (PCs)")]
        public int AvailableQty { get; set; }
        [Display(Name = "Balance Qty")]
        public int BalanceQty { get; set; }
        public int UoM { get; set; }
        [Display(Name ="PCs Per Carton")]
        public int PcsPerCtn { get; set; }
        [Display(Name ="Available Qty (Crtn)")]
        public decimal AvailableQtyCtn { get; set; }
    }
}