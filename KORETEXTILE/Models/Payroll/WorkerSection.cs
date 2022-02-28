using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Payroll
{
    [Table("WorkerSections")]
    public class WorkerSection
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Production Section ID")]
        public int ProdSectID { get; set; }

        [Required]
        [Display( Name = "Production Section")]
        public string ProductionSection { get; set; }
    }
}