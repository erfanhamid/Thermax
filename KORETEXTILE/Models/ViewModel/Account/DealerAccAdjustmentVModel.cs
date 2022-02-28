using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Account
{
    public class DealerAccAdjustmentVModel
    {
        [Display(Name = "DAAD No")]
        public int DAANo { get; set; }
        [Display(Name = "Depot")]
        public int DepotID { get; set; }
        [Display(Name = "Dealer")]
        public int DealerID { get; set; }
        [Display(Name = "Cost Group")]
        public int CostGroup { get; set; }
        [Display(Name = "Date")]
        public DateTime DAADate { get; set; }
        [Display(Name = "Account")]
        public int DAAAccountID { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal Due { get; set; }
        public string Zone { get; set; }
        [Display(Name =  "Total Amount")]
        public decimal TotalAmount { get; set; }

    }
}