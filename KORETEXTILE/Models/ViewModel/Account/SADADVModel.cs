using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ViewModel.Account
{
    public class SADADVModel
    {
        [Display(Name = "SADAD No")]
        public int SADADNo { get; set; }
        public DateTime TDate { get; set; }
        [Display(Name = "Group")]
        public int GroupID { get; set; }
        [Display(Name = "Supplier")]
        public int SupplierID { get; set; }
        public string Discription { get; set; }
        [Display(Name = "Reference No")]
        public string RefNo { get; set; }
        [Display(Name = "Bill Type")]
        public string BillType { get; set; }
        [Display(Name = "Bill No")]
        public int BillNo { get; set; }
        [Display(Name = "Remaining Bill Amount")]
        public decimal BillAmount { get; set; }
        [Display(Name = "Paid Amount")]
        public decimal PaidAmount { get; set; }
        [Display(Name = "Balance Amount")]
        public decimal BalAmount { get; set; }
        [Display(Name = "Adjustable SPV No")]
        public int AdjustSPVNo { get; set; }
        [Display(Name = "Available Amount")]
        public decimal AvalAmount { get; set; }
        public decimal Amount { get; set; }
        [Display(Name = "Rest Amount in Advance")]
        public decimal RAmountAdvance { get; set; }
        [Display(Name = "Amount")]
        public decimal PayInfoAmount { get; set; }
        public decimal Remaining { get; set; }
        [Display(Name = "Balance Amount")]
        public decimal BalanceAmount { get; set; }
        [Display(Name = "Payment Amount")]
        public decimal PaymentAmount { get; set; }
        [Display(Name = "Remaining Amount")]
        public decimal RemainingAmount { get; set; }
        [Display(Name = "Total Paid Amount")]
        public decimal TotalPaidAmount { get; set; }
        public int AdvanceNO { get; set; }
        [Display(Name = "Remaining Advance Amount")]
        public decimal RemAdvAmount { get; set; }
        public decimal TAAmount { get; set; }
        [Display(Name = "Advance Amount")]
        public decimal AdvanceAmount { get; set; }
    }
}