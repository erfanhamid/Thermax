using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.TaxCalculator
{
    [Table("tblSLabList")]
    public class SLabList
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID")]
        public int Id { get; set; } 
        [Required]
        [Column("clmSlab")]
        public string Slab { get; set; }
        [Required]
        [Column("clmMaxLimit")]
        public decimal MaxLimit { get; set; }
        [Required]
        [Column("Percentage")]
        public float Percentage { get; set; }
    }
}