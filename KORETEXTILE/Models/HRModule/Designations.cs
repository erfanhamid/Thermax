using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.HRModule
{
    [Table("Designation")]
    public class Designations
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Display(Name = "Designation ID")]
        public int Id { set; get; }
        [Required]
        [Display(Name = "Designation")]
        public string Name { set; get; }
    }
}