using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.Payroll
{
    [Table("ReasonofLeaving")]
    public class ReasonOfLeaving
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Display(Name = "RoL ID")]
        public Int16 RoLID { get; set; }
        [Required]
        [Display(Name = "Reason of Leaving Name")]
        public string RoL { get; set; }
    }
}