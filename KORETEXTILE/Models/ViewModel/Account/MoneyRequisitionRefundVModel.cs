using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ViewModel.Account
{
    public class MoneyRequisitionRefundVModel
    {
        [Display(Name = "MRR No")]
        public int MRRNo { get; set; }
        //[Display(Name = "MRV NO")]
        //public int MRVNo { get; set; }
        [Display(Name = "Employee ID")]
        public int EmpID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Account")]
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        [Display(Name = "Ref No")]
        public string RefNo { set; get; }
        public string Description { get; set; }
        public decimal Balance { get; set; }
        //[Display(Name = "Total Amount")]
        //public decimal TotalAmount { get; set; }
        //[Display(Name = "Due Amount")]
        //public decimal DueAmount { get; set; }
    }
}