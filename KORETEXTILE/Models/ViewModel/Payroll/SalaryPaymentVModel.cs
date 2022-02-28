using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ViewModel.Payroll
{
    public class SalaryPaymentVModel
    {
        [Display(Name = "EPV No")]
        public int SPVNo { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Company")]
        public int CompanyId { get; set; }
        [Display(Name = "Paymode")]
        public String Paymode { get; set; }
        [Display(Name = "Employee ID")]
        public int EmployeeId { get; set; }
        [Display(Name = "Amount")]
        public decimal SalaryAmount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}