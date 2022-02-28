using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ViewModel.Account
{
    public class RefundFromSupplierVModel
    {
        [Display(Name ="PRS No")]
        public int PRSNo { set; get; }
        public DateTime Date { set; get; }
        [Display(Name = "Supplier")]
        public int SupplierID { set; get; }
        public decimal Due { set; get; }
        [Display(Name = "Select A/C")]
        public int ReceiveAccountID { set; get; }
        [Display(Name = "Amount")]
        public decimal ReturnAmount { set; get; }
        [Display(Name = "Reference No")]
        public string RefNo { set; get; }
        public string Description { set; get; }   
    }
}