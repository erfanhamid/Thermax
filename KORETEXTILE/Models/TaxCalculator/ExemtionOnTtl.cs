using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.TaxCalculator
{
    [Table("tblExemtionOnTtl")]
    public class ExemtionOnTtl
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Id")]
        public int Id { get; set; }
        [Required]
        [Column("clmGender")]
        public string Gender { get; set; }   
        [Column("clmLimit")]
        public decimal Limit { get; set; }

    }
}