using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Models.ViewModel.Account
{
    public class SAADVModel
    {
        public int SAADNo { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Adjustment Account")]
        public int LedgerAcId { get; set; }
        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }
        public decimal Amount { get; set; }
        [Display(Name = "Ref No")]
        public string RefNo { get; set; }
        public int MyProperty { get; set; }
        public string Description { get; set; }
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Balance")]
        public decimal AdBalance { get; set; }
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Balance")]
        public decimal EmpBalance { get; set; }
        [Display(Name = "TotalPaidAmount")]
        public decimal TotalPaidAmount { get; set; }
        [Display(Name = "Group")]
        public int GroupID { get; set; }
        [Display(Name = "Bill Type")]
        public string BillType { get; set; }
        [Display(Name = "Bill No")]
        public int BillNo { get; set; }
        [Display(Name = "Remaining Bill Amount")]
        public decimal BillAmount { get; set; }
        [Display(Name = "Adjustment Amount")]
        public decimal PaidAmount { get; set; }
        [Display(Name = "Balance Amount")]
        public decimal BalAmount { get; set; }
    }
}