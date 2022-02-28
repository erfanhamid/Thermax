using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BEEERP.Models.Payroll
{
    [Table("Departments")]
    public class Department
    {
        [Key]
        [Required]
        [Display(Name = "Department ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int16 DeptmentID { get; set; }
        [Required]
        [Display(Name = "Department Name")]
        public string DeprtmntName { get; set; }   
    }
}