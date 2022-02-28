using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Payroll
{
    [Table("EmployeesPromotionHistory")]
    public class EmployeesPromotionHistory
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public int EmployeeId { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public int OldDesignation { set; get; }
        [Required]
        public int NewDesignation { set; get; }
        [Required]
        public int OldDepatmentID { set; get; }
        [Required]
        public int NewDepatmentID { set; get; }
        [Required]
        public int OldFunctDesignation { set; get; }
        [Required]
        public int NewFunctDesignation { set; get; }
        [Required]
        public int OldSection { set; get; }
        [Required]
        public int NewSection { set; get; }
        [Required]
        public int OldBranchID { set; get; }
        [Required]
        public int NewBranchID { set; get; }
        [Required]
        public int OldCompanyId { set; get; }
        [Required]
        public int NewCompanyId { set; get; }
        [Required]
        public decimal OldGrossSalary { set; get; }
        [Required]
        public decimal NewGrossSalary { set; get; }
        [Required]
        public DateTime ApplicableDate { get; set; }
        [Required]
        public DateTime PreviousDate { get; set; }
    }
}