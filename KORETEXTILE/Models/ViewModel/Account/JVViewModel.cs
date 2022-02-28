using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Account
{
    public class JVViewModel
    {
        [Display(Name = "JV No")]
        public int JVNO { get; set; }
        [Display(Name = "Date")]
        public DateTime JVDate { get; set; }
        [Display(Name = "Ref No")]
        public string RefNo { get; set; }
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }
        [Display(Name = "Cost Center")]
        public int BranchId { get; set; }
        [Display(Name = "Cost Group")]
        public int CostGroupId { get; set; }
        [Display(Name = "Amount")]
        public decimal JVAmount { get; set; }
        [Display(Name = "Debit/Credit")]
        public string DebOrCre { get; set; }
        public string Description { get; set; }
        [Display(Name = "GL Account")]
        public int AccountHeadID { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        [Display(Name = "Sub Ledger")]
        public int SubLedgerID { get; set; }
        [Display(Name = "Sub Ledger Amount")]
        public decimal SubJVAmount { get; set; }    
        public string Note { get; set; }
        public decimal SubLedgerATot { get; set; }
    }
}