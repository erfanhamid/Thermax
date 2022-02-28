using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ViewModel.Account
{
    public class PaymentVVModel
    {
        [Display(Name = "PV No")]
        public int PVNo { get; set; }
        [Display(Name = "Date")]
        public DateTime PVDate { get; set; }
        [Display(Name = "Payment A/C")]
        public int CashBankID { get; set; }
        [Display(Name = "Receiver Name")]
        public string ReceiverName { get; set; }    
        [Display(Name = "Reference No")]
        public string RefNo { get; set; }
        [Display(Name = "Cost Center")]
        public int SalesCenterID { get; set; }
        [Display(Name = "PV Total")]
        public decimal PVTotalAmount { get; set; }
        [Display(Name = "Cost Group")]
        public int CostGroupID { get; set; }
        [Display(Name = "Debit A/C")]
        public int DebitAccID { get; set; }
        [Display(Name = "Amount")]
        public decimal RVAmount { get; set; }
        public string Description { get; set; }
        [Display(Name = "Sub Ledger")]
        public int SubLedgerID { get; set; }
        [Display(Name = "Sub Ledger Amount")]
        public decimal PVAmount { get; set; }
        public string Note { get; set; }
        [Display(Name = "Sub Ledger Total")]
        public decimal SubLedgerTotal { get; set; }
        [Display(Name ="Total Amount")]
        public decimal TotalAmount { get; set; }
    }
}