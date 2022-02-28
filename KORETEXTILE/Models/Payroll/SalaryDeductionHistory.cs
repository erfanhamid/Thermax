using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Payroll
{
    public class SalaryDeductionHistory
    {
        [Key][Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public string Designation { get; set; }
        [Required]
        public decimal AdvanceAdjustment { get; set; }
        [Required]
        public decimal IncomeTax { get; set; }
        [Required]
        public decimal ProvidentFund { get; set; }
        public decimal OtherAdjustment { get; set; }
        [Required]
        public DateTime UpdateDate { get; set; }
        [Required]
        public decimal MCAdjustment { get; set; }
    }
}