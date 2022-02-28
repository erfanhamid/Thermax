using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.Data_Admin
{
    [Table("Screen")]
    public class Screen
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ScreenID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int ModuleID { get; set; }
    }
}