using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.CommercialVM
{
    public class ImportLCPaymentViewModel
    {
        [Display(Name = "ILCP No")]
        public int ILCPNo { get; set; }
        [Display(Name = "ILC ID")]
        public int ILCID { get; set; }
        [Display(Name = "ILC No")]
        public String ILCNo { get; set; }
        [Display(Name = "Alt ILC No")]
        public int AltILCNo { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; } 

        [Display(Name = "Select Debit Account")]
        public int DebitAccID { get; set; }
        [Display(Name = "Select Credit Account")]
        public int CreditAccID { get; set; }
        public decimal Amount { get; set; }
        public string RefNo { get; set; }
        public string Description { get; set; }
    }
}