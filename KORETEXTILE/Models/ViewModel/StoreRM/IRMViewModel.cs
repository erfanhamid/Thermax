using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ViewModel.StoreRM
{
    public class IRMViewModel
    {
        [Display(Name = "IRMP No")]
        public int IRMID { get; set; }
        [Display(Name = "Date")]
        public DateTime IRMDate { get; set; }
        [Display(Name = "Total")]
        public decimal IRMTotal { get; set; }
        public decimal? TotalCost { get; set; }
        public string Description { get; set; }
        [Display(Name = "Ref No")]
        public string Refno { get; set; }
        [Display(Name = "GRQ No")]
        public int GRQNO { get; set; }
        [Display(Name = "Group")]
        public int GroupID { get; set; }    
        [Display(Name = "Item")]
        public int ItemID { get; set; }
        [Display(Name = "UoM")]
        public int UomID { get; set; }
        [Display(Name = "Available Qty")]
        public decimal StockQty { get; set; }
        [Display(Name = "Issue Qty")]
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal Value { get; set; }
        [Display(Name = "Balance Qty")]
        public decimal BalanceQty { get; set; }
        [Display(Name = "Issue From")]
        public int IssueFrom { get; set; }
        [Display(Name = "Issue To")]
        public int IssueTo { get; set; }
        public decimal CostRate { get; set; }
    }
}