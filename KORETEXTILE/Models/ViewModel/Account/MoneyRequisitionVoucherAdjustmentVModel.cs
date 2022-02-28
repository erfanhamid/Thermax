using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ViewModel.Account
{
    public class MoneyRequisitionVoucherAdjustmentVModel
    {
        [Display(Name = "MRVA No")]
        public int MRVANo { get; set; }
        [Display(Name = "Cost Center")]
        public int CostCenterId { get; set; }
        [Display(Name = "Employee ID")]
        public int EmpID { get; set; }
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
        [Display(Name = "Designation")]
        public string EmployeeDesignation { get; set; }
        [Display(Name = "Debit A/C")]
        public int DebitAcId { get; set; }
        [Display(Name = "Cost Group")]
        public int CostGroupId { get; set; }
        [Display(Name = "Amount")]
        public decimal MRVAAmount { get; set;}
        [Display(Name = "Description")]
        public string MRVADescription { get; set; }
        [Display(Name = "Voucher Total")]
        public decimal VoucherTotal { get; set; }
        [Display(Name = "Negative balance payment now?")]
        public bool NegativeBalanceCheckbox { get; set; }
        [Display(Name = "MRV No")]
        public int MRVNo { get; set; }
        public DateTime Date { get; set; }
        [Display(Name ="Payment A/C")]
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        [Display(Name = "Previous Balance")]
        public decimal PreviousBalance { get; set; }
        [Display(Name = "Rest Amount")]
        public double prevMinuseVoucherTotal { get; set; }
        [Display(Name = "Ref No")]
        public string RefNo { get; set; }
    }
}