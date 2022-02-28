using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.Payroll
{
    [Table("LeaveType")]
    public class LeaveType
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Leave Type ID")]
        public int slno { get; set; }
        [Required]
        [Display(Name = "Leave Type Name")]
        public string Leavename { get; set; }
        [Required]
        [Display(Name = "Number of Days")]
        public int NumberOfDays { get; set; }
    }
}