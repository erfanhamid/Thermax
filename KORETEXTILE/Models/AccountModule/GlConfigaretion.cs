using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.AccountModule
{
    [Table("TblGlConfiguration")]
    public class GlConfiguration
    {
        public int Id { get; set; }
        [Required]
        [Key, Column(Order = 0)]
        public string VehicleType { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public int GlAccount { get; set; }
        [NotMapped]
        public string AccName { get; set; }
    }
}