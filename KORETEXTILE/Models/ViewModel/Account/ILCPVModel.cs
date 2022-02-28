using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ViewModel.Account
{
    public class ILCPVModel
    {
        [Display(Name = "ILCP No")]
        public int ILCPNo { get; set; }
        [Display(Name = "ILC ID")]
        public int ILCID { get; set; }
        [Display(Name = "ILC No")]
        public int ILCNo { get; set; }
        [Display(Name = "Alt ILC No")]
        public int AltILCNo { get; set; }
        public string Supplier { get; set; }
        public int Date { get; set; }
        [Display(Name = "Payment A/C")]
        public int AccountID { get; set; }
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }

        [Display(Name = "Select A/C")]
        public int DebitAccID { get; set; }
        public decimal Amount { get; set; }     
        public string Description { get; set; }
        
        [Display(Name = "Sub Ledger")]
        public int SubLedgerID { get; set; }
        [Display(Name = "Sub Ledger Amount")]
        public decimal SlAmount { get; set; }
        [Display(Name = "Note")]
        public string SlDescription { get; set; }
        [Display(Name = "Sub Ledger Total")]
        public decimal SubLedgerTotal { get; set; }
    }
}