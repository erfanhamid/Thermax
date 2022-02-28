using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Payroll
{
    [Table("Tso_Rsm")]
    public class TsoRsm
    {
        [Key]
        [Column(Order = 0)]
        public int RsmId { set; get; }
        [Key]
        [Column(Order = 1)]
        public int TsoId { set; get; }
        [NotMapped]
        public string TsoName { set; get; }
        [NotMapped]
        public string RsmName { set; get; }
    }
}