using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Payroll
{
    [Table("WorkPlaces")]
    public class WorkPlace
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Work Place No")]
        public int WorkPlaceNo { get; set; }

        [Required]
        [Display(Name = "Work Place")]
        public string WorksPlace { get; set; }
    }
}