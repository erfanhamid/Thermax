using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.PaymentReceiveInfo
{
    public class RevPaymentVModel
    {
        [Display(Name = "DRP")]
        public int RPID { get; set; }
        [Display(Name = "Customer ID")]
        public int CustomerID { get; set; }
        [Required]
        [Display(Name = "Date")]
        public DateTime TDate { get; set; }
        [Required]
        [Display(Name = "Account Id")]
        public int AccountID { get; set; }
        [Required]
        [Display(Name = "Amount")]
        public decimal ReceiveAmt { get; set; }
        [Required]
        [Display(Name = "Reference No")]
        public string RefNo { get; set; }
        public string Description { get; set; }
        public string Purpose { get; set; }
        [Required]
        [Display(Name = "Bank Charges")]
        public decimal BankCharges { get; set; }
        [Required]
        [Display(Name = "Net Amount")]
        public decimal NetAmount { get; set; }
        [Required]
        [Display(Name = "Depot")]
        public int DepotId { get; set; }
        [Required]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Required]
        [Display(Name = "Zone")]
        public int ZoneId { get; set; }
        [Required]
        public string Area { get; set; }
        public decimal Due { get; set; }
        public string AccName { get; set; }
        public string ZoneName { get; set; }
        public string PayMode { set; get; }
    }
}