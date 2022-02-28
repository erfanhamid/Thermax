using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.Payroll
{
    [Table("HighestEducation")]
    public class HighestEducation
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Highest Education ID")]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Highest Education Name")]
        public string Education { get; set; }
    }
}