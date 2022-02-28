using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Payroll
{
    [Table("WorkerTypes")]
    public class WorkerType
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Work Type No")]
        public int WTNo { get; set; }

        [Required]
        [Display(Name = "Work Type")]
        public string WorksType { get; set; }
    }
}