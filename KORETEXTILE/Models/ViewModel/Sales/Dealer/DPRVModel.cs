using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Sales.Dealer
{
    public class DPRVModel
    {
        [Display(Name ="DPR No")]
        public int RPID { get; set; }
        [Display(Name ="Depot")]
        public int DepotID { get; set; }
        [Display(Name ="Dealer ID")]
        public int CustomerID { get; set; }
        public string Zone { get; set; }
        public string Area { get; set; }
        [Display(Name ="Dealer Name")]
        public string CustomerName { get; set; }
        public decimal Due { get; set; }
        [Display(Name ="Date")]
        public DateTime TDate { get; set; }
        [Display(Name ="Account")]
        public int AccountID { get; set; }
        [Display(Name ="Amount")]
        public decimal ReceiveAmt { get; set; }
        [Display(Name ="Reference No")]
        public string RefNo { get; set; }
        public string Description { get; set; }
        public string Purpose { get; set; }
        [Display(Name = "Bank Charges")]
        public decimal BankCharges { get; set; }
        [Display(Name ="Net Amount")]
        public decimal NetAmount { get; set; }
    }
}