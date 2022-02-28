using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Account
{
    public class DealerSalesIncentiveAdjustmentVModel
    {
        [Display(Name = "DSIA No")]
        public int DSIANo { get; set; }
        [Display(Name ="Date")]
        public DateTime DSIADate { get; set; }
        //[Display(Name = "Account")]
        //public int AccountID { get; set; }
        [Display(Name = "Depot")]
        public int DepotID { get; set; }
        public string RefNo { get; set; }
        public string Description { get; set; }
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }
        [Display(Name = "Dealer")]
        public int DealerID { get; set; }
        public decimal Amount { get; set; }
        [Display(Name ="Available Amount")]
        public decimal AvailableAmount { get; set; }
    }
}