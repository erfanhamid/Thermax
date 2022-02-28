using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.Payroll
{
    [Table("Grade")]
    public class Grade
    {
        [Key]
        [Required]
        [Display(Name = "Grade ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GradeID { get; set; }
        [Required]
        [Display(Name = "Grade Name")]
        public string GradeName { get; set; }
    }
}