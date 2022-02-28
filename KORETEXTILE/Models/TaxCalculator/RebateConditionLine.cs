using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.TaxCalculator
{
    [Table("tblRebateConditionLine")]
    public class RebateConditionLine
    {
        [Required]
        [Key, Column("conditionID", Order = 0)]
        public int ConditionID { get; set; } 
        [Required]
        [Key, Column(Order = 1)]
        public int SlabID { get; set; }
        [Required]
        [Column("clmSlab")]
        public string Slab { get; set; }
        [Required]
        [Column("clmMaxLimit")]
        public decimal MaxLimit { get; set; }
        [Required]
        public float Percentage { get; set; }
        [NotMapped]
        public string ConditionName { get; set; }
    }
}