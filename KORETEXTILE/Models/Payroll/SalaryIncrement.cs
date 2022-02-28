using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Payroll
{
    [Table("SalaryIncrement")]
    public class SalaryIncrement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public DateTime ApplicableDate { get; set; }
        [Required]
        public decimal Gross { get; set; }
        public string Description { get; set; }
        public string RefNo { get; set; }
        public bool IsArrear { get; set; }
        [Required]
        public decimal IncrementAmount { get; set; }
        [NotMapped]
        public string EmployeeName { get; set; }
        [NotMapped]
        public string UpdateEmployeeName { get; set; }
        [NotMapped]
        public string CompanyName { get; set; }
        [NotMapped]
        public string WorkStation { get; set; }
        [NotMapped]
        public int BranchId { get; set; }
        [NotMapped]
        public int DesignationId { get; set; }
        [NotMapped]
        public string DesignationName { get; set; }
        [NotMapped]
        public decimal PreviousGross { get; set; }
        [NotMapped]
        public int UpdateEmployeeId { get; set; }
        [NotMapped]
        public int DepartmentId { get; set; }
        [NotMapped]
        public int FuncDesignationId { get; set; }
        [NotMapped]
        public int SectionId { get; set; }
        [NotMapped]
        public string DepartmentName { get; set; }
        [NotMapped]
        public string FuncDesignationName { get; set; }
        [NotMapped]
        public string SectionName { get; set; }
        [NotMapped]
        public int CompanyId { get; set; }
    }
}