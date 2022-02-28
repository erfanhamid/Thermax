using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Payroll
{
    [Table("WorkerDesignations")]
    public class WorkerDesignation
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Worker Designation No")]
        public int WDesignationNo { get; set; }

        [Required]
        [Display( Name = "Worker Designation")]
        public string WorkersDesignation { get; set; }
    }
}