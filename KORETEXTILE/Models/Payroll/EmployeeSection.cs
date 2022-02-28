using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.Payroll
{
    [Table("CostGroup")]
    public class EmployeeSection
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "SL No")]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Section Description")]
        public string CGroup { get; set; }
    }
}