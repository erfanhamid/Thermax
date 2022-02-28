using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ViewModel.Account
{
    public class MRPAVModel
    {
        [Display(Name = "MRPA No")]
        public int MRPANo { get; set; }
        [Display(Name = "Date")]
        public DateTime MRPADate { get; set; }
        [Display(Name = "Employee ID")]
        public int EmployeeID { get; set; }
        [Display(Name = "Supplier")]
        public int SupplierID { get; set; }
        [Display(Name = "Adjustment Amount")]
        public decimal AdjustmentAmnt { get; set; }
        [Display(Name = "Reference No")]
        public string RefNo { get; set; }
        public string Descriptions { get; set; }
        [Display(Name = "Employee Name")]
        public string EmpName { get; set; }
        [Display(Name = "Designation")]
        public string EmpDesignation { get; set; }
        [Display(Name = "Current Balance")]
        public decimal CurrentBal { get; set; }
        [Display(Name = "Previous Balance")]
        public decimal PreviousBal { get; set; }
    }
}