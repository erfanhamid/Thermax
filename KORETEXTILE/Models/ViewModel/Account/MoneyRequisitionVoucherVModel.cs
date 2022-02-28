using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ViewModel.Account
{
    public class MoneyRequisitionVoucherVModel
    {
        [Display(Name = "MRV No")]
        public int MRVId { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Employee ID")]
        public int EmployeeId { get; set; }
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
        [Display(Name = "Designation")]
        public string Designation { get; set; }
        [Display(Name = "Previous Balance")]
        public double PreviousBalance { get; set; }
        [Display(Name = "Current Balance")]
        public double CurrentBalance { get; set; }
        [Display(Name = "Payment A/C")]
        public int PaymentAcId { get; set; }
        public decimal Amount { get; set; }
        [Display(Name ="Ref No")]
        public string RefNo { get; set; }
        public string Description { get; set; }
    }
}