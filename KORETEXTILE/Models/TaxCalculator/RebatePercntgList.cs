using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.TaxCalculator
{
    [Table("tblRebatePercntgList")]
    public class RebatePercntgList
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [Column("clmItem")]
        public string Item { get; set; }
        [Required]
        [Column("clmPercentage")]
        public float Percentage { get; set; }
    }
}