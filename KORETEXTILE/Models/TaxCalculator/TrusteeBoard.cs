using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.TaxCalculator
{
    [Table("tblTrusteeBoard")]
    public class TrusteeBoard
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        [Column("clmTrusteeNo")]
        public int TrusteeNo { get; set; }
        [Required]
        [Column("clmTrusteeName")]
        public string TrusteeName { get; set; } 
    }
}