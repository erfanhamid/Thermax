using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Production
{
    public class RmcVmodel
    {
       
        [Display(Name = "RMC No")]
        public int RMCNo { get; set; }
        [Display(Name = "Date")]
        public DateTime RMCDate { get; set; }
        [Display(Name = "Batch")]
        public int BatchID { get; set; }
        [Display(Name = "Store")]
        public int StoreId { get; set; }
        [Display(Name = "Reference No")]
        public string RefNo { get; set; }
        [Display(Name = "Description")]
        public string Descriptions { get; set; }
        public decimal RMCTotalQty { set; get; }
        public int GroupID { get; set; }
        [Display(Name = "Item Name")]
        public int ItemID { set; get; }
        [Display(Name ="Available QTY")]
        public decimal AvailableQTY { set; get; }
        [Display(Name = "Issue QTY")]
        public decimal IssueQty { set; get; }
        [Display(Name = "Issue QTY")]
        public decimal ItemQty { set; get; }
        [Display(Name = "Balance QTY")]
        public decimal BalanceQty { set; get; }
        [Display(Name ="UoM")]
        public string UOM { set; get; }
        [Display(Name ="RMC Rate")]
        public decimal Rate { get; set; }
        [Display(Name ="Total RMC Value")]
        public decimal RMCTotalValue { get; set; }
        [Display(Name = "Group")]
        public int SupplierGroup { get; set; }
    }
}