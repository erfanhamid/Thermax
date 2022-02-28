using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ViewModel.Account
{
    public class LTRAVModel
    {
        [Display(Name = "LTRA No")]
        public int LTRANo { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "LTR Id")]
        public int LTRID { get; set; }
        [Display(Name = "Entry Type")]
        public string Type { get; set; }
        [Display(Name = "Account No")]
        public int AccountID { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        [Display(Name = "ILC Id")]
        public int ILCId { get; set; }
        [Display(Name = "ILC No")]
        public string ILCNo { get; set; }
        [Display(Name = "AIt ILC No")]
        public string AitILCNo { get; set; }
        public string Supplier { get; set; }
        [Display(Name = "LTR No")]
        public int LTRNo { get; set; }
        public string GL { get; set; }
        [Display(Name = "OB Amount")]
        public decimal OBAmount { get; set; }
        [Display(Name = "Balance Amount")]
        public decimal BalanceAmount { get; set; }
        [Display(Name ="Reference No")]
        public string RefNo { get; set; }
    }
}