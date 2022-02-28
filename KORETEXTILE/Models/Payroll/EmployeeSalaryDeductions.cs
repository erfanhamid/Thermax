using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace BEEERP.Models.Payroll
{
    public class EmployeeSalaryDeductions
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeID { get; set; }
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
        [NotMapped]
        public string EmployeeName { get; set; }
        [NotMapped]
        public string Designation { get; set; }
    }
}