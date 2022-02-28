using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Payroll
{
    public class SetSalaryStructureVModel
    {
        [Display(Name = "Employee Grade")]
        public int Grade { get; set; }
        [Display(Name ="Basic")]
        public decimal Basic { get; set; }
        [Display(Name ="House Rent")]
        public decimal HouseRent { get; set; }
        [Display(Name = "Medical Allowance")]
        public decimal MedicalAllowance { get; set; }
        [Display(Name = "Conveyance Allowance")]
        public decimal ConveyanceAllowance { get; set; }
        [Display(Name = "Special Allowance")]
        public decimal DearnessAllowance { get; set; }
        public decimal Gross { get; set; }
        [Display(Name = "Scale")]
        public string Scale { get; set; }
    }
}