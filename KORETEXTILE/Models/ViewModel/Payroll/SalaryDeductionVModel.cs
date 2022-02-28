using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Payroll
{
    public class SalaryDeductionVModel
    {
        [Required]
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        [Required]
        public decimal AdvanceAdjustment { get; set; }
        [Required]
        public decimal IncomeTax { get; set; }
        [Required]
        public decimal ProvidentFund { get; set; }
        [Required]
        public decimal OtherAdjustment { get; set; }
        [Required]
        public decimal MCAdjustment { get; set; }
    }
}