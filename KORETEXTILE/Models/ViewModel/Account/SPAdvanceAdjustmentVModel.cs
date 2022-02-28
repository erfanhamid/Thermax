using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ViewModel.Account
{
    public class SPAdvanceAdjustmentVModel
    {
        [Display(Name = "SPAA No")]
        public int SPAANo { get; set; }
        public int GroupID { get; set; }
        [Display(Name = "Supplier ID")]
        public int SupplierID { get; set; }
        [Display(Name = "Supplier Name")]
        public string SupplierName { get; set; }
        public DateTime Date { get; set; }
        public decimal Due { get; set; }
        public string Remarks { get; set; }
        [Display(Name = "Due Amount")]
        public decimal DueAmount { get; set; }
        [Display(Name = "Advance Amount")]
        public decimal AdvanceAmount { get; set; }
        [Display(Name = "Adjusted Amount")]
        public decimal AdjustedAmount { get; set; }
    }
}