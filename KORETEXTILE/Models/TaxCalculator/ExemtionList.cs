using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.TaxCalculator
{
    [Table("tblExemtionList")]
    public class ExemtionList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Column("id")]
        public int Id { get; set; } 
        [Required]
        [Column("clmItem")]
        public string Item { get; set; }
        [Required]
        [Column("clmPercentage")]
        public float Percentage { get; set; }
        [Required]
        [Column("clmLimit")]
        public decimal Limit { get; set; }
    }
}