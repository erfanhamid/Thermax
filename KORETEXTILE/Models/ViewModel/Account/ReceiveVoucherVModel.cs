using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ViewModel.Account
{
    public class ReceiveVoucherVModel
    {
        [Display(Name = "RV No")]
        public int RVNo { set; get; }
        [Display(Name = "Date")]
        public DateTime RVDate { set; get; }
        [Display(Name = "Amount")]
        public decimal RVAmount { set; get; }
        [Display(Name = "Receive A/C")]
        public int ReceiveAccountID { set; get; }
        [Display(Name = "Credit A/C")]
        public int CreditAccountID { set; get; }
        [Display(Name = "Receiver Name")]
        public string PayeeName { set; get; }
        public string Description { set; get; }
        [Display(Name = "Reference No")]
        public string RefNo { get; set; }
        [Display(Name = "Cost Center")]
        public int BranchID { set; get; }
        [Display(Name = "Cost Group")]
        public int CostGroupID { set; get; }
        [Display(Name = "RV No")]
        public int IDP { set; get; }
        [Display(Name = "RV No")]
        public string YearPart { set; get; }      
    }
}